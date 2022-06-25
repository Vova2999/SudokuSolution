namespace SudokuSolution.Domain.Entities {
	public class Game {
		public Field Field { get; }

		public Game(Field field) {
			Field = field;
		}
	}
}