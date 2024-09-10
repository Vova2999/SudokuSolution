using FluentAssertions;
using NUnit.Framework;
using SudokuSolution.Domain.Entities;
using SudokuSolution.Test.Helpers;

namespace SudokuSolution.Test.Domain;

[TestFixture]
public class FieldTests
{
	private static readonly int[] MaxValues = { 4, 9, 16 };

	[Test]
	[TestCaseSource(nameof(MaxValues))]
	public void StateAfterCreateTest(int maxValue)
	{
		var field = new Field(maxValue);

		field.MaxValue.Should().Be(maxValue);
		field.Cells.GetLength(0).Should().Be(maxValue);
		field.Cells.GetLength(1).Should().Be(maxValue);
		field.Cells[0, 0].Final = 1;
		field.Cells[0, 1].Final = maxValue;
	}

	[Test]
	public void CloneTest()
	{
		var field1 = TestFieldHelper.GetSmallTestFieldWithPossible();
		var field2 = (Field) field1.Clone();

		field1.Equals(field1).Should().BeTrue();
		field1.Equals(field2).Should().BeTrue();

		field1.Cells[0, 1].Final = 3;

		field1.Equals(field2).Should().BeFalse();
	}

	[Test]
	public void EqualsTest()
	{
		var field1 = TestFieldHelper.GetSmallTestFieldWithPossible();
		var field2 = TestFieldHelper.GetSmallTestFieldWithPossible();

		field1.Equals(field1).Should().BeTrue();
		field1.Equals(field2).Should().BeTrue();

		field1.Cells[0, 1].Final = 3;

		field1.Equals(field2).Should().BeFalse();

		field2.Cells[0, 1].Final = 3;

		field1.Equals(field2).Should().BeTrue();

		field1.Cells[0, 2][1] = false;

		field1.Equals(field2).Should().BeFalse();

		field2.Cells[0, 2][1] = false;

		field1.Equals(field2).Should().BeTrue();
	}
}