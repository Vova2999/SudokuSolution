using System.Linq;
using SudokuSolution.Common.Extensions;
using SudokuSolution.Domain.Entities;

namespace SudokuSolution.Logic.FieldFactory {
	public class FieldFactory : IFieldFactory {
		public Field Create(int size) {
			var totalValue = size * size;
			var field = new Field(new Cell[totalValue, totalValue]);
			Enumerable.Range(0, size)
				.ForEach(i => Enumerable.Range(0, size)
					.ForEach(j => field.Cells[i, j] = new Cell(new bool[totalValue])));

			return field;
		}
	}
}