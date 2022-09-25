using SudokuSolution.Domain.Entities;

namespace SudokuSolution.Logic.FieldActions.SetFinal.SetFinalForSquare {
	public interface ISetFinalForSquare {
		void Execute(Field field);
	}
}