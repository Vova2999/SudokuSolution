using SudokuSolution.Domain.Entities;

namespace SudokuSolution.Logic.FieldActions.CleanPossibleByColumn {
	public interface ICleanPossibleByColumn {
		void Execute(Field field);
	}
}