using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight.CommandWpf;
using SudokuSolution.Common.Extensions;
using SudokuSolution.Domain.Entities;
using SudokuSolution.Wpf.Common.Base;
using SudokuSolution.Wpf.Common.MessageBox;
using SudokuSolution.Wpf.Controls.Field;

namespace SudokuSolution.Wpf.Views.Solved;

public class SolvedViewModel : ViewModel<SolvedWindow>
{
	public override object Header => string.Empty;

	private int _currentSolved;
	private int? _totalSolvedCount;

	private ICommand _loadedCommand;
	private ICommand _prevSolvedCommand;
	private ICommand _nextSolvedCommand;
	private ICommand _contentRenderedCommand;
	private ICommand _handleMouseMoveCommand;

	private readonly IMessageBoxService _messageBoxService;

	private readonly Field _startField;
	private readonly IEnumerator<Field> _solvedFields;
	private readonly bool _solveAllFields;

	private readonly List<Field> _fields;

	public int CurrentSolved
	{
		get => _currentSolved;
		set => Set(ref _currentSolved, value);
	}

	public int? TotalSolvedCount
	{
		get => _totalSolvedCount;
		set => Set(ref _totalSolvedCount, value);
	}

	public FieldViewModel FieldViewModel { get; private set; }

	public ICommand LoadedCommand => _loadedCommand ??= new RelayCommand(OnLoaded);
	public ICommand PrevSolvedCommand => _prevSolvedCommand ??= new RelayCommand(OnPrevSolved, CanPrevSolved);
	public ICommand NextSolvedCommand => _nextSolvedCommand ??= new RelayCommand(OnNextSolved, CanNextSolved);
	public ICommand ContentRenderedCommand => _contentRenderedCommand ??= new RelayCommand(OnContentRendered);
	public ICommand HandleMouseMoveCommand => _handleMouseMoveCommand ??= new RelayCommand<MouseEventArgs>(OnHandleMouseMove);

	public SolvedViewModel(
		IMessageBoxService messageBoxService,
		FieldViewModel fieldViewModel,
		Field startField,
		IEnumerable<Field> solvedFields,
		bool solveAllFields)
	{
		_messageBoxService = messageBoxService;
		_startField = startField;
		_solvedFields = solvedFields.GetEnumerator();
		_solveAllFields = solveAllFields;
		FieldViewModel = fieldViewModel;

		_fields = new List<Field>();
	}

	private void OnLoaded()
	{
		FieldViewModel.LockSelectedMenu = true;

		if (_solveAllFields)
		{
			while (_solvedFields.MoveNext())
				_fields.Add(_solvedFields.Current);

			TotalSolvedCount = _fields.Count;
		}
		else
		{
			if (_solvedFields.MoveNext())
				_fields.Add(_solvedFields.Current);
			else
				TotalSolvedCount = 0;
		}

		if (TotalSolvedCount == 0)
			return;

		CurrentSolved = 1;
		LoadCurrentField();
	}

	private void LoadCurrentField()
	{
		var field = _fields[CurrentSolved - 1];

		if (FieldViewModel.Size != field.MaxValue)
			FieldViewModel.RefreshField(field.MaxValue);

		FieldViewModel.Cells.ForEach((row, cells) => cells.ForEach((column, cell) =>
		{
			cell.Value = field.Cells[row, column].Final;
			cell.IsBoldFont = _startField.Cells[row, column].HasFinal;
		}));
	}

	private void OnPrevSolved()
	{
		CurrentSolved--;
		LoadCurrentField();
	}

	private bool CanPrevSolved()
	{
		return CurrentSolved > 1;
	}

	private void OnNextSolved()
	{
		if (CurrentSolved == _fields.Count)
		{
			if (!_solvedFields.MoveNext())
			{
				TotalSolvedCount = _fields.Count;
				return;
			}

			_fields.Add(_solvedFields.Current);
		}

		CurrentSolved++;
		LoadCurrentField();
	}

	private bool CanNextSolved()
	{
		return !TotalSolvedCount.HasValue || CurrentSolved < TotalSolvedCount;
	}

	private void OnContentRendered()
	{
		if (TotalSolvedCount != 0)
			return;

		_messageBoxService.Show("Решений нет!", "", MessageBoxButton.OK, MessageBoxImage.Warning);
		TypedView.Close();
	}

	private static void OnHandleMouseMove(MouseEventArgs obj)
	{
		obj.Handled = true;
	}

	public override void Cleanup()
	{
		FieldViewModel?.Cleanup();
		FieldViewModel = null;
		base.Cleanup();
	}

	public interface IFactory
	{
		SolvedViewModel Create(Field startField, IEnumerable<Field> solvedFields, bool solveAllFields);
	}
}