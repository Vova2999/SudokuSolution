using SudokuSolution.Domain.Entities;

namespace SudokuSolution.Logic.FieldActions.SetFinalForSinglePossible {
	public interface ISetFinalForSinglePossible {
		void Execute(Field field);
	}
}