using System;
using System.Collections.Generic;

namespace SudokuSolution.Common.Extensions {
	public static class EnumerableExtensions {
		public static void ForEach<TValue>(this TValue[,] values, Action<TValue> action) {
			for (var row = 0; row < values.GetLength(0); row++)
			for (var column = 0; column < values.GetLength(1); column++)
				action(values[row, column]);
		}

		public static void ForEach<TValue, TResult>(this TValue[,] values, Func<TValue, TResult> action) {
			for (var row = 0; row < values.GetLength(0); row++)
			for (var column = 0; column < values.GetLength(1); column++)
				action(values[row, column]);
		}

		public static void ForEach<TValue>(this TValue[,] values, Action<int, int, TValue> action) {
			for (var row = 0; row < values.GetLength(0); row++)
			for (var column = 0; column < values.GetLength(1); column++)
				action(row, column, values[row, column]);
		}

		public static void ForEach<TValue, TResult>(this TValue[,] values, Func<int, int, TValue, TResult> action) {
			for (var row = 0; row < values.GetLength(0); row++)
			for (var column = 0; column < values.GetLength(1); column++)
				action(row, column, values[row, column]);
		}

		public static void ForEach<TValue>(this IEnumerable<TValue> values, Action<TValue> action) {
			foreach (var value in values)
				action(value);
		}

		public static void ForEach<TValue, TResult>(this IEnumerable<TValue> values, Func<TValue, TResult> action) {
			foreach (var value in values)
				action(value);
		}

		public static void ForEach<TValue>(this IEnumerable<TValue> values, Action<int, TValue> action) {
			var index = 0;
			foreach (var value in values)
				action(index++, value);
		}

		public static void ForEach<TValue, TResult>(this IEnumerable<TValue> values, Func<int, TValue, TResult> action) {
			var index = 0;
			foreach (var value in values)
				action(index++, value);
		}

		public static IEnumerable<TValue> GetRow<TValue>(this TValue[,] values, int row) {
			for (var column = 0; column < values.GetLength(1); column++)
				yield return values[row, column];
		}

		public static IEnumerable<TValue> GetColumn<TValue>(this TValue[,] values, int column) {
			for (var row = 0; row < values.GetLength(0); row++)
				yield return values[row, column];
		}
	}
}