using System.Collections.Generic;
using SudokuSolution.Domain.Entities;

namespace SudokuSolution.Logic.FieldActions.SetRandomFinalAndSplitField {
	public interface ISetRandomFinalAndSplitField {
		IEnumerable<Field> Execute(Field field);
	}
}