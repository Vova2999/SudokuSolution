using System;
using System.Linq;
using SudokuSolution.Common.Extensions;
using SudokuSolution.Domain.Entities;

namespace SudokuSolution.Logic.FieldService {
	public class FieldService : IFieldService {
		public bool IsSolved(Field field) {
			for (var row = 0; row < field.MaxValue; row++)
			for (var column = 0; column < field.MaxValue; column++) {
				if (!field.Cells[row, column].HasFinal)
					return false;
			}

			return true;
		}

		public bool IsFailed(Field field) {
			for (var row = 0; row < field.MaxValue; row++)
			for (var column = 0; column < field.MaxValue; column++) {
				if (field.Cells[row, column].HasFinal)
					continue;

				if (Enumerable.Range(1, field.MaxValue).All(value => !field.Cells[row, column][value]))
					return true;
			}

			for (var row = 0; row < field.MaxValue; row++) {
				var rowCells = field.Cells.SelectRow(row).ToArray();
				if (rowCells.Any(cell => !cell.HasFinal))
					continue;

				var rowValues = rowCells.Select(cell => cell.Final).OrderBy(value => value).ToArray();
				for (var value = 1; value <= field.MaxValue; value++) {
					if (rowValues[value - 1] != value)
						return true;
				}
			}

			for (var column = 0; column < field.MaxValue; column++) {
				var columnCells = field.Cells.SelectColumn(column).ToArray();
				if (columnCells.Any(cell => !cell.HasFinal))
					continue;

				var columnValues = columnCells.Select(cell => cell.Final).OrderBy(value => value).ToArray();
				for (var value = 1; value <= field.MaxValue; value++) {
					if (columnValues[value - 1] != value)
						return true;
				}
			}

			var squareSize = (int) Math.Sqrt(field.MaxValue);
			for (var squareRow = 0; squareRow < squareSize; squareRow++)
			for (var squareColumn = 0; squareColumn < squareSize; squareColumn++) {
				var squareCells = field.Cells.SelectSquare(squareSize, squareRow, squareColumn).ToArray();
				if (squareCells.Any(cell => !cell.HasFinal))
					continue;

				var squareValues = squareCells.Select(cell => cell.Final).OrderBy(value => value).ToArray();
				for (var value = 1; value <= field.MaxValue; value++) {
					if (squareValues[value - 1] != value)
						return true;
				}
			}

			return false;
		}
	}
}