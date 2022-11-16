using GalaSoft.MvvmLight.Messaging;

namespace SudokuSolution.Wpf.Controls.Settings {
	public class DemoModel : SettingsViewModel {
		public DemoModel() : base(Locator.Current.Locate<IMessenger>()) {
			Size = Constants.StartedSize;
		}
	}
}