using SudokuSolution.Domain.Entities;

namespace SudokuSolution.Logic.FieldActions.SetFinalForSquare {
	public interface ISetFinalForSquare {
		void Execute(Field field);
	}
}