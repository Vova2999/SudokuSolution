using SudokuSolution.Domain.Entities;

namespace SudokuSolution.Logic.FieldActions.CleanPossibleValues {
	public interface ICleanPossibleValues {
		void Execute(Field field);
	}
}