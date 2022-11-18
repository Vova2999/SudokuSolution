using System.Collections.Generic;
using System.Threading.Tasks;
using SudokuSolution.Common.Lock;
using SudokuSolution.Domain.Entities;

namespace SudokuSolution.Logic.GameService {
	public class FieldEnumeratorAsync {
		public Field Current { get; private set; }

		private readonly IGameService gameService;
		private readonly Field field;

		private readonly AsyncLock asyncLock;

		private IEnumerator<Field> fieldEnumerator;

		public FieldEnumeratorAsync(IGameService gameService, Field field) {
			this.gameService = gameService;
			this.field = field;

			asyncLock = new AsyncLock();
		}

		public Task<bool> MoveNextAsync() {
			var taskCompletionSource = new TaskCompletionSource<bool>();

			Task.Run(async () => {
				using (await asyncLock.LockAsync().ConfigureAwait(false)) {
					fieldEnumerator ??= gameService.Solve(field).GetEnumerator();

					var moveNextResult = fieldEnumerator.MoveNext();
					Current = moveNextResult ? fieldEnumerator.Current : null;

					taskCompletionSource.SetResult(moveNextResult);
				}
			});

			return taskCompletionSource.Task;
		}
	}
}