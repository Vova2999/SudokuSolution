using System.Collections.Generic;
using System.Threading.Tasks;
using SudokuSolution.Common.Lock;
using SudokuSolution.Domain.Entities;

namespace SudokuSolution.Logic.GameService;

public class FieldEnumeratorAsync
{
	public Field Current { get; private set; }

	private readonly IGameService _gameService;
	private readonly Field _field;

	private readonly AsyncLock _asyncLock;

	private IEnumerator<Field> _fieldEnumerator;

	public FieldEnumeratorAsync(IGameService gameService, Field field)
	{
		_gameService = gameService;
		_field = field;

		_asyncLock = new AsyncLock();
	}

	public Task<bool> MoveNextAsync()
	{
		var taskCompletionSource = new TaskCompletionSource<bool>();

		Task.Run(async () =>
		{
			using (await _asyncLock.LockAsync().ConfigureAwait(false))
			{
				_fieldEnumerator ??= _gameService.Solve(_field).GetEnumerator();

				var moveNextResult = _fieldEnumerator.MoveNext();
				Current = moveNextResult ? _fieldEnumerator.Current : null;

				taskCompletionSource.SetResult(moveNextResult);
			}
		});

		return taskCompletionSource.Task;
	}
}