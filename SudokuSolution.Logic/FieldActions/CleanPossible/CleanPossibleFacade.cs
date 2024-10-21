using SudokuSolution.Domain.Entities;
using SudokuSolution.Logic.Extensions;
using SudokuSolution.Logic.FieldActions.CleanPossible.CleanPossibleByColumn;
using SudokuSolution.Logic.FieldActions.CleanPossible.CleanPossibleByFinal;
using SudokuSolution.Logic.FieldActions.CleanPossible.CleanPossibleByRow;

namespace SudokuSolution.Logic.FieldActions.CleanPossible;

public class CleanPossibleFacade : ICleanPossibleFacade
{
	private readonly ICleanPossibleByRow cleanPossibleByRow;
	private readonly ICleanPossibleByFinal cleanPossibleByFinal;
	private readonly ICleanPossibleByColumn cleanPossibleByColumn;

	public CleanPossibleFacade(ICleanPossibleByRow cleanPossibleByRow,
							   ICleanPossibleByFinal cleanPossibleByFinal,
							   ICleanPossibleByColumn cleanPossibleByColumn)
	{
		this.cleanPossibleByRow = cleanPossibleByRow;
		this.cleanPossibleByFinal = cleanPossibleByFinal;
		this.cleanPossibleByColumn = cleanPossibleByColumn;
	}

	public FieldActionsResult Execute(Field field)
	{
		return new[]
		{
			cleanPossibleByFinal.Execute(field),
			cleanPossibleByRow.Execute(field),
			cleanPossibleByColumn.Execute(field)
		}.GetChangedResultIfAnyIsChanged();
	}

	public FieldActionsResult Execute(Field field, int row, int column)
	{
		return new[]
		{
			cleanPossibleByFinal.Execute(field, row, column),
			cleanPossibleByRow.Execute(field, row, column),
			cleanPossibleByColumn.Execute(field, row, column)
		}.GetChangedResultIfAnyIsChanged();
	}
}