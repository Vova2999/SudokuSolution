using System.Collections.Generic;
using SudokuSolution.Wpf.Common.Base;

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
	}
}