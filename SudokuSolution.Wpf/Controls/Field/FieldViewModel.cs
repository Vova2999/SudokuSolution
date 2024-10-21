using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using SudokuSolution.Wpf.Common.Base;
using SudokuSolution.Wpf.Helpers;
using SudokuSolution.Wpf.Messages;

namespace SudokuSolution.Wpf.Controls.Field;

public class FieldViewModel : ViewModel<FieldControl>
{
	public override object Header => string.Empty;

	private int _size;
	private bool _lockSelectedMenu;
	private CellModel _selectedCell;
	private IReadOnlyList<IReadOnlyList<int>> _values;
	private IReadOnlyList<IReadOnlyList<CellModel>> _cells;

	private ICommand _keyDownCommand;
	private ICommand _selectCellCommand;
	private ICommand _setCellValueCommand;

	public int Size
	{
		get => _size;
		set => Set(ref _size, value);
	}

	public bool LockSelectedMenu
	{
		get => _lockSelectedMenu;
		set => Set(ref _lockSelectedMenu, value);
	}

	public CellModel SelectedCell
	{
		get => _selectedCell;
		set => Set(ref _selectedCell, value);
	}

	public IReadOnlyList<IReadOnlyList<int>> Values
	{
		get => _values;
		set => Set(ref _values, value);
	}

	public IReadOnlyList<IReadOnlyList<CellModel>> Cells
	{
		get => _cells;
		set => Set(ref _cells, value);
	}

	public ICommand KeyDownCommand => _keyDownCommand ??= new RelayCommand<KeyEventArgs>(OnKeyDown);
	public ICommand SelectCellCommand => _selectCellCommand ??= new RelayCommand<CellModel>(OnSelectCell);
	public ICommand SetCellValueCommand => _setCellValueCommand ??= new RelayCommand<int>(OnSetCellValue);

	private readonly IMessenger _messenger;

	public FieldViewModel(IMessenger messenger)
	{
		_messenger = messenger;

		messenger.Register<SizeChangedMessage>(this, OnSizeChanged);
	}

	private void OnSizeChanged(SizeChangedMessage message)
	{
		RefreshField(message.NewSize);
	}

	public void RefreshField(int newSize)
	{
		Size = newSize;

		var sqrtOfSize = (int) Math.Sqrt(Size);
		Values = Enumerable.Range(0, sqrtOfSize)
			.Select(i => Enumerable.Range(0, sqrtOfSize)
				.Select(j => i * sqrtOfSize + j + 1)
				.ToArray())
			.ToArray();

		Cells = Enumerable.Range(1, Size)
			.Select(_ => Enumerable.Range(1, Size)
				.Select(_ => new CellModel { Value = null })
				.ToArray())
			.ToArray();
	}

	private void OnKeyDown(KeyEventArgs args)
	{
		if (SelectedCell == null || !KeyHelper.DigitKeys.TryGetValue(args.Key, out var value) || value > Size)
			return;

		SelectedCell.Value = value == 0 ? null : value;
		SelectedCell.IsMenuOpened = false;
		SelectedCell = null;
	}

	private void OnSelectCell(CellModel cell)
	{
		if (LockSelectedMenu)
			return;

		SelectedCell = cell;
		cell.IsMenuOpened = true;
	}

	private void OnSetCellValue(int value)
	{
		if (SelectedCell == null)
			return;

		SelectedCell.Value = value == 0 ? null : value;
		SelectedCell.IsMenuOpened = false;
		SelectedCell = null;
	}

	public override void Cleanup()
	{
		_messenger.Unregister(this);
		base.Cleanup();
	}
}