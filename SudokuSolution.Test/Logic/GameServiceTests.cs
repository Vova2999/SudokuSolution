﻿using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using SudokuSolution.Domain.Entities;
using SudokuSolution.Logic.FieldActions.CleanPossible;
using SudokuSolution.Logic.FieldActions.CleanPossible.CleanPossibleByColumn;
using SudokuSolution.Logic.FieldActions.CleanPossible.CleanPossibleByFinal;
using SudokuSolution.Logic.FieldActions.CleanPossible.CleanPossibleByRow;
using SudokuSolution.Logic.FieldActions.SetFinal;
using SudokuSolution.Logic.FieldActions.SetFinal.SetFinalForColumn;
using SudokuSolution.Logic.FieldActions.SetFinal.SetFinalForRow;
using SudokuSolution.Logic.FieldActions.SetFinal.SetFinalForSinglePossible;
using SudokuSolution.Logic.FieldActions.SetFinal.SetFinalForSquare;
using SudokuSolution.Logic.FieldActions.SetRandomFinalAndSplitField;
using SudokuSolution.Logic.FieldService;
using SudokuSolution.Logic.GameService;
using SudokuSolution.Test.Extensions;
using SudokuSolution.Test.Helpers;

namespace SudokuSolution.Test.Logic;

[TestFixture]
public class GameServiceTests
{
	private static readonly Field[] Fields = { TestFieldHelper.GetTestField(), TestFieldHelper.GetTestFieldWithPossible() };
	private static readonly Field[] SmallFields = { TestFieldHelper.GetSmallTestField(), TestFieldHelper.GetSmallTestFieldWithPossible(), TestFieldHelper.GetSolvedSmallTestField() };

	private IGameService _gameService;

	[OneTimeSetUp]
	public void CreateService()
	{
		var cleanPossibleFacade = new CleanPossibleFacade(new CleanPossibleByRow(), new CleanPossibleByFinal(), new CleanPossibleByColumn());

		_gameService = new GameService(
			new FieldService(),
			new SetFinalFacade(
				new SetFinalForRow(cleanPossibleFacade),
				new SetFinalForColumn(cleanPossibleFacade),
				new SetFinalForSquare(cleanPossibleFacade),
				new SetFinalForSinglePossible(cleanPossibleFacade)),
			cleanPossibleFacade,
			new SetRandomFinalAndSplitField());
	}

	[Test]
	[TestCaseSource(nameof(SmallFields))]
	public void SolveSmallFieldTest(Field field)
	{
		var solvedFields = _gameService.Solve(field).ToArray();
		solvedFields.Length.Should().Be(1);

		var solvedField = solvedFields.Single();
		solvedField.Cells[0, 0].ShouldBeFinal(2);
		solvedField.Cells[0, 1].ShouldBeFinal(3);
		solvedField.Cells[0, 2].ShouldBeFinal(4);
		solvedField.Cells[0, 3].ShouldBeFinal(1);
		solvedField.Cells[1, 0].ShouldBeFinal(1);
		solvedField.Cells[1, 1].ShouldBeFinal(4);
		solvedField.Cells[1, 2].ShouldBeFinal(3);
		solvedField.Cells[1, 3].ShouldBeFinal(2);
		solvedField.Cells[2, 0].ShouldBeFinal(4);
		solvedField.Cells[2, 1].ShouldBeFinal(2);
		solvedField.Cells[2, 2].ShouldBeFinal(1);
		solvedField.Cells[2, 3].ShouldBeFinal(3);
		solvedField.Cells[3, 0].ShouldBeFinal(3);
		solvedField.Cells[3, 1].ShouldBeFinal(1);
		solvedField.Cells[3, 2].ShouldBeFinal(2);
		solvedField.Cells[3, 3].ShouldBeFinal(4);
	}

	[Test]
	[TestCaseSource(nameof(Fields))]
	public void SolveFieldTest(Field field)
	{
		var solvedFields = _gameService.Solve(field).ToArray();
		solvedFields.Length.Should().Be(1);

		var solvedField = solvedFields.Single();
		solvedField.Cells[0, 0].ShouldBeFinal(5);
		solvedField.Cells[0, 1].ShouldBeFinal(8);
		solvedField.Cells[0, 2].ShouldBeFinal(4);
		solvedField.Cells[0, 3].ShouldBeFinal(9);
		solvedField.Cells[0, 4].ShouldBeFinal(3);
		solvedField.Cells[0, 5].ShouldBeFinal(6);
		solvedField.Cells[0, 6].ShouldBeFinal(1);
		solvedField.Cells[0, 7].ShouldBeFinal(2);
		solvedField.Cells[0, 8].ShouldBeFinal(7);
		solvedField.Cells[1, 0].ShouldBeFinal(9);
		solvedField.Cells[1, 1].ShouldBeFinal(1);
		solvedField.Cells[1, 2].ShouldBeFinal(2);
		solvedField.Cells[1, 3].ShouldBeFinal(4);
		solvedField.Cells[1, 4].ShouldBeFinal(7);
		solvedField.Cells[1, 5].ShouldBeFinal(5);
		solvedField.Cells[1, 6].ShouldBeFinal(8);
		solvedField.Cells[1, 7].ShouldBeFinal(3);
		solvedField.Cells[1, 8].ShouldBeFinal(6);
		solvedField.Cells[2, 0].ShouldBeFinal(6);
		solvedField.Cells[2, 1].ShouldBeFinal(3);
		solvedField.Cells[2, 2].ShouldBeFinal(7);
		solvedField.Cells[2, 3].ShouldBeFinal(2);
		solvedField.Cells[2, 4].ShouldBeFinal(8);
		solvedField.Cells[2, 5].ShouldBeFinal(1);
		solvedField.Cells[2, 6].ShouldBeFinal(9);
		solvedField.Cells[2, 7].ShouldBeFinal(4);
		solvedField.Cells[2, 8].ShouldBeFinal(5);
		solvedField.Cells[3, 0].ShouldBeFinal(7);
		solvedField.Cells[3, 1].ShouldBeFinal(5);
		solvedField.Cells[3, 2].ShouldBeFinal(1);
		solvedField.Cells[3, 3].ShouldBeFinal(6);
		solvedField.Cells[3, 4].ShouldBeFinal(4);
		solvedField.Cells[3, 5].ShouldBeFinal(2);
		solvedField.Cells[3, 6].ShouldBeFinal(3);
		solvedField.Cells[3, 7].ShouldBeFinal(9);
		solvedField.Cells[3, 8].ShouldBeFinal(8);
		solvedField.Cells[4, 0].ShouldBeFinal(8);
		solvedField.Cells[4, 1].ShouldBeFinal(6);
		solvedField.Cells[4, 2].ShouldBeFinal(9);
		solvedField.Cells[4, 3].ShouldBeFinal(7);
		solvedField.Cells[4, 4].ShouldBeFinal(1);
		solvedField.Cells[4, 5].ShouldBeFinal(3);
		solvedField.Cells[4, 6].ShouldBeFinal(4);
		solvedField.Cells[4, 7].ShouldBeFinal(5);
		solvedField.Cells[4, 8].ShouldBeFinal(2);
		solvedField.Cells[5, 0].ShouldBeFinal(2);
		solvedField.Cells[5, 1].ShouldBeFinal(4);
		solvedField.Cells[5, 2].ShouldBeFinal(3);
		solvedField.Cells[5, 3].ShouldBeFinal(5);
		solvedField.Cells[5, 4].ShouldBeFinal(9);
		solvedField.Cells[5, 5].ShouldBeFinal(8);
		solvedField.Cells[5, 6].ShouldBeFinal(6);
		solvedField.Cells[5, 7].ShouldBeFinal(7);
		solvedField.Cells[5, 8].ShouldBeFinal(1);
		solvedField.Cells[6, 0].ShouldBeFinal(4);
		solvedField.Cells[6, 1].ShouldBeFinal(7);
		solvedField.Cells[6, 2].ShouldBeFinal(6);
		solvedField.Cells[6, 3].ShouldBeFinal(1);
		solvedField.Cells[6, 4].ShouldBeFinal(5);
		solvedField.Cells[6, 5].ShouldBeFinal(9);
		solvedField.Cells[6, 6].ShouldBeFinal(2);
		solvedField.Cells[6, 7].ShouldBeFinal(8);
		solvedField.Cells[6, 8].ShouldBeFinal(3);
		solvedField.Cells[7, 0].ShouldBeFinal(3);
		solvedField.Cells[7, 1].ShouldBeFinal(2);
		solvedField.Cells[7, 2].ShouldBeFinal(5);
		solvedField.Cells[7, 3].ShouldBeFinal(8);
		solvedField.Cells[7, 4].ShouldBeFinal(6);
		solvedField.Cells[7, 5].ShouldBeFinal(4);
		solvedField.Cells[7, 6].ShouldBeFinal(7);
		solvedField.Cells[7, 7].ShouldBeFinal(1);
		solvedField.Cells[7, 8].ShouldBeFinal(9);
		solvedField.Cells[8, 0].ShouldBeFinal(1);
		solvedField.Cells[8, 1].ShouldBeFinal(9);
		solvedField.Cells[8, 2].ShouldBeFinal(8);
		solvedField.Cells[8, 3].ShouldBeFinal(3);
		solvedField.Cells[8, 4].ShouldBeFinal(2);
		solvedField.Cells[8, 5].ShouldBeFinal(7);
		solvedField.Cells[8, 6].ShouldBeFinal(5);
		solvedField.Cells[8, 7].ShouldBeFinal(6);
		solvedField.Cells[8, 8].ShouldBeFinal(4);
	}

	[Test]
	public void SolveHardFieldTest()
	{
		var field = TestFieldHelper.GetHardTestField();
		var solvedFields = _gameService.Solve(field).ToArray();
		solvedFields.Length.Should().Be(1);

		var solvedField = solvedFields.Single();
		solvedField.Cells[0, 0].ShouldBeFinal(7);
		solvedField.Cells[0, 1].ShouldBeFinal(8);
		solvedField.Cells[0, 2].ShouldBeFinal(5);
		solvedField.Cells[0, 3].ShouldBeFinal(4);
		solvedField.Cells[0, 4].ShouldBeFinal(3);
		solvedField.Cells[0, 5].ShouldBeFinal(9);
		solvedField.Cells[0, 6].ShouldBeFinal(1);
		solvedField.Cells[0, 7].ShouldBeFinal(6);
		solvedField.Cells[0, 8].ShouldBeFinal(2);
		solvedField.Cells[1, 0].ShouldBeFinal(3);
		solvedField.Cells[1, 1].ShouldBeFinal(1);
		solvedField.Cells[1, 2].ShouldBeFinal(4);
		solvedField.Cells[1, 3].ShouldBeFinal(6);
		solvedField.Cells[1, 4].ShouldBeFinal(8);
		solvedField.Cells[1, 5].ShouldBeFinal(2);
		solvedField.Cells[1, 6].ShouldBeFinal(5);
		solvedField.Cells[1, 7].ShouldBeFinal(7);
		solvedField.Cells[1, 8].ShouldBeFinal(9);
		solvedField.Cells[2, 0].ShouldBeFinal(9);
		solvedField.Cells[2, 1].ShouldBeFinal(2);
		solvedField.Cells[2, 2].ShouldBeFinal(6);
		solvedField.Cells[2, 3].ShouldBeFinal(7);
		solvedField.Cells[2, 4].ShouldBeFinal(5);
		solvedField.Cells[2, 5].ShouldBeFinal(1);
		solvedField.Cells[2, 6].ShouldBeFinal(8);
		solvedField.Cells[2, 7].ShouldBeFinal(4);
		solvedField.Cells[2, 8].ShouldBeFinal(3);
		solvedField.Cells[3, 0].ShouldBeFinal(1);
		solvedField.Cells[3, 1].ShouldBeFinal(4);
		solvedField.Cells[3, 2].ShouldBeFinal(7);
		solvedField.Cells[3, 3].ShouldBeFinal(3);
		solvedField.Cells[3, 4].ShouldBeFinal(2);
		solvedField.Cells[3, 5].ShouldBeFinal(5);
		solvedField.Cells[3, 6].ShouldBeFinal(6);
		solvedField.Cells[3, 7].ShouldBeFinal(9);
		solvedField.Cells[3, 8].ShouldBeFinal(8);
		solvedField.Cells[4, 0].ShouldBeFinal(6);
		solvedField.Cells[4, 1].ShouldBeFinal(9);
		solvedField.Cells[4, 2].ShouldBeFinal(2);
		solvedField.Cells[4, 3].ShouldBeFinal(1);
		solvedField.Cells[4, 4].ShouldBeFinal(7);
		solvedField.Cells[4, 5].ShouldBeFinal(8);
		solvedField.Cells[4, 6].ShouldBeFinal(3);
		solvedField.Cells[4, 7].ShouldBeFinal(5);
		solvedField.Cells[4, 8].ShouldBeFinal(4);
		solvedField.Cells[5, 0].ShouldBeFinal(8);
		solvedField.Cells[5, 1].ShouldBeFinal(5);
		solvedField.Cells[5, 2].ShouldBeFinal(3);
		solvedField.Cells[5, 3].ShouldBeFinal(9);
		solvedField.Cells[5, 4].ShouldBeFinal(4);
		solvedField.Cells[5, 5].ShouldBeFinal(6);
		solvedField.Cells[5, 6].ShouldBeFinal(2);
		solvedField.Cells[5, 7].ShouldBeFinal(1);
		solvedField.Cells[5, 8].ShouldBeFinal(7);
		solvedField.Cells[6, 0].ShouldBeFinal(4);
		solvedField.Cells[6, 1].ShouldBeFinal(3);
		solvedField.Cells[6, 2].ShouldBeFinal(8);
		solvedField.Cells[6, 3].ShouldBeFinal(5);
		solvedField.Cells[6, 4].ShouldBeFinal(6);
		solvedField.Cells[6, 5].ShouldBeFinal(7);
		solvedField.Cells[6, 6].ShouldBeFinal(9);
		solvedField.Cells[6, 7].ShouldBeFinal(2);
		solvedField.Cells[6, 8].ShouldBeFinal(1);
		solvedField.Cells[7, 0].ShouldBeFinal(5);
		solvedField.Cells[7, 1].ShouldBeFinal(7);
		solvedField.Cells[7, 2].ShouldBeFinal(9);
		solvedField.Cells[7, 3].ShouldBeFinal(2);
		solvedField.Cells[7, 4].ShouldBeFinal(1);
		solvedField.Cells[7, 5].ShouldBeFinal(3);
		solvedField.Cells[7, 6].ShouldBeFinal(4);
		solvedField.Cells[7, 7].ShouldBeFinal(8);
		solvedField.Cells[7, 8].ShouldBeFinal(6);
		solvedField.Cells[8, 0].ShouldBeFinal(2);
		solvedField.Cells[8, 1].ShouldBeFinal(6);
		solvedField.Cells[8, 2].ShouldBeFinal(1);
		solvedField.Cells[8, 3].ShouldBeFinal(8);
		solvedField.Cells[8, 4].ShouldBeFinal(9);
		solvedField.Cells[8, 5].ShouldBeFinal(4);
		solvedField.Cells[8, 6].ShouldBeFinal(7);
		solvedField.Cells[8, 7].ShouldBeFinal(3);
		solvedField.Cells[8, 8].ShouldBeFinal(5);
	}

	[Test]
	public void SolveNotCompleteSmallFieldTest()
	{
		var solvedFields = _gameService.Solve(TestFieldHelper.GetNotCompleteSmallTestField()).ToArray();
		solvedFields.Length.Should().Be(3);

		solvedFields[0].Cells[0, 0].ShouldBeFinal(1);
		solvedFields[0].Cells[0, 1].ShouldBeFinal(3);
		solvedFields[0].Cells[0, 2].ShouldBeFinal(4);
		solvedFields[0].Cells[0, 3].ShouldBeFinal(2);
		solvedFields[0].Cells[1, 0].ShouldBeFinal(2);
		solvedFields[0].Cells[1, 1].ShouldBeFinal(4);
		solvedFields[0].Cells[1, 2].ShouldBeFinal(3);
		solvedFields[0].Cells[1, 3].ShouldBeFinal(1);
		solvedFields[0].Cells[2, 0].ShouldBeFinal(4);
		solvedFields[0].Cells[2, 1].ShouldBeFinal(2);
		solvedFields[0].Cells[2, 2].ShouldBeFinal(1);
		solvedFields[0].Cells[2, 3].ShouldBeFinal(3);
		solvedFields[0].Cells[3, 0].ShouldBeFinal(3);
		solvedFields[0].Cells[3, 1].ShouldBeFinal(1);
		solvedFields[0].Cells[3, 2].ShouldBeFinal(2);
		solvedFields[0].Cells[3, 3].ShouldBeFinal(4);

		solvedFields[1].Cells[0, 0].ShouldBeFinal(1);
		solvedFields[1].Cells[0, 1].ShouldBeFinal(3);
		solvedFields[1].Cells[0, 2].ShouldBeFinal(4);
		solvedFields[1].Cells[0, 3].ShouldBeFinal(2);
		solvedFields[1].Cells[1, 0].ShouldBeFinal(4);
		solvedFields[1].Cells[1, 1].ShouldBeFinal(2);
		solvedFields[1].Cells[1, 2].ShouldBeFinal(3);
		solvedFields[1].Cells[1, 3].ShouldBeFinal(1);
		solvedFields[1].Cells[2, 0].ShouldBeFinal(2);
		solvedFields[1].Cells[2, 1].ShouldBeFinal(4);
		solvedFields[1].Cells[2, 2].ShouldBeFinal(1);
		solvedFields[1].Cells[2, 3].ShouldBeFinal(3);
		solvedFields[1].Cells[3, 0].ShouldBeFinal(3);
		solvedFields[1].Cells[3, 1].ShouldBeFinal(1);
		solvedFields[1].Cells[3, 2].ShouldBeFinal(2);
		solvedFields[1].Cells[3, 3].ShouldBeFinal(4);

		solvedFields[2].Cells[0, 0].ShouldBeFinal(2);
		solvedFields[2].Cells[0, 1].ShouldBeFinal(3);
		solvedFields[2].Cells[0, 2].ShouldBeFinal(4);
		solvedFields[2].Cells[0, 3].ShouldBeFinal(1);
		solvedFields[2].Cells[1, 0].ShouldBeFinal(1);
		solvedFields[2].Cells[1, 1].ShouldBeFinal(4);
		solvedFields[2].Cells[1, 2].ShouldBeFinal(3);
		solvedFields[2].Cells[1, 3].ShouldBeFinal(2);
		solvedFields[2].Cells[2, 0].ShouldBeFinal(4);
		solvedFields[2].Cells[2, 1].ShouldBeFinal(2);
		solvedFields[2].Cells[2, 2].ShouldBeFinal(1);
		solvedFields[2].Cells[2, 3].ShouldBeFinal(3);
		solvedFields[2].Cells[3, 0].ShouldBeFinal(3);
		solvedFields[2].Cells[3, 1].ShouldBeFinal(1);
		solvedFields[2].Cells[3, 2].ShouldBeFinal(2);
		solvedFields[2].Cells[3, 3].ShouldBeFinal(4);
	}

	[Test]
	[TestCaseSource(nameof(SmallFields))]
	public void SolveShouldBeNotChangingStartField(Field field)
	{
		var startField = (Field) field.Clone();
		_gameService.Solve(field);

		field.Equals(startField).Should().BeTrue();
	}
}