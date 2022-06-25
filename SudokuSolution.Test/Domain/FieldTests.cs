using FluentAssertions;
using NUnit.Framework;
using SudokuSolution.Domain.Entities;

namespace SudokuSolution.Test.Domain {
	[TestFixture]
	public class FieldTests {
		private static readonly int[] MaxValues = { 4, 9, 16 };

		[Test]
		[TestCaseSource(nameof(MaxValues))]
		public void StateAfterCreateTest(int maxValue) {
			var field = new Field(maxValue);

			field.MaxValue.Should().Be(maxValue);
			field.Cells.GetLength(0).Should().Be(maxValue);
			field.Cells.GetLength(1).Should().Be(maxValue);
			field.Cells[0, 0].Final = 1;
			field.Cells[0, 1].Final = maxValue;
		}
	}
}