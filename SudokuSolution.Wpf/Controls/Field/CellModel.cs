using GalaSoft.MvvmLight;

namespace SudokuSolution.Wpf.Controls.Field {
	public class CellModel : ViewModelBase {
		private int? value;
		private bool isBoldFont;
		private bool isMenuOpened;

		public int? Value {
			get => value;
			set => Set(ref this.value, value);
		}

		public bool IsBoldFont {
			get => isBoldFont;
			set => Set(ref isBoldFont, value);
		}

		public bool IsMenuOpened {
			get => isMenuOpened;
			set => Set(ref isMenuOpened, value);
		}
	}
}