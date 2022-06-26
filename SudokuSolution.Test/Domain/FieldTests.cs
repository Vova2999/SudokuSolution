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

		[Test]
		[TestCaseSource(nameof(MaxValues))]
		public void CloneTest(int maxValue) {
			var cell1 = new Cell(maxValue) { Final = 1 };
			var cell2 = (Cell) cell1.Clone();

			cell1.Equals(cell2).Should().BeTrue();

			var cell3 = new Cell(maxValue) { [1] = false };
			var cell4 = (Cell) cell3.Clone();

			cell3.Equals(cell4).Should().BeTrue();

			cell3[1] = true;

			cell3.Equals(cell4).Should().BeFalse();
		}

		[Test]
		[TestCaseSource(nameof(MaxValues))]
		public void EqualsTest(int maxValue) {
			var cell1 = new Cell(maxValue);
			var cell2 = new Cell(maxValue);

			cell1.Final = 1;
			cell2[1] = false;
			cell2.Final = 1;

			cell1.Equals(cell2).Should().BeTrue();

			var cell3 = new Cell(maxValue);
			var cell4 = new Cell(maxValue);

			cell3[1] = false;
			cell4[1] = false;

			cell3.Equals(cell4).Should().BeTrue();
		}

		[Test]
		[TestCaseSource(nameof(MaxValues))]
		public void NotEqualsTest(int maxValue) {
			var cell1 = new Cell(maxValue);
			var cell2 = new Cell(maxValue);
			var cell3 = new Cell(maxValue);
			var cell4 = new Cell(maxValue);

			cell1.Final = 1;
			cell2[1] = false;
			cell2.Final = 2;
			cell3[1] = false;
			cell4[2] = false;

			cell1.Equals(cell2).Should().BeFalse();
			cell1.Equals(cell3).Should().BeFalse();
			cell2.Equals(cell3).Should().BeFalse();
			cell3.Equals(cell4).Should().BeFalse();
		}
	}
}