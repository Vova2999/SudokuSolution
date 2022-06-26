using SudokuSolution.Domain.Entities;

namespace SudokuSolution.Logic.FieldService {
	public interface IFieldService {
		bool IsSolved(Field field);
		bool IsFailed(Field field);
	}
}