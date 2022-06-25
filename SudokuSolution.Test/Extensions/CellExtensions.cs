using System.Linq;
using FluentAssertions;
using SudokuSolution.Common.Extensions;
using SudokuSolution.Domain.Entities;

namespace SudokuSolution.Test.Extensions {
	public static class CellExtensions {
		public static void SetFinal(this ref Cell cell, int value) {
			cell.Final = value;
		}

		public static void SetNotFinal(this ref Cell cell, int maxValue, params int[] possibleValues) {
			cell.SetMaxValue(maxValue);
			foreach (var value in Enumerable.Range(1, maxValue))
				cell[value] = possibleValues.Contains(value);
		}

		public static void ShouldBeFinal(this Cell cell, int value) {
			cell.HasFinal.Should().BeTrue($"final value must be {value}");
			cell.Final.Should().Be(value);
		}

		public static void ShouldBeNotFinal(this Cell cell, int maxValue, params int[] possibleValues) {
			cell.HasFinal.Should().BeFalse();
			Enumerable.Range(1, maxValue).ForEach(value => cell[value].Should().Be(possibleValues.Contains(value),
				$"excepted possible is {{{string.Join(", ", possibleValues)}}}, with {{{string.Join(", ", Enumerable.Range(1, maxValue).Where(x => cell[x]))}}} received"));
		}
	}
}