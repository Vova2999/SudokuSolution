using SudokuSolution.Domain.Entities;

namespace SudokuSolution.Logic.FieldActions.CleanPossible.CleanPossibleByRow {
	public interface ICleanPossibleByRow {
		void Execute(Field field);
		void Execute(Field field, int row, int column);
	}
}