using SudokuSolution.Domain.Entities;

namespace SudokuSolution.Logic.GameService {
	public interface IGameService {
		void Solve(Game game);
	}
}