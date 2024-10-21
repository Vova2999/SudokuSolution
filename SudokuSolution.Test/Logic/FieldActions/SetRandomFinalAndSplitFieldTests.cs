using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using SudokuSolution.Logic.FieldActions.SetRandomFinalAndSplitField;
using SudokuSolution.Test.Extensions;
using SudokuSolution.Test.Helpers;

namespace SudokuSolution.Test.Logic.FieldActions;

[TestFixture]
public class SetRandomFinalAndSplitFieldTests
{
	private ISetRandomFinalAndSplitField _setRandomFinalAndSplitField;

	[OneTimeSetUp]
	public void CreateService()
	{
		_setRandomFinalAndSplitField = new SetRandomFinalAndSplitField();
	}

	[Test]
	public void ExecuteWithSmallFieldTest()
	{
		var field = TestFieldHelper.GetSmallTestFieldWithPossible();
		var newFields = _setRandomFinalAndSplitField.Execute(field).ToArray();

		newFields.Length.Should().Be(2);

		newFields[0].Cells[0, 1].ShouldBeFinal(3);
		newFields[1].Cells[0, 1].ShouldBeFinal(4);

		foreach (var newField in newFields)
		{
			newField.Cells[0, 0].ShouldBeFinal(2);
			newField.Cells[0, 2].ShouldBeNotFinal(field.MaxValue, 1, 4);
			newField.Cells[0, 3].ShouldBeNotFinal(field.MaxValue, 1);
			newField.Cells[1, 0].ShouldBeNotFinal(field.MaxValue, 1, 4);
			newField.Cells[1, 1].ShouldBeNotFinal(field.MaxValue, 4);
			newField.Cells[1, 2].ShouldBeFinal(3);
			newField.Cells[1, 3].ShouldBeNotFinal(field.MaxValue, 1, 2);
			newField.Cells[2, 0].ShouldBeNotFinal(field.MaxValue, 3, 4);
			newField.Cells[2, 1].ShouldBeNotFinal(field.MaxValue, 2, 3, 4);
			newField.Cells[2, 2].ShouldBeNotFinal(field.MaxValue, 1, 2);
			newField.Cells[2, 3].ShouldBeNotFinal(field.MaxValue, 1, 2, 3);
			newField.Cells[3, 0].ShouldBeNotFinal(field.MaxValue, 3);
			newField.Cells[3, 1].ShouldBeFinal(1);
			newField.Cells[3, 2].ShouldBeNotFinal(field.MaxValue, 2);
			newField.Cells[3, 3].ShouldBeFinal(4);
		}
	}

	[Test]
	public void ExecuteWithFieldTest()
	{
		var field = TestFieldHelper.GetTestFieldWithPossible();
		var newFields = _setRandomFinalAndSplitField.Execute(field).ToArray();

		newFields.Length.Should().Be(4);

		newFields[0].Cells[0, 0].ShouldBeFinal(2);
		newFields[1].Cells[0, 0].ShouldBeFinal(4);
		newFields[2].Cells[0, 0].ShouldBeFinal(5);
		newFields[3].Cells[0, 0].ShouldBeFinal(6);

		foreach (var newField in newFields)
		{
			newField.Cells[0, 1].SetFinal(8);
			newField.Cells[0, 2].SetNotFinal(field.MaxValue, 2, 4, 6);
			newField.Cells[0, 3].SetFinal(9);
			newField.Cells[0, 4].SetFinal(3);
			newField.Cells[0, 5].SetNotFinal(field.MaxValue, 1, 6);
			newField.Cells[0, 6].SetNotFinal(field.MaxValue, 1, 2, 5);
			newField.Cells[0, 7].SetNotFinal(field.MaxValue, 1, 2, 5);
			newField.Cells[0, 8].SetFinal(7);
			newField.Cells[1, 0].SetNotFinal(field.MaxValue, 2, 3, 6, 7, 9);
			newField.Cells[1, 1].SetNotFinal(field.MaxValue, 1, 2, 3, 6, 9);
			newField.Cells[1, 2].SetNotFinal(field.MaxValue, 2, 3, 6, 7, 9);
			newField.Cells[1, 3].SetFinal(4);
			newField.Cells[1, 4].SetNotFinal(field.MaxValue, 6, 7);
			newField.Cells[1, 5].SetFinal(5);
			newField.Cells[1, 6].SetFinal(8);
			newField.Cells[1, 7].SetNotFinal(field.MaxValue, 1, 2, 3);
			newField.Cells[1, 8].SetNotFinal(field.MaxValue, 1, 3, 6, 9);
			newField.Cells[2, 0].SetNotFinal(field.MaxValue, 2, 3, 5, 6, 7, 9);
			newField.Cells[2, 1].SetNotFinal(field.MaxValue, 1, 2, 3, 5, 6, 9);
			newField.Cells[2, 2].SetNotFinal(field.MaxValue, 2, 3, 6, 7, 9);
			newField.Cells[2, 3].SetNotFinal(field.MaxValue, 1, 2, 6, 7);
			newField.Cells[2, 4].SetNotFinal(field.MaxValue, 6, 7, 8);
			newField.Cells[2, 5].SetNotFinal(field.MaxValue, 1, 6, 8);
			newField.Cells[2, 6].SetNotFinal(field.MaxValue, 1, 2, 3, 5, 9);
			newField.Cells[2, 7].SetFinal(4);
			newField.Cells[2, 8].SetNotFinal(field.MaxValue, 1, 3, 5, 6, 9);
			newField.Cells[3, 0].SetNotFinal(field.MaxValue, 3, 5, 6, 7);
			newField.Cells[3, 1].SetNotFinal(field.MaxValue, 3, 5, 6);
			newField.Cells[3, 2].SetFinal(1);
			newField.Cells[3, 3].SetNotFinal(field.MaxValue, 3, 6, 7);
			newField.Cells[3, 4].SetNotFinal(field.MaxValue, 4, 6, 7, 8);
			newField.Cells[3, 5].SetFinal(2);
			newField.Cells[3, 6].SetNotFinal(field.MaxValue, 3, 4, 5, 7);
			newField.Cells[3, 7].SetFinal(9);
			newField.Cells[3, 8].SetNotFinal(field.MaxValue, 3, 4, 5, 8);
			newField.Cells[4, 0].SetFinal(8);
			newField.Cells[4, 1].SetNotFinal(field.MaxValue, 3, 5, 6, 9);
			newField.Cells[4, 2].SetNotFinal(field.MaxValue, 3, 6, 7, 9);
			newField.Cells[4, 3].SetNotFinal(field.MaxValue, 3, 6, 7);
			newField.Cells[4, 4].SetFinal(1);
			newField.Cells[4, 5].SetNotFinal(field.MaxValue, 3, 6, 9);
			newField.Cells[4, 6].SetNotFinal(field.MaxValue, 3, 4, 5, 7);
			newField.Cells[4, 7].SetNotFinal(field.MaxValue, 3, 5, 7);
			newField.Cells[4, 8].SetFinal(2);
			newField.Cells[5, 0].SetNotFinal(field.MaxValue, 2, 3, 7, 9);
			newField.Cells[5, 1].SetFinal(4);
			newField.Cells[5, 2].SetNotFinal(field.MaxValue, 2, 3, 7, 9);
			newField.Cells[5, 3].SetFinal(5);
			newField.Cells[5, 4].SetNotFinal(field.MaxValue, 7, 8, 9);
			newField.Cells[5, 5].SetNotFinal(field.MaxValue, 3, 8, 9);
			newField.Cells[5, 6].SetFinal(6);
			newField.Cells[5, 7].SetNotFinal(field.MaxValue, 1, 3, 7, 8);
			newField.Cells[5, 8].SetNotFinal(field.MaxValue, 1, 3, 8);
			newField.Cells[6, 0].SetNotFinal(field.MaxValue, 2, 3, 4, 6, 9);
			newField.Cells[6, 1].SetFinal(7);
			newField.Cells[6, 2].SetNotFinal(field.MaxValue, 2, 3, 4, 6, 8, 9);
			newField.Cells[6, 3].SetNotFinal(field.MaxValue, 1, 3, 6);
			newField.Cells[6, 4].SetNotFinal(field.MaxValue, 5, 6, 9);
			newField.Cells[6, 5].SetNotFinal(field.MaxValue, 1, 6, 9);
			newField.Cells[6, 6].SetNotFinal(field.MaxValue, 1, 2, 3, 4, 5, 9);
			newField.Cells[6, 7].SetNotFinal(field.MaxValue, 1, 2, 3, 5, 8);
			newField.Cells[6, 8].SetNotFinal(field.MaxValue, 1, 3, 4, 5, 8, 9);
			newField.Cells[7, 0].SetNotFinal(field.MaxValue, 2, 3, 6, 9);
			newField.Cells[7, 1].SetNotFinal(field.MaxValue, 2, 3, 6, 9);
			newField.Cells[7, 2].SetFinal(5);
			newField.Cells[7, 3].SetFinal(8);
			newField.Cells[7, 4].SetNotFinal(field.MaxValue, 6, 9);
			newField.Cells[7, 5].SetFinal(4);
			newField.Cells[7, 6].SetNotFinal(field.MaxValue, 1, 2, 3, 7, 9);
			newField.Cells[7, 7].SetNotFinal(field.MaxValue, 1, 2, 3, 7);
			newField.Cells[7, 8].SetNotFinal(field.MaxValue, 1, 3, 9);
			newField.Cells[8, 0].SetFinal(1);
			newField.Cells[8, 1].SetNotFinal(field.MaxValue, 3, 9);
			newField.Cells[8, 2].SetNotFinal(field.MaxValue, 3, 4, 8, 9);
			newField.Cells[8, 3].SetNotFinal(field.MaxValue, 3);
			newField.Cells[8, 4].SetFinal(2);
			newField.Cells[8, 5].SetFinal(7);
			newField.Cells[8, 6].SetNotFinal(field.MaxValue, 3, 4, 5, 9);
			newField.Cells[8, 7].SetFinal(6);
			newField.Cells[8, 8].SetNotFinal(field.MaxValue, 3, 4, 5, 8, 9);
		}
	}
}