using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using SudokuSolution.Common.Extensions;

namespace SudokuSolution.Wpf.Common.Commands {
	public class AsyncRelayCommand<T> : ICommand {
		public event EventHandler CanExecuteChanged {
			add => CommandManager.RequerySuggested += value;
			remove => CommandManager.RequerySuggested -= value;
		}

		private readonly Func<T, Task> execute;
		private readonly Func<T, bool> canExecute;

		private long isExecuting;

		public AsyncRelayCommand(Func<T, Task> execute, Func<T, bool> canExecute = null) {
			this.execute = execute ?? throw new ArgumentNullException(nameof(execute));
			this.canExecute = canExecute;
		}

		public void RaiseCanExecuteChanged() {
			CommandManager.InvalidateRequerySuggested();
		}

		public bool CanExecute(object parameter) {
			if (canExecute == null)
				return true;

			if (Interlocked.Read(ref isExecuting) != 0)
				return false;

			if (parameter == null)
				return canExecute(default);

			if (parameter.GetType() != typeof(T) && parameter is IConvertible)
				return canExecute((T) Convert.ChangeType(parameter, typeof(T), null));

			if (parameter is T t)
				return canExecute(t);

			return false;
		}

		public void Execute(object parameter) {
			ExecuteAsync(parameter).FireAndForgetSafeAsync();
		}

		private async Task ExecuteAsync(object parameter) {
			if (!CanExecute(parameter))
				return;

			Interlocked.Exchange(ref isExecuting, 1);
			RaiseCanExecuteChanged();

			try {
				if (parameter == null)
					await execute(default).ConfigureAwait(false);
				else if (parameter.GetType() != typeof(T) && parameter is IConvertible)
					await execute((T) Convert.ChangeType(parameter, typeof(T), null)).ConfigureAwait(false);
				else if (parameter is T t)
					await execute(t).ConfigureAwait(false);
			}
			finally {
				Interlocked.Exchange(ref isExecuting, 0);
				RaiseCanExecuteChanged();
			}
		}
	}
}