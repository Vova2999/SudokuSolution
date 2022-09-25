using SudokuSolution.Domain.Entities;

namespace SudokuSolution.Logic.FieldActions.SetFinal {
	public interface ISetFinalFacade {
		void Execute(Field field);
	}
}