namespace SudokuSolution.Domain.Entities {
	public class Field {
		public Cell[,] Cells { get; }

		public Field(int maxValue) {
			Cells = new Cell[maxValue, maxValue];

			for (var i = 0; i < maxValue; i++)
			for (var j = 0; j < maxValue; j++)
				Cells[i, j].SetMaxValue(maxValue);
		}
	}
}