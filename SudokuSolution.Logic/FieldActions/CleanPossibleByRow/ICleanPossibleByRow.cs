using SudokuSolution.Domain.Entities;

namespace SudokuSolution.Logic.FieldActions.CleanPossibleByRow {
	public interface ICleanPossibleByRow {
		void Execute(Field field);
	}
}