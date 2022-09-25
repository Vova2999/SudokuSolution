using SudokuSolution.Common.Extensions;
using SudokuSolution.Domain.Entities;
using SudokuSolution.Logic.FieldActions.CleanPossible;

namespace SudokuSolution.Logic.FieldActions.SetFinal.SetFinalForRow {
	public class SetFinalForRow : ISetFinalForRow {
		private readonly ICleanPossibleFacade cleanPossibleFacade;

		public SetFinalForRow(ICleanPossibleFacade cleanPossibleFacade) {
			this.cleanPossibleFacade = cleanPossibleFacade;
		}

		public void Execute(Field field) {
			for (var row = 0; row < field.MaxValue; row++)
				ExecuteOneRow(field, row);
		}

		private void ExecuteOneRow(Field field, int row) {
			for (var value = 1; value <= field.MaxValue; value++)
				ExecuteOneRowOneValue(field, row, value);
		}

		private void ExecuteOneRowOneValue(Field field, int row, int value) {
			var skip = false;
			var lastIndex = -1;

			field.Cells.ForRow(row,
				(column, cell) => {
					if (skip)
						return;

					if (cell.HasFinal) {
						if (cell.Final == value)
							skip = true;

						return;
					}

					if (cell[value]) {
						if (lastIndex != -1) {
							skip = true;
							return;
						}

						lastIndex = column;
					}
				});

			if (skip || lastIndex == -1)
				return;

			field.Cells[row, lastIndex].Final = value;
			cleanPossibleFacade.Execute(field, row, lastIndex);
		}
	}
}