using SudokuSolution.Wpf.Views.Solved;

namespace SudokuSolution.Wpf.Views.Main;

public class DemoModel : MainViewModel
{
	public DemoModel() : base(
		new Controls.Field.DemoModel(),
		new Controls.Settings.DemoModel(),
		DemoLocator.Locate<SolvedViewModel.IFactory>())
	{
		IsSettingsOpened = false;
	}
}