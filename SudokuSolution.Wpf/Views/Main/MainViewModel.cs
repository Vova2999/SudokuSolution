using SudokuSolution.Wpf.Common.Base;
using SudokuSolution.Wpf.Controls.Field;

namespace SudokuSolution.Wpf.Views.Main {
	public class MainViewModel : ViewModel<MainWindow> {
		public override object Header => string.Empty;

		public FieldViewModel FieldViewModel { get; private set; }

		public MainViewModel(FieldViewModel fieldViewModel) {
			FieldViewModel = fieldViewModel;
		}

		public override void Cleanup() {
			FieldViewModel?.Cleanup();
			FieldViewModel = null;
			base.Cleanup();
		}
	}
}