using SudokuSolution.Domain.Entities;

namespace SudokuSolution.Logic.FieldActions.SetFinal.SetFinalForSinglePossible;

public interface ISetFinalForSinglePossible
{
	FieldActionsResult Execute(Field field);
}