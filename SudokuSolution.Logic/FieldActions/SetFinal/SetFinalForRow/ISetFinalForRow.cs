using SudokuSolution.Domain.Entities;

namespace SudokuSolution.Logic.FieldActions.SetFinal.SetFinalForRow;

public interface ISetFinalForRow
{
	FieldActionsResult Execute(Field field);
}