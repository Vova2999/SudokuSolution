using System;
using System.Threading;

namespace SudokuSolution.Common.Execution {
    public class ExecutionTracker {
        private readonly Action onSuspend;
        private readonly Action onResume;
        private bool isBusy;
        private int nestingLevel;
        private CancellationTokenSource cancellationSource;

        public bool IsBusy => Volatile.Read(ref isBusy);

        public ExecutionTracker() {
            cancellationSource = new CancellationTokenSource();
        }

        public ExecutionTracker(Action onSuspend, Action onResume)
            : this() {
            this.onSuspend = onSuspend;
            this.onResume = onResume;
        }

        public ExecutionSuspenderContext TrackExecution() {
            return new ExecutionSuspenderContext(this, cancellationSource.Token);
        }

        public ExecutionSuspenderContext TrackWithReset() {
            RecreateAndCancel();
            return new ExecutionSuspenderContext(this, cancellationSource.Token);
        }

        private void RecreateAndCancel() {
            cancellationSource.Cancel();
            cancellationSource = new CancellationTokenSource();
            nestingLevel = 0;
        }

        public class ExecutionSuspenderContext : IDisposable {
            private readonly ExecutionTracker executionTracker;
            private readonly CancellationToken token;

            public ExecutionSuspenderContext(ExecutionTracker executionTracker, CancellationToken token) {
                if (this.token.IsCancellationRequested)
                    return;

                this.executionTracker = executionTracker;
                this.token = token;

                this.executionTracker.isBusy = true;

                if (Interlocked.Increment(ref this.executionTracker.nestingLevel) == 1) {
                    this.executionTracker = executionTracker;

                    this.executionTracker.onSuspend?.Invoke();
                }
            }

            public void Dispose() {
                if (token.IsCancellationRequested)
                    return;

                if (Interlocked.Decrement(ref executionTracker.nestingLevel) == 0) {
                    executionTracker.isBusy = false;
                    executionTracker.onResume?.Invoke();
                }
            }
        }
    }
}