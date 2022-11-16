using System.Windows.Input;
using GalaSoft.MvvmLight.CommandWpf;
using SudokuSolution.Wpf.Common.Base;
using SudokuSolution.Wpf.Controls.Field;
using SudokuSolution.Wpf.Controls.Settings;

namespace SudokuSolution.Wpf.Views.Main {
	public class MainViewModel : ViewModel<MainWindow> {
		public override object Header => string.Empty;

		private bool isSettingsOpened;

		private ICommand openSettingsCommand;
		private ICommand closeSettingsCommand;

		public bool IsSettingsOpened {
			get => isSettingsOpened;
			set => Set(ref isSettingsOpened, value);
		}

		public FieldViewModel FieldViewModel { get; private set; }
		public SettingsViewModel SettingsViewModel { get; private set; }

		public ICommand OpenSettingsCommand => openSettingsCommand ??= new RelayCommand(OnOpenSettings);
		public ICommand CloseSettingsCommand => closeSettingsCommand ??= new RelayCommand(OnCloseSettings);

		public MainViewModel(FieldViewModel fieldViewModel,
							 SettingsViewModel settingsViewModel) {
			FieldViewModel = fieldViewModel;
			SettingsViewModel = settingsViewModel;
		}

		private void OnOpenSettings() {
			IsSettingsOpened = true;
		}

		private void OnCloseSettings() {
			IsSettingsOpened = false;
		}

		public override void Cleanup() {
			FieldViewModel?.Cleanup();
			FieldViewModel = null;
			SettingsViewModel?.Cleanup();
			SettingsViewModel = null;
			base.Cleanup();
		}
	}
}