using System;
using System.Collections.Generic;
using System.Linq;
using SudokuSolution.Common.Extensions;
using SudokuSolution.Domain.Entities;

namespace SudokuSolution.Logic.FieldActions.SetFinalForSquare {
	public class SetFinalForSquare : ISetFinalForSquare {
		public void Execute(Field field) {
			GetSquareIndexes(field).ForEach(indexes => ExecuteOneSquare(field, indexes));
		}

		private static IEnumerable<IEnumerable<(int Row, int Column)>> GetSquareIndexes(Field field) {
			var squareSize = (int) Math.Sqrt(field.MaxValue);
			for (var row = 0; row < squareSize; row += squareSize)
			for (var column = 0; column < squareSize; column += squareSize)
				yield return GetSquareIndexes(squareSize, row, column);
		}

		private static IEnumerable<(int Row, int Column)> GetSquareIndexes(int squareSize, int rowStart, int columnStart) {
			for (var row = 0; row < squareSize; row++)
			for (var column = 0; column < squareSize; column++)
				yield return (row + rowStart, column + columnStart);
		}

		private static void ExecuteOneSquare(Field field, IEnumerable<(int Row, int Column)> indexes) {
			var indexesArray = indexes.ToArray();
			for (var value = 1; value <= field.MaxValue; value++)
				ExecuteOneSquareOneValue(field, indexesArray, value);
		}

		private static void ExecuteOneSquareOneValue(Field field, IEnumerable<(int Row, int Column)> indexes, int value) {
			var lastRowIndex = -1;
			var lastColumnIndex = -1;

			foreach (var (row, column) in indexes) {
				if (field.Cells[row, column].HasFinal) {
					if (field.Cells[row, column].Final == value)
						return;

					continue;
				}

				if (field.Cells[row, column][value]) {
					if (lastRowIndex != -1)
						return;

					lastRowIndex = row;
					lastColumnIndex = column;
				}
			}

			if (lastRowIndex != -1)
				field.Cells[lastRowIndex, lastColumnIndex].Final = value;
		}
	}
}