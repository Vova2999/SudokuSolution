using System.Windows;
using SudokuSolution.Wpf.Views.Main.Logic;

namespace SudokuSolution.Wpf;

public partial class App
{
	private readonly IMainWindowProvider _mainWindowProvider;

	public App(IMainWindowProvider mainWindowProvider)
	{
		_mainWindowProvider = mainWindowProvider;

		InitializeComponent();
	}

	protected override void OnStartup(StartupEventArgs e)
	{
		base.OnStartup(e);

		_mainWindowProvider.Show();
	}
}