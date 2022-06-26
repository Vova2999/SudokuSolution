using System.Linq;
using SudokuSolution.Domain.Entities;

namespace SudokuSolution.Logic.FieldService {
	public class FieldService : IFieldService {
		public bool IsSolved(Field field) {
			for (var row = 0; row < field.MaxValue; row++)
			for (var column = 0; column < field.MaxValue; column++) {
				if (!field.Cells[row, column].HasFinal)
					return false;
			}

			return true;
		}

		public bool IsFailed(Field field) {
			for (var row = 0; row < field.MaxValue; row++)
			for (var column = 0; column < field.MaxValue; column++) {
				if (field.Cells[row, column].HasFinal)
					continue;

				if (Enumerable.Range(1, field.MaxValue).All(value => !field.Cells[row, column][value]))
					return true;
			}

			return false;
		}
	}
}