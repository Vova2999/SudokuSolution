using SudokuSolution.Domain.Entities;

namespace SudokuSolution.Logic.FieldActions.CleanPossible;

public interface ICleanPossibleFacade
{
	FieldActionsResult Execute(Field field);
	FieldActionsResult Execute(Field field, int row, int column);
}