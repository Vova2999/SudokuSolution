using SudokuSolution.Domain.Entities;

namespace SudokuSolution.Logic.FieldActions.SetRandomFinalAndSplitField {
	public interface ISetRandomFinalAndSplitField {
		Field[] Execute(Field field);
	}
}