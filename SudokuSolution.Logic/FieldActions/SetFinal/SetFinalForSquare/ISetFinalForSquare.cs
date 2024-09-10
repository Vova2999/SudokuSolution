using SudokuSolution.Domain.Entities;

namespace SudokuSolution.Logic.FieldActions.SetFinal.SetFinalForSquare;

public interface ISetFinalForSquare
{
	FieldActionsResult Execute(Field field);
}