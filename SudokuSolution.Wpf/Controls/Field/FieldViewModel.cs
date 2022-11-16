using System.Collections.Generic;
using System.Linq;
using GalaSoft.MvvmLight.Messaging;
using SudokuSolution.Wpf.Common.Base;
using SudokuSolution.Wpf.Messages;

namespace SudokuSolution.Wpf.Controls.Field {
	public class FieldViewModel : ViewModel<FieldControl> {
		public override object Header => string.Empty;

		private int size;
		private IReadOnlyList<IReadOnlyList<CellModel>> cells;

		public int Size {
			get => size;
			set => Set(ref size, value);
		}

		public IReadOnlyList<IReadOnlyList<CellModel>> Cells {
			get => cells;
			set => Set(ref cells, value);
		}

		private readonly IMessenger messenger;

		public FieldViewModel(IMessenger messenger) {
			this.messenger = messenger;

			messenger.Register<SizeChangedMessage>(this, OnSizeChanged);

			RefreshField(Constants.StartedSize);
		}

		private void OnSizeChanged(SizeChangedMessage message) {
			RefreshField(message.NewSize);
		}

		private void RefreshField(int newSize) {
			Size = newSize;
			Cells = Enumerable.Range(1, Size * Size)
				.Select(_ => Enumerable.Range(1, Size * Size)
					.Select(_ => new CellModel { Value = null })
					.ToArray())
				.ToArray();
		}
	}
}