using SudokuSolution.Domain.Entities;
using SudokuSolution.Logic.FieldActions.CleanPossible.CleanPossibleByColumn;
using SudokuSolution.Logic.FieldActions.CleanPossible.CleanPossibleByFinal;
using SudokuSolution.Logic.FieldActions.CleanPossible.CleanPossibleByRow;

namespace SudokuSolution.Logic.FieldActions.CleanPossible {
	public class CleanPossibleFacade : ICleanPossibleFacade {
		private readonly ICleanPossibleByRow cleanPossibleByRow;
		private readonly ICleanPossibleByFinal cleanPossibleByFinal;
		private readonly ICleanPossibleByColumn cleanPossibleByColumn;

		public CleanPossibleFacade(ICleanPossibleByRow cleanPossibleByRow,
								   ICleanPossibleByFinal cleanPossibleByFinal,
								   ICleanPossibleByColumn cleanPossibleByColumn) {
			this.cleanPossibleByRow = cleanPossibleByRow;
			this.cleanPossibleByFinal = cleanPossibleByFinal;
			this.cleanPossibleByColumn = cleanPossibleByColumn;
		}

		public void Execute(Field field) {
			cleanPossibleByFinal.Execute(field);
			cleanPossibleByRow.Execute(field);
			cleanPossibleByColumn.Execute(field);
		}

		public void Execute(Field field, int row, int column) {
			cleanPossibleByFinal.Execute(field, row, column);
			cleanPossibleByRow.Execute(field, row, column);
			cleanPossibleByColumn.Execute(field, row, column);
		}
	}
}