using System.Linq;
using SudokuSolution.Domain.Entities;
using SudokuSolution.Logic.Extensions;
using SudokuSolution.Logic.FieldActions.SetFinal.SetFinalForColumn;
using SudokuSolution.Logic.FieldActions.SetFinal.SetFinalForRow;
using SudokuSolution.Logic.FieldActions.SetFinal.SetFinalForSinglePossible;
using SudokuSolution.Logic.FieldActions.SetFinal.SetFinalForSquare;

namespace SudokuSolution.Logic.FieldActions.SetFinal {
	public class SetFinalFacade : ISetFinalFacade {
		private readonly ISetFinalForRow setFinalForRow;
		private readonly ISetFinalForColumn setFinalForColumn;
		private readonly ISetFinalForSquare setFinalForSquare;
		private readonly ISetFinalForSinglePossible setFinalForSinglePossible;

		public SetFinalFacade(ISetFinalForRow setFinalForRow,
							  ISetFinalForColumn setFinalForColumn,
							  ISetFinalForSquare setFinalForSquare,
							  ISetFinalForSinglePossible setFinalForSinglePossible) {
			this.setFinalForRow = setFinalForRow;
			this.setFinalForColumn = setFinalForColumn;
			this.setFinalForSquare = setFinalForSquare;
			this.setFinalForSinglePossible = setFinalForSinglePossible;
		}

		public FieldActionsResult Execute(Field field) {
			return new[] {
				setFinalForSinglePossible.Execute(field),
				setFinalForSquare.Execute(field),
				setFinalForRow.Execute(field),
				setFinalForColumn.Execute(field)
			}.GetChangedResultIfAnyIsChanged();
		}
	}
}