using System;
using SudokuSolution.Common.Extensions;
using SudokuSolution.Domain.Entities;

namespace SudokuSolution.Logic.FieldActions.CleanPossibleByRow {
	public class CleanPossibleByRow : ICleanPossibleByRow {
		public void Execute(Field field) {
			var squareSize = (int) Math.Sqrt(field.MaxValue);
			for (var squareRow = 0; squareRow < squareSize; squareRow++)
			for (var squareColumn = 0; squareColumn < squareSize; squareColumn++)
				ExecuteOneSquare(field, squareSize, squareRow, squareColumn);
		}

		private static void ExecuteOneSquare(Field field, int squareSize, int squareRow, int squareColumn) {
			for (var value = 1; value <= field.MaxValue; value++)
				ExecuteOneSquareOneValue(field, squareSize, squareRow, squareColumn, value);
		}

		private static void ExecuteOneSquareOneValue(Field field, int squareSize, int squareRow, int squareColumn, int value) {
			var skip = false;
			var hasValueInRow = new bool[squareSize];

			field.Cells.ForSquare(
				squareSize,
				squareRow,
				squareColumn,
				(row, _, cell) => {
					if (skip)
						return;

					if (cell.HasFinal) {
						if (cell.Final == value)
							skip = true;

						return;
					}

					if (cell[value])
						hasValueInRow[row % squareSize] = true;
				});

			if (skip)
				return;

			var singleRow = -1;
			for (var row = 0; row < hasValueInRow.Length; row++) {
				if (!hasValueInRow[row])
					continue;

				if (singleRow != -1)
					return;

				singleRow = row;
			}

			var squareColumnStart = squareSize * squareColumn;
			var squareColumnEnd = squareSize * (squareColumn + 1);
			field.Cells.ForRow(squareSize * squareRow + singleRow,
				(column, c) => {
					if (column < squareColumnStart || column >= squareColumnEnd)
						c[value] = false;
				});
		}
	}
}