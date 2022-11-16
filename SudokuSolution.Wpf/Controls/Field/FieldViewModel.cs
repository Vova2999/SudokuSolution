using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using SudokuSolution.Wpf.Common.Base;
using SudokuSolution.Wpf.Messages;

namespace SudokuSolution.Wpf.Controls.Field {
	public class FieldViewModel : ViewModel<FieldControl> {
		public override object Header => string.Empty;

		private int size;
		private bool lockSelectedMenu;
		private CellModel selectedCell;
		private IReadOnlyList<IReadOnlyList<int>> values;
		private IReadOnlyList<IReadOnlyList<CellModel>> cells;

		private ICommand selectCellCommand;
		private ICommand setCellValueCommand;

		public int Size {
			get => size;
			set => Set(ref size, value);
		}

		public bool LockSelectedMenu {
			get => lockSelectedMenu;
			set => Set(ref lockSelectedMenu, value);
		}

		public CellModel SelectedCell {
			get => selectedCell;
			set => Set(ref selectedCell, value);
		}

		public IReadOnlyList<IReadOnlyList<int>> Values {
			get => values;
			set => Set(ref values, value);
		}

		public IReadOnlyList<IReadOnlyList<CellModel>> Cells {
			get => cells;
			set => Set(ref cells, value);
		}

		public ICommand SelectCellCommand => selectCellCommand ??= new RelayCommand<CellModel>(OnSelectCell);
		public ICommand SetCellValueCommand => setCellValueCommand ??= new RelayCommand<int>(OnSetCellValue);

		private readonly IMessenger messenger;

		public FieldViewModel(IMessenger messenger) {
			this.messenger = messenger;

			messenger.Register<SizeChangedMessage>(this, OnSizeChanged);

			RefreshField(Constants.StartedSize);
		}

		private void OnSizeChanged(SizeChangedMessage message) {
			RefreshField(message.NewSize);
		}

		public void RefreshField(int newSize) {
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

		private void OnSelectCell(CellModel cell) {
			if (LockSelectedMenu)
				return;

			SelectedCell = cell;
			cell.IsMenuOpened = true;
		}

		private void OnSetCellValue(int value) {
			SelectedCell.Value = value == 0 ? null : value;
			SelectedCell.IsMenuOpened = false;
		}
	}
}