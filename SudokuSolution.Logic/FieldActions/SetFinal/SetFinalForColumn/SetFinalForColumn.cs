using SudokuSolution.Common.Extensions;
using SudokuSolution.Domain.Entities;
using SudokuSolution.Logic.FieldActions.CleanPossible;

namespace SudokuSolution.Logic.FieldActions.SetFinal.SetFinalForColumn {
	public class SetFinalForColumn : ISetFinalForColumn {
		private readonly ICleanPossibleFacade cleanPossibleFacade;

		public SetFinalForColumn(ICleanPossibleFacade cleanPossibleFacade) {
			this.cleanPossibleFacade = cleanPossibleFacade;
		}

		public void Execute(Field field) {
			for (var column = 0; column < field.MaxValue; column++)
				ExecuteOneColumn(field, column);
		}

		private void ExecuteOneColumn(Field field, int column) {
			for (var value = 1; value <= field.MaxValue; value++)
				ExecuteOneColumnOneValue(field, column, value);
		}

		private void ExecuteOneColumnOneValue(Field field, int column, int value) {
			var skip = false;
			var lastIndex = -1;

			field.Cells.ForColumn(column,
				(row, cell) => {
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

						lastIndex = row;
					}
				});

			if (skip || lastIndex == -1)
				return;

			field.Cells[lastIndex, column].Final = value;
			cleanPossibleFacade.Execute(field, lastIndex, column);
		}
	}
}