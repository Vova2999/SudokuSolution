using System;
using SudokuSolution.Common.Extensions;
using SudokuSolution.Domain.Entities;

namespace SudokuSolution.Logic.FieldActions.CleanPossibleByFinal {
	public class CleanPossibleByFinal : ICleanPossibleByFinal {
		public void Execute(Field field) {
			var squareSize = (int) Math.Sqrt(field.MaxValue);
			field.Cells.ForEach((row, column, cell) => {
				if (!cell.HasFinal)
					return;

				var cellFinal = cell.Final;
				field.Cells.ForRow(row, c => c[cellFinal] = false);
				field.Cells.ForColumn(column, c => c[cellFinal] = false);
				field.Cells.ForSquare(squareSize, row / squareSize, column / squareSize, c => c[cellFinal] = false);
			});
		}
	}
}