using System;
using SudokuSolution.Common.Extensions;
using SudokuSolution.Domain.Entities;

namespace SudokuSolution.Logic.FieldActions.SetFinalForSquare {
	public class SetFinalForSquare : ISetFinalForSquare {
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
			var lastRowIndex = -1;
			var lastColumnIndex = -1;

			field.Cells.ForSquare(
				squareSize,
				squareRow,
				squareColumn,
				(row, column, cell) => {
					if (skip)
						return;

					if (field.Cells[row, column].HasFinal) {
						if (field.Cells[row, column].Final == value)
							skip = true;

						return;
					}

					if (field.Cells[row, column][value]) {
						if (lastRowIndex != -1) {
							skip = true;
							return;
						}

						lastRowIndex = row;
						lastColumnIndex = column;
					}
				});

			if (!skip && lastRowIndex != -1)
				field.Cells[lastRowIndex, lastColumnIndex].Final = value;
		}
	}
}