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
			var cell = new Cell();
			cell.SetMaxValue(maxValue);

			cell.HasFinal.Should().BeFalse();
			foreach (var value in Enumerable.Range(1, maxValue))
				cell[value].Should().BeTrue();
		}

		[Test]
		[TestCaseSource(nameof(MaxValues))]
		public void ChangePossibleTest(int maxValue) {
			var cell = new Cell();
			cell.SetMaxValue(maxValue);

			cell[1] = false;
			cell[1].Should().BeFalse();
			foreach (var value in Enumerable.Range(2, maxValue - 1))
				cell[value].Should().BeTrue();
		}

		[Test]
		[TestCaseSource(nameof(MaxValues))]
		public void ChangePossibleBrokenTest(int maxValue) {
			var cell = new Cell();
			cell.SetMaxValue(maxValue);

			new Action(() => cell[-1] = false).Should().Throw<Exception>();
			new Action(() => cell[0] = false).Should().Throw<Exception>();
			new Action(() => cell[maxValue + 1] = false).Should().Throw<Exception>();
			new Action(() => cell[maxValue + 2] = false).Should().Throw<Exception>();
		}

		[Test]
		[TestCaseSource(nameof(MaxValues))]
		public void ChangeFinalTest(int maxValue) {
			var cell = new Cell();
			cell.SetMaxValue(maxValue);

			cell.Final = 1;
			cell.HasFinal.Should().BeTrue();
			cell.Final.Should().Be(1);
			foreach (var value in Enumerable.Range(1, maxValue))
				cell[value].Should().BeFalse();
		}
		[Test]
		[TestCaseSource(nameof(MaxValues))]
		public void ChangeFinalBrokenTest(int maxValue) {
			var cell = new Cell();
			cell.SetMaxValue(maxValue);

			new Action(() => cell.Final = -1).Should().Throw<Exception>();
			new Action(() => cell.Final = 0).Should().Throw<Exception>();
			new Action(() => cell.Final = maxValue + 1).Should().Throw<Exception>();
			new Action(() => cell.Final = maxValue + 2).Should().Throw<Exception>();
		}
	}
}