using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight.CommandWpf;
using SudokuSolution.Common.Execution;
using SudokuSolution.Common.Extensions;
using SudokuSolution.Domain.Entities;
using SudokuSolution.Logic.GameService;
using SudokuSolution.Wpf.Common.Base;
using SudokuSolution.Wpf.Common.Commands;
using SudokuSolution.Wpf.Common.Dispatcher;
using SudokuSolution.Wpf.Common.MessageBox;
using SudokuSolution.Wpf.Controls.Field;

namespace SudokuSolution.Wpf.Views.Solved;

public class SolvedViewModel : ViewModel<SolvedWindow>
{
	public override object Header => string.Empty;

	private bool _isBusy;
	private int _currentSolved;
	private int? _totalSolvedCount;

	private ICommand _loadedCommand;
	private ICommand _prevSolvedCommand;
	private ICommand _nextSolvedCommand;
	private ICommand _contentRenderedCommand;
	private ICommand _handleMouseMoveCommand;

	private readonly IGameService _gameService;
	private readonly IDispatcherHelper _dispatcherHelper;
	private readonly IMessageBoxService _messageBoxService;

	private readonly Field _startField;
	private readonly int _maxSolvedCount;
	private readonly bool _solveAllFields;

	private List<Field> _solvedFields;
	private FieldEnumeratorAsync _fieldEnumeratorAsync;

	private readonly ExecutionTracker _executionTracker;

	public bool IsBusy
	{
		get => _isBusy;
		set => Set(ref _isBusy, value);
	}

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
	public ICommand NextSolvedCommand => _nextSolvedCommand ??= new AsyncRelayCommand(OnNextSolvedAsync, CanNextSolved);
	public ICommand ContentRenderedCommand => _contentRenderedCommand ??= new AsyncRelayCommand(OnContentRenderedAsync);
	public ICommand HandleMouseMoveCommand => _handleMouseMoveCommand ??= new RelayCommand<MouseEventArgs>(OnHandleMouseMove);

	public SolvedViewModel(
		IGameService gameService,
		IDispatcherHelper dispatcherHelper,
		IMessageBoxService messageBoxService,
		FieldViewModel fieldViewModel,
		Field startField,
		int maxSolvedCount,
		bool solveAllFields)
	{
		_gameService = gameService;
		_dispatcherHelper = dispatcherHelper;
		_messageBoxService = messageBoxService;
		_startField = startField;
		_maxSolvedCount = maxSolvedCount;
		_solveAllFields = solveAllFields;
		FieldViewModel = fieldViewModel;

		_executionTracker = new ExecutionTracker(
			() => _dispatcherHelper.CheckBeginInvokeOnUI(() => IsBusy = true),
			() => _dispatcherHelper.CheckBeginInvokeOnUI(() => IsBusy = false));
	}

	private void OnLoaded()
	{
		FieldViewModel.LockSelectedMenu = true;
		FieldViewModel.RefreshField(_startField.MaxValue);
	}

	private void OnPrevSolved()
	{
		using (_executionTracker.TrackExecution())
		{
			CurrentSolved--;
			LoadCurrentField();
		}
	}

	private bool CanPrevSolved()
	{
		return CurrentSolved > 1;
	}

	private async Task OnNextSolvedAsync()
	{
		using (_executionTracker.TrackExecution())
		{
			if (CurrentSolved == _solvedFields.Count)
			{
				if (CurrentSolved == _maxSolvedCount)
				{
					_dispatcherHelper.CheckBeginInvokeOnUI(() => TotalSolvedCount = CurrentSolved);
					return;
				}

				var moveNextResult = await _fieldEnumeratorAsync.MoveNextAsync().ConfigureAwait(false);
				if (!moveNextResult)
				{
					_dispatcherHelper.CheckBeginInvokeOnUI(() => TotalSolvedCount = CurrentSolved);
					return;
				}

				_solvedFields.Add(_fieldEnumeratorAsync.Current);
			}

			_dispatcherHelper.CheckBeginInvokeOnUI(() =>
			{
				CurrentSolved++;
				LoadCurrentField();
			});
		}
	}

	private bool CanNextSolved()
	{
		return !TotalSolvedCount.HasValue || CurrentSolved < TotalSolvedCount;
	}

	private async Task OnContentRenderedAsync()
	{
		using (_executionTracker.TrackExecution())
		{
			if (_solveAllFields)
			{
				_solvedFields = await SolveAllFieldsAsync().ConfigureAwait(false);
			}
			else
			{
				_solvedFields = new List<Field>();
				_fieldEnumeratorAsync = _gameService.StartSolve(_startField);
				var moveNextResult = await _fieldEnumeratorAsync.MoveNextAsync().ConfigureAwait(false);
				if (moveNextResult)
					_solvedFields.Add(_fieldEnumeratorAsync.Current);
			}

			_dispatcherHelper.CheckBeginInvokeOnUI(() =>
			{
				if (!_solvedFields.Any())
				{
					_messageBoxService.Show("Решений нет!", "", MessageBoxButton.OK, MessageBoxImage.Warning);
					TypedView.Close();
					return;
				}

				CurrentSolved = 1;
				if (_solveAllFields)
					TotalSolvedCount = _solvedFields.Count;

				LoadCurrentField();
			});
		}
	}

	private Task<List<Field>> SolveAllFieldsAsync()
	{
		var taskCompletionSource = new TaskCompletionSource<List<Field>>();
		Task.Run(() => taskCompletionSource.SetResult(_gameService.Solve(_startField).Take(_maxSolvedCount).ToList()));
		return taskCompletionSource.Task;
	}

	private void LoadCurrentField()
	{
		var field = _solvedFields[CurrentSolved - 1];

		if (FieldViewModel.Size != field.MaxValue)
			FieldViewModel.RefreshField(field.MaxValue);

		FieldViewModel.Cells.ForEach((row, cells) => cells.ForEach((column, cell) =>
		{
			cell.Value = field.Cells[row, column].Final;
			cell.IsBoldFont = _startField.Cells[row, column].HasFinal;
		}));
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
		SolvedViewModel Create(Field startField, int maxSolvedCount, bool solveAllFields);
	}
}