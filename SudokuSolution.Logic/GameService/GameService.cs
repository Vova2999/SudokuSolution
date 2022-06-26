using System.Collections.Generic;
using System.Linq;
using SudokuSolution.Common.Extensions;
using SudokuSolution.Domain.Entities;
using SudokuSolution.Logic.FieldActions.CleanPossibleByColumn;
using SudokuSolution.Logic.FieldActions.CleanPossibleByFinal;
using SudokuSolution.Logic.FieldActions.CleanPossibleByRow;
using SudokuSolution.Logic.FieldActions.SetFinalForColumn;
using SudokuSolution.Logic.FieldActions.SetFinalForRow;
using SudokuSolution.Logic.FieldActions.SetFinalForSinglePossible;
using SudokuSolution.Logic.FieldActions.SetFinalForSquare;
using SudokuSolution.Logic.FieldActions.SetRandomFinalAndSplitField;
using SudokuSolution.Logic.FieldService;

namespace SudokuSolution.Logic.GameService {
	public class GameService : IGameService {
		private readonly IFieldService fieldService;
		private readonly ICleanPossibleByFinal cleanPossibleByFinal;
		private readonly ICleanPossibleByRow cleanPossibleByRow;
		private readonly ICleanPossibleByColumn cleanPossibleByColumn;
		private readonly ISetFinalForSinglePossible setFinalForSinglePossible;
		private readonly ISetFinalForRow setFinalForRow;
		private readonly ISetFinalForColumn setFinalForColumn;
		private readonly ISetFinalForSquare setFinalForSquare;
		private readonly ISetRandomFinalAndSplitField setRandomFinalAndSplitField;

		public GameService(IFieldService fieldService,
						   ICleanPossibleByFinal cleanPossibleByFinal,
						   ICleanPossibleByRow cleanPossibleByRow,
						   ICleanPossibleByColumn cleanPossibleByColumn,
						   ISetFinalForSinglePossible setFinalForSinglePossible,
						   ISetFinalForRow setFinalForRow,
						   ISetFinalForColumn setFinalForColumn,
						   ISetFinalForSquare setFinalForSquare,
						   ISetRandomFinalAndSplitField setRandomFinalAndSplitField) {
			this.fieldService = fieldService;
			this.cleanPossibleByFinal = cleanPossibleByFinal;
			this.cleanPossibleByRow = cleanPossibleByRow;
			this.cleanPossibleByColumn = cleanPossibleByColumn;
			this.setFinalForSinglePossible = setFinalForSinglePossible;
			this.setFinalForRow = setFinalForRow;
			this.setFinalForColumn = setFinalForColumn;
			this.setFinalForSquare = setFinalForSquare;
			this.setRandomFinalAndSplitField = setRandomFinalAndSplitField;
		}

		public IEnumerable<Field> Solve(Field field) {
			return SolveWithChangeField((Field) field.Clone());
		}

		private IEnumerable<Field> SolveWithChangeField(Field field) {
			var withoutRandomResult = TrySolveWithoutRandom(field);
			if (withoutRandomResult.HasValue)
				return withoutRandomResult.Value ? field.AsEnumerable() : Enumerable.Empty<Field>();

			return setRandomFinalAndSplitField.Execute(field).SelectMany(Solve);
		}

		private bool? TrySolveWithoutRandom(Field field) {
			Field previewField;

			do {
				previewField = (Field) field.Clone();
				cleanPossibleByFinal.Execute(field);
				cleanPossibleByRow.Execute(field);
				cleanPossibleByColumn.Execute(field);
				setFinalForSinglePossible.Execute(field);
				setFinalForSquare.Execute(field);
				setFinalForRow.Execute(field);
				setFinalForColumn.Execute(field);

				if (fieldService.IsFailed(field))
					return false;

				if (fieldService.IsSolved(field))
					return true;
			} while (!field.Equals(previewField));

			return null;
		}
	}
}