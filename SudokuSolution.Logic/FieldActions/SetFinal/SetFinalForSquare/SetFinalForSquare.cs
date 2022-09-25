using System;
using SudokuSolution.Common.Extensions;
using SudokuSolution.Domain.Entities;
using SudokuSolution.Logic.FieldActions.CleanPossible;

namespace SudokuSolution.Logic.FieldActions.SetFinal.SetFinalForSquare {
	public class SetFinalForSquare : ISetFinalForSquare {
		private readonly ICleanPossibleFacade cleanPossibleFacade;

		public SetFinalForSquare(ICleanPossibleFacade cleanPossibleFacade) {
			this.cleanPossibleFacade = cleanPossibleFacade;
		}

		public void Execute(Field field) {
			var squareSize = (int) Math.Sqrt(field.MaxValue);
			for (var squareRow = 0; squareRow < squareSize; squareRow++)
			for (var squareColumn = 0; squareColumn < squareSize; squareColumn++)
				ExecuteOneSquare(field, squareSize, squareRow, squareColumn);
		}

		private void ExecuteOneSquare(Field field, int squareSize, int squareRow, int squareColumn) {
			for (var value = 1; value <= field.MaxValue; value++)
				ExecuteOneSquareOneValue(field, squareSize, squareRow, squareColumn, value);
		}

		private void ExecuteOneSquareOneValue(Field field, int squareSize, int squareRow, int squareColumn, int value) {
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

					if (cell.HasFinal) {
						if (cell.Final == value)
							skip = true;

						return;
					}

					if (cell[value]) {
						if (lastRowIndex != -1) {
							skip = true;
							return;
						}

						lastRowIndex = row;
						lastColumnIndex = column;
					}
				});

			if (skip || lastRowIndex == -1)
				return;

			field.Cells[lastRowIndex, lastColumnIndex].Final = value;
			cleanPossibleFacade.Execute(field, lastRowIndex, lastColumnIndex);
		}
	}
}