using System.Linq;
using SudokuSolution.Common.Extensions;
using SudokuSolution.Domain.Entities;

namespace SudokuSolution.Logic.GameFactory {
	public class GameFactory : IGameFactory {
		public Game Create(int size) {
			var totalValue = size * size;
			var game = new Game(new Field(new Cell[totalValue, totalValue]));
			Enumerable.Range(0, size)
				.ForEach(i => Enumerable.Range(0, size)
					.ForEach(j => game.Field.Cells[i, j] = new Cell(new bool[totalValue])));

			return game;
		}
	}
}