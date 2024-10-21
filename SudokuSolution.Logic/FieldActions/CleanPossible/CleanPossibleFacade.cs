using SudokuSolution.Domain.Entities;
using SudokuSolution.Logic.Extensions;
using SudokuSolution.Logic.FieldActions.CleanPossible.CleanPossibleByColumn;
using SudokuSolution.Logic.FieldActions.CleanPossible.CleanPossibleByFinal;
using SudokuSolution.Logic.FieldActions.CleanPossible.CleanPossibleByRow;

namespace SudokuSolution.Logic.FieldActions.CleanPossible;

public class CleanPossibleFacade : ICleanPossibleFacade
{
	private readonly ICleanPossibleByRow _cleanPossibleByRow;
	private readonly ICleanPossibleByFinal _cleanPossibleByFinal;
	private readonly ICleanPossibleByColumn _cleanPossibleByColumn;

	public CleanPossibleFacade(
		ICleanPossibleByRow cleanPossibleByRow,
		ICleanPossibleByFinal cleanPossibleByFinal,
		ICleanPossibleByColumn cleanPossibleByColumn)
	{
		_cleanPossibleByRow = cleanPossibleByRow;
		_cleanPossibleByFinal = cleanPossibleByFinal;
		_cleanPossibleByColumn = cleanPossibleByColumn;
	}

	public FieldActionsResult Execute(Field field)
	{
		return new[]
		{
			_cleanPossibleByFinal.Execute(field),
			_cleanPossibleByRow.Execute(field),
			_cleanPossibleByColumn.Execute(field)
		}.GetChangedResultIfAnyIsChanged();
	}

	public FieldActionsResult Execute(Field field, int row, int column)
	{
		return new[]
		{
			_cleanPossibleByFinal.Execute(field, row, column),
			_cleanPossibleByRow.Execute(field, row, column),
			_cleanPossibleByColumn.Execute(field, row, column)
		}.GetChangedResultIfAnyIsChanged();
	}
}