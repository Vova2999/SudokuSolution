using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace SudokuSolution.Common.Extensions {
	[PublicAPI]
	public static class EnumerableExtensions {
		public static void ForEach<TValue>(this TValue[,] values, Action<TValue> action) {
			for (var row = 0; row < values.GetLength(0); row++)
			for (var column = 0; column < values.GetLength(1); column++)
				action(values[row, column]);
		}

		public static void ForEach<TValue>(this TValue[,] values, Action<int, int, TValue> action) {
			for (var row = 0; row < values.GetLength(0); row++)
			for (var column = 0; column < values.GetLength(1); column++)
				action(row, column, values[row, column]);
		}

		public static void ForEach<TValue>(this IEnumerable<TValue> values, Action<TValue> action) {
			foreach (var value in values)
				action(value);
		}

		public static void ForEach<TValue>(this IEnumerable<TValue> values, Action<int, TValue> action) {
			var index = 0;
			foreach (var value in values)
				action(index++, value);
		}

		public static IEnumerable<TValue> AsEnumerable<TValue>(this TValue item) {
			yield return item;
		}

		public static void ForRow<TValue>(this TValue[,] values, int row, Action<TValue> action) {
			for (var column = 0; column < values.GetLength(1); column++)
				action(values[row, column]);
		}

		public static void ForRow<TValue>(this TValue[,] values, int row, Action<int, TValue> action) {
			for (var column = 0; column < values.GetLength(1); column++)
				action(column, values[row, column]);
		}

		public static void ForColumn<TValue>(this TValue[,] values, int column, Action<TValue> action) {
			for (var row = 0; row < values.GetLength(0); row++)
				action(values[row, column]);
		}

		public static void ForColumn<TValue>(this TValue[,] values, int column, Action<int, TValue> action) {
			for (var row = 0; row < values.GetLength(0); row++)
				action(row, values[row, column]);
		}

		public static void ForSquare<TValue>(this TValue[,] values, int squareSize, int squareRow, int squareColumn, Action<TValue> action) {
			var rowStart = squareSize * squareRow;
			var columnStart = squareSize * squareColumn;
			for (var row = 0; row < squareSize; row++)
			for (var column = 0; column < squareSize; column++)
				action(values[rowStart + row, columnStart + column]);
		}

		public static void ForSquare<TValue>(this TValue[,] values, int squareSize, int squareRow, int squareColumn, Action<int, int, TValue> action) {
			var rowStart = squareSize * squareRow;
			var columnStart = squareSize * squareColumn;
			for (var row = 0; row < squareSize; row++)
			for (var column = 0; column < squareSize; column++)
				action(rowStart + row, columnStart + column, values[rowStart + row, columnStart + column]);
		}
	}
}