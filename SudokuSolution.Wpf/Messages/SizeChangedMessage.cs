using GalaSoft.MvvmLight.Messaging;

namespace SudokuSolution.Wpf.Messages {
	public class SizeChangedMessage : MessageBase {
		public int NewSize { get; }

		public SizeChangedMessage(int newSize) {
			NewSize = newSize;
		}
	}
}