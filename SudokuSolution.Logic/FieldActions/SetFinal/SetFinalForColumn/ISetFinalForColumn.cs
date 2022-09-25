using SudokuSolution.Domain.Entities;

namespace SudokuSolution.Logic.FieldActions.SetFinal.SetFinalForColumn {
	public interface ISetFinalForColumn {
		void Execute(Field field);
	}
}