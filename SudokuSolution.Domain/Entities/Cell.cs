namespace SudokuSolution.Domain.Entities {
	public class Cell {
		public int? FinalValue { get; set; }
		public bool[] PossibleValues { get; }

		public Cell(bool[] possibleValues) {
			PossibleValues = possibleValues;
		}
	}
}