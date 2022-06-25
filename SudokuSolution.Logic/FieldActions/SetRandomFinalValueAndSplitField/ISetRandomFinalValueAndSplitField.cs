using SudokuSolution.Domain.Entities;

namespace SudokuSolution.Logic.FieldActions.SetRandomFinalValueAndSplitField {
	public interface ISetRandomFinalValueAndSplitField {
		Field[] Execute(Field field);
	}
}