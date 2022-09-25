using SudokuSolution.Domain.Entities;

namespace SudokuSolution.Logic.FieldActions.CleanPossible.CleanPossibleByFinal {
	public interface ICleanPossibleByFinal {
		void Execute(Field field);
		void Execute(Field field, int row, int column);
	}
}