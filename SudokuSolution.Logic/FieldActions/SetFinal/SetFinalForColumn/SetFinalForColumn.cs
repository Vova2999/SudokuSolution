using System.Linq;
using SudokuSolution.Common.Extensions;
using SudokuSolution.Domain.Entities;
using SudokuSolution.Logic.Extensions;
using SudokuSolution.Logic.FieldActions.CleanPossible;

namespace SudokuSolution.Logic.FieldActions.SetFinal.SetFinalForColumn {
	public class SetFinalForColumn : ISetFinalForColumn {
		private readonly ICleanPossibleFacade cleanPossibleFacade;

		public SetFinalForColumn(ICleanPossibleFacade cleanPossibleFacade) {
			this.cleanPossibleFacade = cleanPossibleFacade;
		}

		public FieldActionsResult Execute(Field field) {
			return Enumerable.Range(0, field.MaxValue)
				.Select(column => ExecuteOneColumn(field, column))
				.GetChangedResultIfAnyIsChanged();
		}

		private FieldActionsResult ExecuteOneColumn(Field field, int column) {
			return Enumerable.Range(1, field.MaxValue)
				.Select(value => ExecuteOneColumnOneValue(field, column, value))
				.GetChangedResultIfAnyIsChanged();
		}

		private FieldActionsResult ExecuteOneColumnOneValue(Field field, int column, int value) {
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
				return FieldActionsResult.Nothing;

			field.Cells[lastIndex, column].Final = value;
			cleanPossibleFacade.Execute(field, lastIndex, column);
			return FieldActionsResult.Changed;
		}
	}
}