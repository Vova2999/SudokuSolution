using System;
using System.Threading;

namespace SudokuSolution.Common.Execution;

public class ExecutionTracker
{
	private readonly Action _onSuspend;
	private readonly Action _onResume;
	private bool _isBusy;
	private int _nestingLevel;
	private CancellationTokenSource _cancellationSource;

	public bool IsBusy => Volatile.Read(ref _isBusy);

	public ExecutionTracker()
	{
		_cancellationSource = new CancellationTokenSource();
	}

	public ExecutionTracker(Action onSuspend, Action onResume)
		: this()
	{
		_onSuspend = onSuspend;
		_onResume = onResume;
	}

	public ExecutionSuspenderContext TrackExecution()
	{
		return new ExecutionSuspenderContext(this, _cancellationSource.Token);
	}

	public ExecutionSuspenderContext TrackWithReset()
	{
		RecreateAndCancel();
		return new ExecutionSuspenderContext(this, _cancellationSource.Token);
	}

	private void RecreateAndCancel()
	{
		_cancellationSource.Cancel();
		_cancellationSource = new CancellationTokenSource();
		_nestingLevel = 0;
	}

	public class ExecutionSuspenderContext : IDisposable
	{
		private readonly ExecutionTracker _executionTracker;
		private readonly CancellationToken _token;

		public ExecutionSuspenderContext(ExecutionTracker executionTracker, CancellationToken token)
		{
			if (_token.IsCancellationRequested)
				return;

			_executionTracker = executionTracker;
			_token = token;

			_executionTracker._isBusy = true;

			if (Interlocked.Increment(ref _executionTracker._nestingLevel) == 1)
			{
				_executionTracker = executionTracker;

				_executionTracker._onSuspend?.Invoke();
			}
		}

		public void Dispose()
		{
			if (_token.IsCancellationRequested)
				return;

			if (Interlocked.Decrement(ref _executionTracker._nestingLevel) == 0)
			{
				_executionTracker._isBusy = false;
				_executionTracker._onResume?.Invoke();
			}
		}
	}
}