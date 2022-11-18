using SudokuSolution.Logic.GameService;
using SudokuSolution.Wpf.Common.Dispatcher;
using SudokuSolution.Wpf.Common.MessageBox;

namespace SudokuSolution.Wpf.Views.Solved {
	public class DemoModel : SolvedViewModel {
		public DemoModel() : base(
			DemoLocator.Locate<IGameService>(),
			DemoLocator.Locate<IDispatcherHelper>(),
			DemoLocator.Locate<IMessageBoxService>(),
			new Controls.Field.DemoModel(),
			null,
			0,
			false) {
			CurrentSolved = 5;
			TotalSolvedCount = 20;
		}
	}
}