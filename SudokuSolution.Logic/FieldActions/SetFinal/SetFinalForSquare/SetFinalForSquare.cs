using System;
using System.Linq;
using SudokuSolution.Common.Extensions;
using SudokuSolution.Domain.Entities;
using SudokuSolution.Logic.Extensions;
using SudokuSolution.Logic.FieldActions.CleanPossible;

namespace SudokuSolution.Logic.FieldActions.SetFinal.SetFinalForSquare;

public class SetFinalForSquare : ISetFinalForSquare
{
	private readonly ICleanPossibleFacade cleanPossibleFacade;

	public SetFinalForSquare(ICleanPossibleFacade cleanPossibleFacade)
	{
		this.cleanPossibleFacade = cleanPossibleFacade;
	}

	public FieldActionsResult Execute(Field field)
	{
		var squareSize = (int) Math.Sqrt(field.MaxValue);
		return Enumerable.Range(0, squareSize)
			.SelectMany(squareRow => Enumerable.Range(0, squareSize)
				.Select(squareColumn => ExecuteOneSquare(field, squareSize, squareRow, squareColumn)))
			.GetChangedResultIfAnyIsChanged();
	}

	private FieldActionsResult ExecuteOneSquare(Field field, int squareSize, int squareRow, int squareColumn)
	{
		return Enumerable.Range(1, field.MaxValue)
			.Select(value => ExecuteOneSquareOneValue(field, squareSize, squareRow, squareColumn, value))
			.GetChangedResultIfAnyIsChanged();
	}

	private FieldActionsResult ExecuteOneSquareOneValue(Field field, int squareSize, int squareRow, int squareColumn, int value)
	{
		var skip = false;
		var lastRowIndex = -1;
		var lastColumnIndex = -1;

		field.Cells.ForSquare(
			squareSize,
			squareRow,
			squareColumn,
			(row, column, cell) =>
			{
				if (skip)
					return;

				if (cell.HasFinal)
				{
					if (cell.Final == value)
						skip = true;

					return;
				}

				if (cell[value])
				{
					if (lastRowIndex != -1)
					{
						skip = true;
						return;
					}

					lastRowIndex = row;
					lastColumnIndex = column;
				}
			});

		if (skip || lastRowIndex == -1)
			return FieldActionsResult.Nothing;

		field.Cells[lastRowIndex, lastColumnIndex].Final = value;
		cleanPossibleFacade.Execute(field, lastRowIndex, lastColumnIndex);
		return FieldActionsResult.Changed;
	}
}