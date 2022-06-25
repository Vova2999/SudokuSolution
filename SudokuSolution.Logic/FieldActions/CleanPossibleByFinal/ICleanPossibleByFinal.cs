using SudokuSolution.Domain.Entities;

namespace SudokuSolution.Logic.FieldActions.CleanPossibleByFinal {
	public interface ICleanPossibleByFinal {
		void Execute(Field field);
	}
}