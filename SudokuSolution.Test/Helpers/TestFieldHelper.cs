using System;
using SudokuSolution.Common.Extensions;
using SudokuSolution.Domain.Entities;

namespace SudokuSolution.Test.Helpers {
	public static class TestFieldHelper {
		public static Field GetTestField() {
			int[,] values = {
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
		public static Field GetSmallTestField() {
			int[,] values = {
				{ 2, 0, 0, 0 },
				{ 0, 0, 3, 0 },
				{ 0, 0, 0, 0 },
				{ 0, 1, 0, 4 }
			};

			return CreateAndFillField(values);
		}

		private static Field CreateAndFillField(int[,] values) {
			var maxValue = values.GetLength(0);
			if (maxValue != values.GetLength(1))
				throw new InvalidOperationException("Invalid array sizes");

			var field = new Field(maxValue);
			values.ForEach((row, column, value) => {
				if (value != 0)
					field.Cells[row, column].Final = value;
			});

			return field;
		}
	}
}