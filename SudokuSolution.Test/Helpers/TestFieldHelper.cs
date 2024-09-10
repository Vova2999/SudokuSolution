using System;
using SudokuSolution.Common.Extensions;
using SudokuSolution.Domain.Entities;
using SudokuSolution.Test.Extensions;

namespace SudokuSolution.Test.Helpers;

public static class TestFieldHelper
{
	public static Field GetSmallTestField()
	{
		int[,] values =
		{
			{ 2, 0, 0, 0 },
			{ 0, 0, 3, 0 },
			{ 0, 0, 0, 0 },
			{ 0, 1, 0, 4 }
		};

		return CreateAndFillField(values);
	}

	public static Field GetNotCompleteSmallTestField()
	{
		int[,] values =
		{
			{ 0, 0, 0, 0 },
			{ 0, 0, 3, 0 },
			{ 0, 0, 0, 0 },
			{ 0, 1, 0, 4 }
		};

		return CreateAndFillField(values);
	}

	public static Field GetTestField()
	{
		int[,] values =
		{
			{ 0, 8, 0, 9, 3, 0, 0, 0, 7 },
			{ 0, 0, 0, 4, 0, 5, 8, 0, 0 },
			{ 0, 0, 0, 0, 0, 0, 0, 4, 0 },
			{ 0, 0, 1, 0, 0, 2, 0, 9, 0 },
			{ 8, 0, 0, 0, 1, 0, 0, 0, 2 },
			{ 0, 4, 0, 5, 0, 0, 6, 0, 0 },
			{ 0, 7, 0, 0, 0, 0, 0, 0, 0 },
			{ 0, 0, 5, 8, 0, 4, 0, 0, 0 },
			{ 1, 0, 0, 0, 2, 7, 0, 6, 0 }
		};

		return CreateAndFillField(values);
	}

	public static Field GetHardTestField()
	{
		int[,] values =
		{
			{ 0, 0, 0, 0, 3, 0, 0, 0, 2 },
			{ 0, 0, 4, 0, 0, 0, 0, 0, 9 },
			{ 0, 0, 0, 0, 5, 1, 0, 4, 0 },
			{ 0, 4, 0, 3, 2, 0, 0, 9, 0 },
			{ 0, 0, 2, 1, 0, 8, 3, 0, 0 },
			{ 0, 5, 0, 0, 4, 6, 0, 1, 0 },
			{ 0, 3, 0, 5, 6, 0, 0, 0, 0 },
			{ 5, 0, 0, 0, 0, 0, 4, 0, 0 },
			{ 2, 0, 0, 0, 9, 0, 0, 0, 0 }
		};

		return CreateAndFillField(values);
	}

	private static Field CreateAndFillField(int[,] values)
	{
		var maxValue = values.GetLength(0);
		if (maxValue != values.GetLength(1))
			throw new InvalidOperationException("Invalid array sizes");

		var field = new Field(maxValue);
		values.ForEach((row, column, value) =>
		{
			if (value != 0)
				field.Cells[row, column].Final = value;
		});

		return field;
	}

	public static Field GetSolvedSmallTestField()
	{
		var field = new Field(4);
		field.Cells[0, 0].SetFinal(2);
		field.Cells[0, 1].SetFinal(3);
		field.Cells[0, 2].SetFinal(4);
		field.Cells[0, 3].SetFinal(1);
		field.Cells[1, 0].SetFinal(1);
		field.Cells[1, 1].SetFinal(4);
		field.Cells[1, 2].SetFinal(3);
		field.Cells[1, 3].SetFinal(2);
		field.Cells[2, 0].SetFinal(4);
		field.Cells[2, 1].SetFinal(2);
		field.Cells[2, 2].SetFinal(1);
		field.Cells[2, 3].SetFinal(3);
		field.Cells[3, 0].SetFinal(3);
		field.Cells[3, 1].SetFinal(1);
		field.Cells[3, 2].SetFinal(2);
		field.Cells[3, 3].SetFinal(4);

		return field;
	}

	public static Field GetSmallTestFieldWithPossible()
	{
		var field = new Field(4);
		field.Cells[0, 0].SetFinal(2);
		field.Cells[0, 1].SetNotFinal(field.MaxValue, 3, 4);
		field.Cells[0, 2].SetNotFinal(field.MaxValue, 1, 4);
		field.Cells[0, 3].SetNotFinal(field.MaxValue, 1);
		field.Cells[1, 0].SetNotFinal(field.MaxValue, 1, 4);
		field.Cells[1, 1].SetNotFinal(field.MaxValue, 4);
		field.Cells[1, 2].SetFinal(3);
		field.Cells[1, 3].SetNotFinal(field.MaxValue, 1, 2);
		field.Cells[2, 0].SetNotFinal(field.MaxValue, 3, 4);
		field.Cells[2, 1].SetNotFinal(field.MaxValue, 2, 3, 4);
		field.Cells[2, 2].SetNotFinal(field.MaxValue, 1, 2);
		field.Cells[2, 3].SetNotFinal(field.MaxValue, 1, 2, 3);
		field.Cells[3, 0].SetNotFinal(field.MaxValue, 3);
		field.Cells[3, 1].SetFinal(1);
		field.Cells[3, 2].SetNotFinal(field.MaxValue, 2);
		field.Cells[3, 3].SetFinal(4);

		return field;
	}

	public static Field GetTestFieldWithPossible()
	{
		var field = new Field(9);
		field.Cells[0, 0].SetNotFinal(field.MaxValue, 2, 4, 5, 6);
		field.Cells[0, 1].SetFinal(8);
		field.Cells[0, 2].SetNotFinal(field.MaxValue, 2, 4, 6);
		field.Cells[0, 3].SetFinal(9);
		field.Cells[0, 4].SetFinal(3);
		field.Cells[0, 5].SetNotFinal(field.MaxValue, 1, 6);
		field.Cells[0, 6].SetNotFinal(field.MaxValue, 1, 2, 5);
		field.Cells[0, 7].SetNotFinal(field.MaxValue, 1, 2, 5);
		field.Cells[0, 8].SetFinal(7);
		field.Cells[1, 0].SetNotFinal(field.MaxValue, 2, 3, 6, 7, 9);
		field.Cells[1, 1].SetNotFinal(field.MaxValue, 1, 2, 3, 6, 9);
		field.Cells[1, 2].SetNotFinal(field.MaxValue, 2, 3, 6, 7, 9);
		field.Cells[1, 3].SetFinal(4);
		field.Cells[1, 4].SetNotFinal(field.MaxValue, 6, 7);
		field.Cells[1, 5].SetFinal(5);
		field.Cells[1, 6].SetFinal(8);
		field.Cells[1, 7].SetNotFinal(field.MaxValue, 1, 2, 3);
		field.Cells[1, 8].SetNotFinal(field.MaxValue, 1, 3, 6, 9);
		field.Cells[2, 0].SetNotFinal(field.MaxValue, 2, 3, 5, 6, 7, 9);
		field.Cells[2, 1].SetNotFinal(field.MaxValue, 1, 2, 3, 5, 6, 9);
		field.Cells[2, 2].SetNotFinal(field.MaxValue, 2, 3, 6, 7, 9);
		field.Cells[2, 3].SetNotFinal(field.MaxValue, 1, 2, 6, 7);
		field.Cells[2, 4].SetNotFinal(field.MaxValue, 6, 7, 8);
		field.Cells[2, 5].SetNotFinal(field.MaxValue, 1, 6, 8);
		field.Cells[2, 6].SetNotFinal(field.MaxValue, 1, 2, 3, 5, 9);
		field.Cells[2, 7].SetFinal(4);
		field.Cells[2, 8].SetNotFinal(field.MaxValue, 1, 3, 5, 6, 9);
		field.Cells[3, 0].SetNotFinal(field.MaxValue, 3, 5, 6, 7);
		field.Cells[3, 1].SetNotFinal(field.MaxValue, 3, 5, 6);
		field.Cells[3, 2].SetFinal(1);
		field.Cells[3, 3].SetNotFinal(field.MaxValue, 3, 6, 7);
		field.Cells[3, 4].SetNotFinal(field.MaxValue, 4, 6, 7, 8);
		field.Cells[3, 5].SetFinal(2);
		field.Cells[3, 6].SetNotFinal(field.MaxValue, 3, 4, 5, 7);
		field.Cells[3, 7].SetFinal(9);
		field.Cells[3, 8].SetNotFinal(field.MaxValue, 3, 4, 5, 8);
		field.Cells[4, 0].SetFinal(8);
		field.Cells[4, 1].SetNotFinal(field.MaxValue, 3, 5, 6, 9);
		field.Cells[4, 2].SetNotFinal(field.MaxValue, 3, 6, 7, 9);
		field.Cells[4, 3].SetNotFinal(field.MaxValue, 3, 6, 7);
		field.Cells[4, 4].SetFinal(1);
		field.Cells[4, 5].SetNotFinal(field.MaxValue, 3, 6, 9);
		field.Cells[4, 6].SetNotFinal(field.MaxValue, 3, 4, 5, 7);
		field.Cells[4, 7].SetNotFinal(field.MaxValue, 3, 5, 7);
		field.Cells[4, 8].SetFinal(2);
		field.Cells[5, 0].SetNotFinal(field.MaxValue, 2, 3, 7, 9);
		field.Cells[5, 1].SetFinal(4);
		field.Cells[5, 2].SetNotFinal(field.MaxValue, 2, 3, 7, 9);
		field.Cells[5, 3].SetFinal(5);
		field.Cells[5, 4].SetNotFinal(field.MaxValue, 7, 8, 9);
		field.Cells[5, 5].SetNotFinal(field.MaxValue, 3, 8, 9);
		field.Cells[5, 6].SetFinal(6);
		field.Cells[5, 7].SetNotFinal(field.MaxValue, 1, 3, 7, 8);
		field.Cells[5, 8].SetNotFinal(field.MaxValue, 1, 3, 8);
		field.Cells[6, 0].SetNotFinal(field.MaxValue, 2, 3, 4, 6, 9);
		field.Cells[6, 1].SetFinal(7);
		field.Cells[6, 2].SetNotFinal(field.MaxValue, 2, 3, 4, 6, 8, 9);
		field.Cells[6, 3].SetNotFinal(field.MaxValue, 1, 3, 6);
		field.Cells[6, 4].SetNotFinal(field.MaxValue, 5, 6, 9);
		field.Cells[6, 5].SetNotFinal(field.MaxValue, 1, 6, 9);
		field.Cells[6, 6].SetNotFinal(field.MaxValue, 1, 2, 3, 4, 5, 9);
		field.Cells[6, 7].SetNotFinal(field.MaxValue, 1, 2, 3, 5, 8);
		field.Cells[6, 8].SetNotFinal(field.MaxValue, 1, 3, 4, 5, 8, 9);
		field.Cells[7, 0].SetNotFinal(field.MaxValue, 2, 3, 6, 9);
		field.Cells[7, 1].SetNotFinal(field.MaxValue, 2, 3, 6, 9);
		field.Cells[7, 2].SetFinal(5);
		field.Cells[7, 3].SetFinal(8);
		field.Cells[7, 4].SetNotFinal(field.MaxValue, 6, 9);
		field.Cells[7, 5].SetFinal(4);
		field.Cells[7, 6].SetNotFinal(field.MaxValue, 1, 2, 3, 7, 9);
		field.Cells[7, 7].SetNotFinal(field.MaxValue, 1, 2, 3, 7);
		field.Cells[7, 8].SetNotFinal(field.MaxValue, 1, 3, 9);
		field.Cells[8, 0].SetFinal(1);
		field.Cells[8, 1].SetNotFinal(field.MaxValue, 3, 9);
		field.Cells[8, 2].SetNotFinal(field.MaxValue, 3, 4, 8, 9);
		field.Cells[8, 3].SetNotFinal(field.MaxValue, 3);
		field.Cells[8, 4].SetFinal(2);
		field.Cells[8, 5].SetFinal(7);
		field.Cells[8, 6].SetNotFinal(field.MaxValue, 3, 4, 5, 9);
		field.Cells[8, 7].SetFinal(6);
		field.Cells[8, 8].SetNotFinal(field.MaxValue, 3, 4, 5, 8, 9);

		return field;
	}
}