using System;
using System.Windows.Threading;

namespace SudokuSolution.Wpf.Common.Dispatcher {
	public interface IDispatcherHelper {
		System.Windows.Threading.Dispatcher UiDispatcher { get; }

		void CheckBeginInvokeOnUI(Action action);
		DispatcherOperation RunAsync(Action action);
		void Reset();
	}
}