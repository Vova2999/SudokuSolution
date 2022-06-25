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
			var lastIndex = -1;

			for (var row = 0; row < field.MaxValue; row++) {
				if (field.Cells[row, column].HasFinal) {
					if (field.Cells[row, column].Final == value)
						return;

					continue;
				}

				if (field.Cells[row, column][value]) {
					if (lastIndex != -1)
						return;

					lastIndex = row;
				}
			}

			if (lastIndex != -1)
				field.Cells[lastIndex, column].Final = value;
		}
	}
}