using GalaSoft.MvvmLight;

namespace SudokuSolution.Wpf.Controls.Field {
	public class CellModel : ViewModelBase {
		private int? value;

		public int? Value {
			get => value;
			set => Set(ref this.value, value);
		}
	}
}