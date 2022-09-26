using SudokuSolution.Domain.Entities;

namespace SudokuSolution.Logic.FieldActions.CleanPossible.CleanPossibleByRow {
	public interface ICleanPossibleByRow {
		FieldActionsResult Execute(Field field);
		FieldActionsResult Execute(Field field, int row, int column);
	}
}