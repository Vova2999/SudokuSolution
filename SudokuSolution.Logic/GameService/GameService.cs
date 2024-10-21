using System.Collections.Generic;
using System.Linq;
using SudokuSolution.Common.Extensions;
using SudokuSolution.Domain.Entities;
using SudokuSolution.Logic.FieldActions;
using SudokuSolution.Logic.FieldActions.CleanPossible;
using SudokuSolution.Logic.FieldActions.SetFinal;
using SudokuSolution.Logic.FieldActions.SetRandomFinalAndSplitField;
using SudokuSolution.Logic.FieldService;

namespace SudokuSolution.Logic.GameService;

public class GameService : IGameService
{
	private readonly IFieldService _fieldService;
	private readonly ISetFinalFacade _setFinalFacade;
	private readonly ICleanPossibleFacade _cleanPossibleFacade;
	private readonly ISetRandomFinalAndSplitField _setRandomFinalAndSplitField;

	public GameService(
		IFieldService fieldService,
		ISetFinalFacade setFinalFacade,
		ICleanPossibleFacade cleanPossibleFacade,
		ISetRandomFinalAndSplitField setRandomFinalAndSplitField)
	{
		_fieldService = fieldService;
		_setFinalFacade = setFinalFacade;
		_cleanPossibleFacade = cleanPossibleFacade;
		_setRandomFinalAndSplitField = setRandomFinalAndSplitField;
	}

	public IEnumerable<Field> Solve(Field field)
	{
		return SolveWithChangeField((Field) field.Clone());
	}

	public FieldEnumeratorAsync StartSolve(Field field)
	{
		return new FieldEnumeratorAsync(this, field);
	}

	private IEnumerable<Field> SolveWithChangeField(Field field)
	{
		var withoutRandomResult = TrySolveWithoutRandom(field);
		if (withoutRandomResult.HasValue)
			return withoutRandomResult.Value ? field.AsEnumerable() : Enumerable.Empty<Field>();

		return _setRandomFinalAndSplitField.Execute(field).SelectMany(SolveWithChangeField);
	}

	private bool? TrySolveWithoutRandom(Field field)
	{
		_cleanPossibleFacade.Execute(field);

		while (true)
		{
			var setFinalResult = _setFinalFacade.Execute(field);

			if (_fieldService.IsFailed(field))
				return false;

			if (_fieldService.IsSolved(field))
				return true;

			if (setFinalResult == FieldActionsResult.Changed)
				continue;

			var cleanPossibleResult = _cleanPossibleFacade.Execute(field);

			if (cleanPossibleResult == FieldActionsResult.Nothing)
				return null;
		}
	}
}