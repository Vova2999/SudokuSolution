using SudokuSolution.Domain.Entities;

namespace SudokuSolution.Logic.FieldActions.CleanPossible.CleanPossibleByFinal;

public interface ICleanPossibleByFinal
{
	FieldActionsResult Execute(Field field);
	FieldActionsResult Execute(Field field, int row, int column);
}