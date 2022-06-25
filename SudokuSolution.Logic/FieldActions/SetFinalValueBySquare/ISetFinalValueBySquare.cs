using SudokuSolution.Domain.Entities;

namespace SudokuSolution.Logic.FieldActions.SetFinalValueBySquare {
	public interface ISetFinalValueBySquare {
		void Execute(Field field);
	}
}