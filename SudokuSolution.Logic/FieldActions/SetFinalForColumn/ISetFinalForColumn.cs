using SudokuSolution.Domain.Entities;

namespace SudokuSolution.Logic.FieldActions.SetFinalForColumn {
	public interface ISetFinalForColumn {
		void Execute(Field field);
	}
}