using SudokuSolution.Common.Extensions;
using SudokuSolution.Domain.Entities;

namespace SudokuSolution.Logic.FieldActions.SetFinalForSinglePossible {
	public class SetFinalForSinglePossible : ISetFinalForSinglePossible {
		public void Execute(Field field) {
			field.Cells.ForEach(cell => {
				if (cell.HasFinal)
					return;

				var lastValue = -1;
				for (var value = 1; value <= field.MaxValue; value++) {
					if (!cell[value])
						continue;

					if (lastValue != -1)
						return;

					lastValue = value;
				}

				if (lastValue != -1)
					cell.Final = lastValue;
			});
		}
	}
}