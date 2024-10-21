using System.Linq;
using SudokuSolution.Common.Extensions;
using SudokuSolution.Domain.Entities;
using SudokuSolution.Logic.Extensions;
using SudokuSolution.Logic.FieldActions.CleanPossible;

namespace SudokuSolution.Logic.FieldActions.SetFinal.SetFinalForRow;

public class SetFinalForRow : ISetFinalForRow
{
	private readonly ICleanPossibleFacade _cleanPossibleFacade;

	public SetFinalForRow(ICleanPossibleFacade cleanPossibleFacade)
	{
		_cleanPossibleFacade = cleanPossibleFacade;
	}

	public FieldActionsResult Execute(Field field)
	{
		return Enumerable.Range(0, field.MaxValue)
			.Select(row => ExecuteOneRow(field, row))
			.GetChangedResultIfAnyIsChanged();
	}

	private FieldActionsResult ExecuteOneRow(Field field, int row)
	{
		return Enumerable.Range(1, field.MaxValue)
			.Select(value => ExecuteOneRowOneValue(field, row, value))
			.GetChangedResultIfAnyIsChanged();
	}

	private FieldActionsResult ExecuteOneRowOneValue(Field field, int row, int value)
	{
		var skip = false;
		var lastIndex = -1;

		field.Cells.ForRow(row,
			(column, cell) =>
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
					if (lastIndex != -1)
					{
						skip = true;
						return;
					}

					lastIndex = column;
				}
			});

		if (skip || lastIndex == -1)
			return FieldActionsResult.Nothing;

		field.Cells[row, lastIndex].Final = value;
		_cleanPossibleFacade.Execute(field, row, lastIndex);
		return FieldActionsResult.Changed;
	}
}