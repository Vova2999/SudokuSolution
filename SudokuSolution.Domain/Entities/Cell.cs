namespace SudokuSolution.Domain.Entities {
	public class Cell {
		public int FinalValue { get; set; }
		public bool[] PossibleValues { get; set; }
	}
}