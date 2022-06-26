using System;
using SudokuSolution.Domain.Entities;
using SudokuSolution.Logic.FieldActions.CleanPossibleByColumn;
using SudokuSolution.Logic.FieldActions.CleanPossibleByFinal;
using SudokuSolution.Logic.FieldActions.CleanPossibleByRow;
using SudokuSolution.Logic.FieldActions.SetFinalForColumn;
using SudokuSolution.Logic.FieldActions.SetFinalForRow;
using SudokuSolution.Logic.FieldActions.SetFinalForSinglePossible;
using SudokuSolution.Logic.FieldActions.SetFinalForSquare;
using SudokuSolution.Logic.FieldActions.SetRandomFinalAndSplitField;

namespace SudokuSolution.Logic.GameService {
	public class GameService : IGameService {
		private readonly ICleanPossibleByFinal cleanPossibleByFinal;
		private readonly ICleanPossibleByRow cleanPossibleByRow;
		private readonly ICleanPossibleByColumn cleanPossibleByColumn;
		private readonly ISetFinalForSinglePossible setFinalForSinglePossible;
		private readonly ISetFinalForRow setFinalForRow;
		private readonly ISetFinalForColumn setFinalForColumn;
		private readonly ISetFinalForSquare setFinalForSquare;
		private readonly ISetRandomFinalAndSplitField setRandomFinalAndSplitField;

		public GameService(ICleanPossibleByFinal cleanPossibleByFinal,
						   ICleanPossibleByRow cleanPossibleByRow,
						   ICleanPossibleByColumn cleanPossibleByColumn,
						   ISetFinalForSinglePossible setFinalForSinglePossible,
						   ISetFinalForRow setFinalForRow,
						   ISetFinalForColumn setFinalForColumn,
						   ISetFinalForSquare setFinalForSquare,
						   ISetRandomFinalAndSplitField setRandomFinalAndSplitField) {
			this.cleanPossibleByFinal = cleanPossibleByFinal;
			this.cleanPossibleByRow = cleanPossibleByRow;
			this.cleanPossibleByColumn = cleanPossibleByColumn;
			this.setFinalForSinglePossible = setFinalForSinglePossible;
			this.setFinalForRow = setFinalForRow;
			this.setFinalForColumn = setFinalForColumn;
			this.setFinalForSquare = setFinalForSquare;
			this.setRandomFinalAndSplitField = setRandomFinalAndSplitField;
		}

		// Заполнить Possible пустых ячеек //
		// Пройтись по имеющимся Final и убрать лишние Possible // CleanPossibleByFinal
		// CleanPossibleByRow
		// CleanPossibleByColumn
		// Выставить те ячейки, в которых только один Possible // SetFinalValueForSinglePossible
		// В каждом квадрате выставить те ячейки, значения которых возможны только в одной ячейке // SetFinalForSquare
		// В каждой строке выставить те ячейки, значения которых возможны только в одной строке // SetFinalForRow
		// В каждом столбце выставить те ячейки, значения которых возможны только в одном столбце // SetFinalForColumn
		// Повторять 2-5 пункты, пока они меняют доску
		// Клонировать доску и зарандомить любую нерешенную ячейку путем поиска в глубину (решений может быть несколько) // SetRandomFinalValueAndSplitField
		// Повторять 2-7 пункты, пока не будут найдены все решения

		public Field[] Solve(Field field) {
			Field previewField;

			do {
				previewField = field.Clone() as Field;
				cleanPossibleByFinal.Execute(field);
				cleanPossibleByRow.Execute(field);
				cleanPossibleByColumn.Execute(field);
				setFinalForSinglePossible.Execute(field);
				setFinalForSquare.Execute(field);
				setFinalForRow.Execute(field);
				setFinalForColumn.Execute(field);

				//if ()
			} while (!field.Equals(previewField));

			throw new Exception();
		}
	}
}