using GalaSoft.MvvmLight.Messaging;
using SudokuSolution.Wpf.Common.Base;
using SudokuSolution.Wpf.Messages;

namespace SudokuSolution.Wpf.Controls.Settings {
	public class SettingsViewModel : ViewModel<SettingsControl> {
		public override object Header => string.Empty;

		private int size;
		private int[] allowedSizes;

		public int Size {
			get => size;
			set {
				if (Set(ref size, value))
					OnSizeChanged();
			}
		}

		public int[] AllowedSizes {
			get => allowedSizes;
			set => Set(ref allowedSizes, value);
		}

		private readonly IMessenger messenger;

		public SettingsViewModel(IMessenger messenger) {
			this.messenger = messenger;

			AllowedSizes = Constants.AllowedSizes;
			Size = Constants.StartedSize;
		}

		private void OnSizeChanged() {
			messenger.Send(new SizeChangedMessage(Size));
		}

		public override void Cleanup() {
			messenger.Unregister(this);
			base.Cleanup();
		}
	}
}