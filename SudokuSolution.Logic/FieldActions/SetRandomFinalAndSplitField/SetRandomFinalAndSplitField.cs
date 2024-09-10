using System.Collections.Generic;
using System.Linq;
using SudokuSolution.Domain.Entities;

namespace SudokuSolution.Logic.FieldActions.SetRandomFinalAndSplitField;

public class SetRandomFinalAndSplitField : ISetRandomFinalAndSplitField
{
	public IEnumerable<Field> Execute(Field field)
	{
		var (row, column, cell) = GetFirstNotFinalCell(field);
		if (cell == null)
			return null;

		return Enumerable.Range(1, field.MaxValue)
			.Where(value => cell[value])
			.Select(value =>
			{
				var newField = (Field) field.Clone();
				newField.Cells[row, column].Final = value;
				return newField;
			});
	}

	private static (int Row, int Column, Cell cell) GetFirstNotFinalCell(Field field)
	{
		for (var row = 0; row < field.MaxValue; row++)
		for (var column = 0; column < field.MaxValue; column++)
		{
			if (!field.Cells[row, column].HasFinal)
				return (row, column, field.Cells[row, column]);
		}

		return (-1, -1, null);
	}
}