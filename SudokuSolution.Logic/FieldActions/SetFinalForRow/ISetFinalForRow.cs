using SudokuSolution.Domain.Entities;

namespace SudokuSolution.Logic.FieldActions.SetFinalForRow {
	public interface ISetFinalForRow {
		void Execute(Field field);
	}
}