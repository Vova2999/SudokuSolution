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
			var lastIndex = -1;

			for (var column = 0; column < field.MaxValue; column++) {
				if (field.Cells[row, column].HasFinal) {
					if (field.Cells[row, column].Final == value)
						return;

					continue;
				}

				if (field.Cells[row, column][value]) {
					if (lastIndex != -1)
						return;

					lastIndex = column;
				}
			}

			if (lastIndex != -1)
				field.Cells[row, lastIndex].Final = value;
		}
	}
}