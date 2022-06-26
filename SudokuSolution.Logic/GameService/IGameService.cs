using SudokuSolution.Domain.Entities;

namespace SudokuSolution.Logic.GameService {
	public interface IGameService {
		Field[] Solve(Field field);
	}
}