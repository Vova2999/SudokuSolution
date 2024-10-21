using System;
using System.Linq;
using SudokuSolution.Common.Extensions;
using SudokuSolution.Domain.Entities;

namespace SudokuSolution.Logic.FieldService;

public class FieldService : IFieldService
{
	public bool IsSolved(Field field)
	{
		for (var row = 0; row < field.MaxValue; row++)
		for (var column = 0; column < field.MaxValue; column++)
		{
			if (!field.Cells[row, column].HasFinal)
				return false;
		}

		return true;
	}

	public bool IsFailed(Field field)
	{
		for (var row = 0; row < field.MaxValue; row++)
		for (var column = 0; column < field.MaxValue; column++)
		{
			if (IsEmptyCell(field.Cells[row, column], field.MaxValue))
				return true;
		}

		for (var row = 0; row < field.MaxValue; row++)
		{
			var rowCells = field.Cells.SelectRow(row).ToArray();
			if (IsFailedCellsOrder(rowCells, field.MaxValue))
				return true;
		}

		for (var column = 0; column < field.MaxValue; column++)
		{
			var columnCells = field.Cells.SelectColumn(column).ToArray();
			if (IsFailedCellsOrder(columnCells, field.MaxValue))
				return true;
		}

		var squareSize = (int) Math.Sqrt(field.MaxValue);
		for (var squareRow = 0; squareRow < squareSize; squareRow++)
		for (var squareColumn = 0; squareColumn < squareSize; squareColumn++)
		{
			var squareCells = field.Cells.SelectSquare(squareSize, squareRow, squareColumn).ToArray();
			if (IsFailedCellsOrder(squareCells, field.MaxValue))
				return true;
		}

		return false;
	}

	private static bool IsEmptyCell(Cell cell, int maxValue)
	{
		return !cell.HasFinal && Enumerable.Range(1, maxValue).All(value => !cell[value]);
	}

	private static bool IsFailedCellsOrder(Cell[] rowCells, int maxValue)
	{
		if (rowCells.Any(cell => !cell.HasFinal))
			return false;

		var rowValues = rowCells.Select(cell => cell.Final).OrderBy(value => value).ToArray();
		for (var value = 1; value <= maxValue; value++)
		{
			if (rowValues[value - 1] != value)
				return true;
		}

		return false;
	}
}