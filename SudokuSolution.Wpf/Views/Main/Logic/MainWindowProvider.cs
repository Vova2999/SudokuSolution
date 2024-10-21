using System.ComponentModel;
using System.Windows;
using GalaSoft.MvvmLight;
using SudokuSolution.Wpf.Common.Dispatcher;
using SudokuSolution.Wpf.Common.View;

namespace SudokuSolution.Wpf.Views.Main.Logic;

public class MainWindowProvider : IMainWindowProvider
{
	private readonly IViewService viewService;
	private readonly IDispatcherHelper dispatcherHelper;

	private Window mainWindow;

	public MainWindowProvider(IViewService viewService, IDispatcherHelper dispatcherHelper)
	{
		this.viewService = viewService;
		this.dispatcherHelper = dispatcherHelper;
	}

	public void Show()
	{
		dispatcherHelper.CheckBeginInvokeOnUI(() =>
		{
			mainWindow ??= CreateWindow();
			Application.Current.MainWindow = mainWindow;
			mainWindow.Show();
		});
	}

	public void CloseIfCreated()
	{
		dispatcherHelper.CheckBeginInvokeOnUI(() => mainWindow?.Close());
	}

	private Window CreateWindow()
	{
		var window = viewService.CreateWindow<MainViewModel>(WindowMode.Window);
		window.Closing += OnWindowClosing;
		return window;
	}

	private void OnWindowClosing(object sender, CancelEventArgs e)
	{
		mainWindow.Closing -= OnWindowClosing;
		(mainWindow.DataContext as ICleanup)?.Cleanup();
		mainWindow = null;
	}
}