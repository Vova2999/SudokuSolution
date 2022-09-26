using System;
using SudokuSolution.Common.Extensions;
using SudokuSolution.Domain.Entities;

namespace SudokuSolution.Logic.FieldActions.CleanPossible.CleanPossibleByFinal {
	public class CleanPossibleByFinal : ICleanPossibleByFinal {
		public FieldActionsResult Execute(Field field) {
			var result = FieldActionsResult.Nothing;
			var squareSize = (int) Math.Sqrt(field.MaxValue);
			field.Cells.ForEach((row, column, cell) => {
				if (!cell.HasFinal)
					return;

				if (ExecuteOneValue(field, cell, row, column, squareSize) == FieldActionsResult.Changed)
					result = FieldActionsResult.Changed;
			});

			return result;
		}

		public FieldActionsResult Execute(Field field, int row, int column) {
			if (!field.Cells[row, column].HasFinal)
				throw new InvalidOperationException("Selected cell is not final");

			var squareSize = (int) Math.Sqrt(field.MaxValue);
			return ExecuteOneValue(field, field.Cells[row, column], row, column, squareSize);
		}

		private static FieldActionsResult ExecuteOneValue(Field field, Cell cell, int row, int column, int squareSize) {
			var value = cell.Final;
			var result = FieldActionsResult.Nothing;
			field.Cells.ForRow(row, c => RemovePossibleIfNeeded(c, value, ref result));
			field.Cells.ForColumn(column, c => RemovePossibleIfNeeded(c, value, ref result));
			field.Cells.ForSquare(squareSize, row / squareSize, column / squareSize, c => RemovePossibleIfNeeded(c, value, ref result));
			return result;
		}

		private static void RemovePossibleIfNeeded(Cell cell, int value, ref FieldActionsResult result) {
			if (!cell[value])
				return;

			cell[value] = false;
			result = FieldActionsResult.Changed;
		}
	}
}