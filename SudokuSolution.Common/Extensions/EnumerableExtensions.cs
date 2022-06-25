using System;
using System.Collections.Generic;

namespace SudokuSolution.Common.Extensions {
	public static class EnumerableExtensions {
		public static void ForEach<TValue>(this IEnumerable<TValue> values, Action<TValue> action) {
			foreach (var value in values)
				action(value);
		}

		public static void ForEach<TValue, TResult>(this IEnumerable<TValue> values, Func<TValue, TResult> action) {
			foreach (var value in values)
				action(value);
		}
	}
}