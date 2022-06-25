using SudokuSolution.Domain.Entities;

namespace SudokuSolution.Logic.FieldFactory {
	public interface IFieldFactory {
		Field Create(int size);
	}
}