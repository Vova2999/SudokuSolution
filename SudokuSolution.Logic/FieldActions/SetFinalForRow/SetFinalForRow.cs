using SudokuSolution.Common.Extensions;
using SudokuSolution.Domain.Entities;

namespace SudokuSolution.Logic.FieldActions.SetFinalForRow {
	public class SetFinalForRow : ISetFinalForRow {
		public void Execute(Field field) {
			for (var row = 0; row < field.MaxValue; row++)
				ExecuteOneRow(field, row);
		}

		private static void ExecuteOneRow(Field field, int row) {
			for (var value = 1; value <= field.MaxValue; value++)
				ExecuteOneRowOneValue(field, row, value);
		}

		private static void ExecuteOneRowOneValue(Field field, int row, int value) {
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

			if (skip)
				return;

			if (lastIndex != -1)
				field.Cells[row, lastIndex].Final = value;
		}
	}
}