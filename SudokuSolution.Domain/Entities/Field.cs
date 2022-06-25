namespace SudokuSolution.Domain.Entities {
	public class Field {
		public int MaxValue { get; }
		public Cell[,] Cells { get; }

		public Field(int maxValue) {
			MaxValue = maxValue;
			Cells = new Cell[maxValue, maxValue];

			for (var row = 0; row < maxValue; row++)
			for (var column = 0; column < maxValue; column++)
				Cells[row, column].SetMaxValue(maxValue);
		}
	}
}