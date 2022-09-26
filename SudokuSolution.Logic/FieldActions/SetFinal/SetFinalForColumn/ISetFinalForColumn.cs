using SudokuSolution.Domain.Entities;

namespace SudokuSolution.Logic.FieldActions.SetFinal.SetFinalForColumn {
	public interface ISetFinalForColumn {
		FieldActionsResult Execute(Field field);
	}
}