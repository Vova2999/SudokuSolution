using System.Windows;

namespace SudokuSolution.Wpf {
	public partial class App {
		private readonly IMainWindowProvider mainWindowProvider;

		public App(IMainWindowProvider mainWindowProvider) {
			this.mainWindowProvider = mainWindowProvider;
		}

		protected override void OnStartup(StartupEventArgs e) {
			base.OnStartup(e);

			mainWindowProvider.Show();
		}
	}
}