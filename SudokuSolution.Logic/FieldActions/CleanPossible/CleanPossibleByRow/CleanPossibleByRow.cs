using System;
using System.Linq;
using SudokuSolution.Common.Extensions;
using SudokuSolution.Domain.Entities;
using SudokuSolution.Logic.Extensions;

namespace SudokuSolution.Logic.FieldActions.CleanPossible.CleanPossibleByRow {
	public class CleanPossibleByRow : ICleanPossibleByRow {
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
				return FieldActionsResult.Nothing;

			var singleRow = -1;
			for (var row = 0; row < hasValueInRow.Length; row++) {
				if (!hasValueInRow[row])
					continue;

				if (singleRow != -1)
					return FieldActionsResult.Nothing;

				singleRow = row;
			}

			if (singleRow == -1)
				return FieldActionsResult.Nothing;

			var result = FieldActionsResult.Nothing;
			var squareColumnStart = squareSize * squareColumn;
			var squareColumnEnd = squareSize * (squareColumn + 1);
			field.Cells.ForRow(squareSize * squareRow + singleRow,
				(column, c) => {
					if (column >= squareColumnStart && column < squareColumnEnd)
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