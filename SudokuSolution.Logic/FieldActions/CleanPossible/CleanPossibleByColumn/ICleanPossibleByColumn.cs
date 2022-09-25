using SudokuSolution.Domain.Entities;

namespace SudokuSolution.Logic.FieldActions.CleanPossible.CleanPossibleByColumn {
	public interface ICleanPossibleByColumn {
		void Execute(Field field);
		void Execute(Field field, int row, int column);
	}
}