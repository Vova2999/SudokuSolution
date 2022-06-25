using SudokuSolution.Common.Extensions;
using SudokuSolution.Domain.Entities;

namespace SudokuSolution.Logic.FieldActions.CleanPossibleValues {
	public class CleanPossibleValues : ICleanPossibleValues {
		public void Execute(Field field) {
			field.Cells.ForEach((row, column, cell) => {
				if (!cell.HasFinal)
					return;

				var cellFinal = cell.Final;
				field.Cells.GetRow(row).ForEach(c => c[cellFinal] = false);
				field.Cells.GetColumn(column).ForEach(c => c[cellFinal] = false);
			});
		}
	}
}