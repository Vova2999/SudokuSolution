using System;
using SudokuSolution.Common.Extensions;
using SudokuSolution.Domain.Entities;

namespace SudokuSolution.Logic.FieldActions.CleanPossible.CleanPossibleByFinal {
	public class CleanPossibleByFinal : ICleanPossibleByFinal {
		public void Execute(Field field) {
			var squareSize = (int) Math.Sqrt(field.MaxValue);
			field.Cells.ForEach((row, column, cell) => {
				if (!cell.HasFinal)
					return;

				ExecuteOneValue(field, cell, row, column, squareSize);
			});
		}

		public void Execute(Field field, int row, int column) {
			if (!field.Cells[row, column].HasFinal)
				throw new InvalidOperationException("Selected cell is not final");

			var squareSize = (int) Math.Sqrt(field.MaxValue);
			ExecuteOneValue(field, field.Cells[row, column], row, column, squareSize);
		}

		private static void ExecuteOneValue(Field field, Cell cell, int row, int column, int squareSize) {
			var cellFinal = cell.Final;
			field.Cells.ForRow(row, c => c[cellFinal] = false);
			field.Cells.ForColumn(column, c => c[cellFinal] = false);
			field.Cells.ForSquare(squareSize, row / squareSize, column / squareSize, c => c[cellFinal] = false);
		}
	}
}