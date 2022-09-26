using System;
using System.Linq;
using SudokuSolution.Common.Extensions;
using SudokuSolution.Domain.Entities;
using SudokuSolution.Logic.Extensions;

namespace SudokuSolution.Logic.FieldActions.CleanPossible.CleanPossibleByColumn {
	public class CleanPossibleByColumn : ICleanPossibleByColumn {
		public FieldActionsResult Execute(Field field) {
			var squareSize = (int) Math.Sqrt(field.MaxValue);
			return Enumerable.Range(0, squareSize)
				.SelectMany(squareRow => Enumerable.Range(0, squareSize)
					.Select(squareColumn => ExecuteOneSquare(field, squareSize, squareRow, squareColumn)))
				.GetChangedResultIfAnyIsChanged();
		}

		public FieldActionsResult Execute(Field field, int row, int column) {
			if (!field.Cells[row, column].HasFinal)
				throw new InvalidOperationException("Selected cell is not final");

			var value = field.Cells[row, column].Final;
			var squareSize = (int) Math.Sqrt(field.MaxValue);
			return ExecuteOneSquareOneValue(field, squareSize, row / squareSize, column / squareSize, value);
		}

		private static FieldActionsResult ExecuteOneSquare(Field field, int squareSize, int squareRow, int squareColumn) {
			return Enumerable.Range(1, field.MaxValue)
				.Select(value => ExecuteOneSquareOneValue(field, squareSize, squareRow, squareColumn, value))
				.GetChangedResultIfAnyIsChanged();
		}

		private static FieldActionsResult ExecuteOneSquareOneValue(Field field, int squareSize, int squareRow, int squareColumn, int value) {
			var skip = false;
			var hasValueInColumn = new bool[squareSize];

			field.Cells.ForSquare(
				squareSize,
				squareRow,
				squareColumn,
				(_, column, cell) => {
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
				return FieldActionsResult.Nothing;

			var singleColumn = -1;
			for (var column = 0; column < hasValueInColumn.Length; column++) {
				if (!hasValueInColumn[column])
					continue;

				if (singleColumn != -1)
					return FieldActionsResult.Nothing;

				singleColumn = column;
			}

			if (singleColumn == -1)
				return FieldActionsResult.Nothing;

			var result = FieldActionsResult.Nothing;
			var squareRowStart = squareSize * squareRow;
			var squareRowEnd = squareSize * (squareRow + 1);
			field.Cells.ForColumn(squareSize * squareColumn + singleColumn,
				(row, c) => {
					if (row >= squareRowStart && row < squareRowEnd)
						return;

					if (!c[value])
						return;

					c[value] = false;
					result = FieldActionsResult.Changed;
				});

			return result;
		}
	}
}