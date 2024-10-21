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
	private readonly IFieldService fieldService;
	private readonly ISetFinalFacade setFinalFacade;
	private readonly ICleanPossibleFacade cleanPossibleFacade;
	private readonly ISetRandomFinalAndSplitField setRandomFinalAndSplitField;

	public GameService(IFieldService fieldService,
					   ISetFinalFacade setFinalFacade,
					   ICleanPossibleFacade cleanPossibleFacade,
					   ISetRandomFinalAndSplitField setRandomFinalAndSplitField)
	{
		this.fieldService = fieldService;
		this.setFinalFacade = setFinalFacade;
		this.cleanPossibleFacade = cleanPossibleFacade;
		this.setRandomFinalAndSplitField = setRandomFinalAndSplitField;
	}

	public IEnumerable<Field> Solve(Field field)
	{
		return SolveWithChangeField((Field) field.Clone());
	}

	private IEnumerable<Field> SolveWithChangeField(Field field)
	{
		var withoutRandomResult = TrySolveWithoutRandom(field);
		if (withoutRandomResult.HasValue)
			return withoutRandomResult.Value ? field.AsEnumerable() : Enumerable.Empty<Field>();

		return setRandomFinalAndSplitField.Execute(field).SelectMany(SolveWithChangeField);
	}

	private bool? TrySolveWithoutRandom(Field field)
	{
		cleanPossibleFacade.Execute(field);

		while (true)
		{
			var setFinalResult = setFinalFacade.Execute(field);

			if (fieldService.IsFailed(field))
				return false;

			if (fieldService.IsSolved(field))
				return true;

			if (setFinalResult == FieldActionsResult.Changed)
				continue;

			var cleanPossibleResult = cleanPossibleFacade.Execute(field);

			if (cleanPossibleResult == FieldActionsResult.Nothing)
				return null;
		}
	}
}