namespace SudokuSolution.Domain.Entities {
	public class Field {
		public Cell[,] Cells { get; }

		public Field(Cell[,] cells) {
			Cells = cells;
		}
	}
}