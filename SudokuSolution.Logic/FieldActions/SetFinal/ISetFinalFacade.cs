using SudokuSolution.Domain.Entities;

namespace SudokuSolution.Logic.FieldActions.SetFinal;

public interface ISetFinalFacade
{
	FieldActionsResult Execute(Field field);
}