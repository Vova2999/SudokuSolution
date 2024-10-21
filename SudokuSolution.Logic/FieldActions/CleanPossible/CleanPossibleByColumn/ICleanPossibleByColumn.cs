using SudokuSolution.Domain.Entities;

namespace SudokuSolution.Logic.FieldActions.CleanPossible.CleanPossibleByColumn;

public interface ICleanPossibleByColumn
{
	FieldActionsResult Execute(Field field);
	FieldActionsResult Execute(Field field, int row, int column);
}