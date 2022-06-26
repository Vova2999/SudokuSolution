using System;

namespace SudokuSolution.Domain.Entities {
	public class Field : ICloneable {
		public int MaxValue { get; }
		public Cell[,] Cells { get; }

		private Field(int maxValue, Func<int, int, Cell> createCell) {
			MaxValue = maxValue;
			Cells = new Cell[maxValue, maxValue];

			for (var row = 0; row < maxValue; row++)
			for (var column = 0; column < maxValue; column++)
				Cells[row, column] = createCell(row, column);
		}

		public Field(int maxValue) : this(maxValue, (_, _) => new Cell(maxValue)) {
			MaxValue = maxValue;
			Cells = new Cell[maxValue, maxValue];

			for (var row = 0; row < maxValue; row++)
			for (var column = 0; column < maxValue; column++)
				Cells[row, column] = new Cell(maxValue);
		}

		public object Clone() {
			return new Field(MaxValue, (row, column) => (Cell) Cells[row, column].Clone());
		}

		public override bool Equals(object obj) {
			return obj is Field field && Equals(field);
		}

		public bool Equals(Field field) {
			if (MaxValue != field.MaxValue)
				return false;

			for (var row = 0; row < MaxValue; row++)
			for (var column = 0; column < MaxValue; column++) {
				if (!Cells[row, column].Equals(field.Cells[row, column]))
					return false;
			}

			return true;
		}

		public override int GetHashCode() {
			return 0;
		}
	}
}