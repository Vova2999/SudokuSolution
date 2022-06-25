using SudokuSolution.Domain.Entities;

namespace SudokuSolution.Logic.GameFactory {
	public interface IGameFactory {
		Game Create(int size);
	}
}