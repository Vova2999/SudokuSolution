using System;
using System.Threading.Tasks;

namespace SudokuSolution.Common.Extensions;

public static class TaskExtensions
{
	public static async void FireAndForgetSafeAsync(this Task task)
	{
		try
		{
			await task.ConfigureAwait(false);
		}
		catch (Exception)
		{
			// ignored
		}
	}
}