using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace SudokuSolution.Common.Lock;

public class AsyncLock
{
	private readonly SemaphoreSlim _semaphoreSlim;
	private readonly LockReleaser _lockReleaser;

	public AsyncLock()
		:
		this(1)
	{
	}

	public AsyncLock(int concurrencyLevel)
	{
		_semaphoreSlim = new SemaphoreSlim(concurrencyLevel);
		_lockReleaser = new LockReleaser(_semaphoreSlim);
	}

	public async Task<LockReleaser> LockAsync(CancellationToken cancellationToken = default)
	{
		await _semaphoreSlim.WaitAsync(cancellationToken).ConfigureAwait(false);
		return _lockReleaser;
	}

	public TaskAwaiter<LockReleaser> GetAwaiter()
	{
		return LockAsync().GetAwaiter();
	}

	public sealed class LockReleaser : IDisposable
	{
		private readonly SemaphoreSlim _semaphoreSlim;

		internal LockReleaser(SemaphoreSlim semaphoreSlim)
		{
			_semaphoreSlim = semaphoreSlim;
		}

		void IDisposable.Dispose()
		{
			_semaphoreSlim.Release();
		}
	}
}