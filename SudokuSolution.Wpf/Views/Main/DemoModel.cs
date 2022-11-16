using SudokuSolution.Logic.GameService;
using SudokuSolution.Wpf.Views.Solved;

namespace SudokuSolution.Wpf.Views.Main {
	public class DemoModel : MainViewModel {
		public DemoModel() : base(
			Locator.Current.Locate<IGameService>(),
			new Controls.Field.DemoModel(),
			new Controls.Settings.DemoModel(),
			Locator.Current.Locate<SolvedViewModel.IFactory>()) {
			IsSettingsOpened = false;
		}
	}
}