using System;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using SudokuSolution.Domain.Entities;

namespace SudokuSolution.Test.Domain {
	[TestFixture]
	public class CellTests {
		private static readonly int[] MaxValues = { 4, 9, 16 };

		[Test]
		[TestCaseSource(nameof(MaxValues))]
		public void StateAfterCreateTest(int maxValue) {
			var cell = new Cell(maxValue);
			cell.HasFinal.Should().BeFalse();

			foreach (var value in Enumerable.Range(1, maxValue))
				cell[value].Should().BeTrue();
		}

		[Test]
		[TestCaseSource(nameof(MaxValues))]
		public void ChangePossibleTest(int maxValue) {
			var cell = new Cell(maxValue);
			cell.HasFinal.Should().BeFalse();

			cell[1] = false;
			cell[1].Should().BeFalse();
			foreach (var value in Enumerable.Range(2, maxValue - 1))
				cell[value].Should().BeTrue();
		}

		[Test]
		[TestCaseSource(nameof(MaxValues))]
		public void ChangePossibleBrokenTest(int maxValue) {
			var cell = new Cell(maxValue);
			cell.HasFinal.Should().BeFalse();

			new Action(() => cell[-1] = false).Should().Throw<Exception>();
			new Action(() => cell[0] = false).Should().Throw<Exception>();
			new Action(() => cell[maxValue + 1] = false).Should().Throw<Exception>();
			new Action(() => cell[maxValue + 2] = false).Should().Throw<Exception>();
		}

		[Test]
		[TestCaseSource(nameof(MaxValues))]
		public void ChangePossibleAfterSetFinalTest(int maxValue) {
			var cell = new Cell(maxValue);
			cell.HasFinal.Should().BeFalse();

			cell.Final = 1;
			cell[1] = true;
			cell[1].Should().BeFalse();
			foreach (var value in Enumerable.Range(2, maxValue - 1))
				cell[value].Should().BeFalse();
		}

		[Test]
		[TestCaseSource(nameof(MaxValues))]
		public void ChangeFinalTest(int maxValue) {
			var cell = new Cell(maxValue);
			cell.HasFinal.Should().BeFalse();

			cell.Final = 1;
			cell.HasFinal.Should().BeTrue();
			cell.Final.Should().Be(1);
			foreach (var value in Enumerable.Range(1, maxValue))
				cell[value].Should().BeFalse();
		}

		[Test]
		[TestCaseSource(nameof(MaxValues))]
		public void ChangeFinalBrokenTest(int maxValue) {
			var cell = new Cell(maxValue);
			cell.HasFinal.Should().BeFalse();

			new Action(() => cell.Final = -1).Should().Throw<Exception>();
			new Action(() => cell.Final = 0).Should().Throw<Exception>();
			new Action(() => cell.Final = maxValue + 1).Should().Throw<Exception>();
			new Action(() => cell.Final = maxValue + 2).Should().Throw<Exception>();
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