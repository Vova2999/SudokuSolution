using SudokuSolution.Domain.Entities;

namespace SudokuSolution.Logic.FieldActions.SetFinal.SetFinalForSinglePossible {
	public interface ISetFinalForSinglePossible {
		void Execute(Field field);
	}
}