using SudokuSolution.Domain.Entities;

namespace SudokuSolution.Logic.FieldActions.CleanPossible {
	public interface ICleanPossibleFacade {
		void Execute(Field field);
		void Execute(Field field, int row, int column);
	}
}