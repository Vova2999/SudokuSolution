using System.Linq;
using SudokuSolution.Domain.Entities;

namespace SudokuSolution.Logic.FieldActions.SetRandomFinalAndSplitField {
	public class SetRandomFinalAndSplitField : ISetRandomFinalAndSplitField {
		public Field[] Execute(Field field) {
			var (row, column, cell) = GetFirstNotFinalCell(field);
			if (cell == null)
				return null;

			var values = Enumerable.Range(1, field.MaxValue).Where(value => cell[value]).ToArray();
			var newFields = Enumerable.Range(0, values.Length).Select(_ => field.Clone() as Field).ToArray();
			for (var fieldIndex = 0; fieldIndex < newFields.Length; fieldIndex++)
				newFields[fieldIndex].Cells[row, column].Final = values[fieldIndex];

			return newFields;
		}

		private static (int Row, int Column, Cell cell) GetFirstNotFinalCell(Field field) {
			for (var row = 0; row < field.MaxValue; row++)
			for (var column = 0; column < field.MaxValue; column++) {
				if (!field.Cells[row, column].HasFinal)
					return (row, column, field.Cells[row, column]);
			}

			return (-1, -1, null);
		}
	}
}