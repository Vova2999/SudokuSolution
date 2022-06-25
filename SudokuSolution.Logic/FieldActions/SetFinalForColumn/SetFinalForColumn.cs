using SudokuSolution.Common.Extensions;
using SudokuSolution.Domain.Entities;

namespace SudokuSolution.Logic.FieldActions.SetFinalForColumn {
	public class SetFinalForColumn : ISetFinalForColumn {
		public void Execute(Field field) {
			for (var column = 0; column < field.MaxValue; column++)
				ExecuteOneColumn(field, column);
		}

		private static void ExecuteOneColumn(Field field, int column) {
			for (var value = 1; value <= field.MaxValue; value++)
				ExecuteOneColumnOneValue(field, column, value);
		}

		private static void ExecuteOneColumnOneValue(Field field, int column, int value) {
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

			if (skip)
				return;

			if (lastIndex != -1)
				field.Cells[lastIndex, column].Final = value;
		}
	}
}