using SudokuSolution.Domain.Entities;
using SudokuSolution.Logic.Extensions;
using SudokuSolution.Logic.FieldActions.SetFinal.SetFinalForColumn;
using SudokuSolution.Logic.FieldActions.SetFinal.SetFinalForRow;
using SudokuSolution.Logic.FieldActions.SetFinal.SetFinalForSinglePossible;
using SudokuSolution.Logic.FieldActions.SetFinal.SetFinalForSquare;

namespace SudokuSolution.Logic.FieldActions.SetFinal;

public class SetFinalFacade : ISetFinalFacade
{
	private readonly ISetFinalForRow _setFinalForRow;
	private readonly ISetFinalForColumn _setFinalForColumn;
	private readonly ISetFinalForSquare _setFinalForSquare;
	private readonly ISetFinalForSinglePossible _setFinalForSinglePossible;

	public SetFinalFacade(
		ISetFinalForRow setFinalForRow,
		ISetFinalForColumn setFinalForColumn,
		ISetFinalForSquare setFinalForSquare,
		ISetFinalForSinglePossible setFinalForSinglePossible)
	{
		_setFinalForRow = setFinalForRow;
		_setFinalForColumn = setFinalForColumn;
		_setFinalForSquare = setFinalForSquare;
		_setFinalForSinglePossible = setFinalForSinglePossible;
	}

	public FieldActionsResult Execute(Field field)
	{
		return new[]
		{
			_setFinalForSinglePossible.Execute(field),
			_setFinalForSquare.Execute(field),
			_setFinalForRow.Execute(field),
			_setFinalForColumn.Execute(field)
		}.GetChangedResultIfAnyIsChanged();
	}
}