using System;
using SudokuSolution.Common.Extensions;
using SudokuSolution.Domain.Entities;

namespace SudokuSolution.Logic.FieldActions.CleanPossibleByColumn {
	public class CleanPossibleByColumn : ICleanPossibleByColumn {
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
			var hasValueInColumn = new bool[squareSize];

			field.Cells.ForSquare(
				squareSize,
				squareRow,
				squareColumn,
				(row, column, cell) => {
					if (skip)
						return;

					if (cell.HasFinal) {
						if (cell.Final == value)
							skip = true;

						return;
					}

					if (cell[value])
						hasValueInColumn[column % squareSize] = true;
				});

			if (skip)
				return;

			var singleColumn = -1;
			for (var column = 0; column < hasValueInColumn.Length; column++) {
				if (!hasValueInColumn[column])
					continue;

				if (singleColumn != -1)
					return;

				singleColumn = column;
			}

			var squareRowStart = squareSize * squareRow;
			var squareRowEnd = squareSize * (squareRow + 1);
			field.Cells.ForColumn(squareSize * squareColumn + singleColumn,
				(row, c) => {
					if (row < squareRowStart || row >= squareRowEnd)
						c[value] = false;
				});
		}
	}
}