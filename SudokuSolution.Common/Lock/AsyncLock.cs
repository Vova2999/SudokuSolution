using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace SudokuSolution.Common.Lock {
	public class AsyncLock {
		private readonly SemaphoreSlim semaphoreSlim;
		private readonly LockReleaser lockReleaser;

		public AsyncLock()
			:
			this(1) {
		}

		public AsyncLock(int concurrencyLevel) {
			semaphoreSlim = new SemaphoreSlim(concurrencyLevel);
			lockReleaser = new LockReleaser(semaphoreSlim);
		}

		public async Task<LockReleaser> LockAsync(CancellationToken cancellationToken = default) {
			await semaphoreSlim.WaitAsync(cancellationToken).ConfigureAwait(false);
			return lockReleaser;
		}

		public TaskAwaiter<LockReleaser> GetAwaiter() {
			return LockAsync().GetAwaiter();
		}

		public sealed class LockReleaser : IDisposable {
			private readonly SemaphoreSlim semaphoreSlim;

			internal LockReleaser(SemaphoreSlim semaphoreSlim) {
				this.semaphoreSlim = semaphoreSlim;
			}

			void IDisposable.Dispose() {
				semaphoreSlim.Release();
			}
		}
	}
}