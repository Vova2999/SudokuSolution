using SudokuSolution.Common.Extensions;
using SudokuSolution.Domain.Entities;
using SudokuSolution.Logic.FieldActions.CleanPossible;

namespace SudokuSolution.Logic.FieldActions.SetFinal.SetFinalForSinglePossible {
	public class SetFinalForSinglePossible : ISetFinalForSinglePossible {
		private readonly ICleanPossibleFacade cleanPossibleFacade;

		public SetFinalForSinglePossible(ICleanPossibleFacade cleanPossibleFacade) {
			this.cleanPossibleFacade = cleanPossibleFacade;
		}

		public FieldActionsResult Execute(Field field) {
			var result = FieldActionsResult.Nothing;
			field.Cells.ForEach((row, column, cell) => {
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

				if (lastValue == -1)
					return;

				cell.Final = lastValue;
				cleanPossibleFacade.Execute(field, row, column);

				result = FieldActionsResult.Changed;
			});

			return result;
		}
	}
}