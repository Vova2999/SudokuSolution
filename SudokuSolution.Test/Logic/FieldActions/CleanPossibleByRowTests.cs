﻿using NUnit.Framework;
using SudokuSolution.Logic.FieldActions.CleanPossibleByRow;
using SudokuSolution.Test.Extensions;
using SudokuSolution.Test.Helpers;

namespace SudokuSolution.Test.Logic.FieldActions {
	[TestFixture]
	public class CleanPossibleByRowTests {
		private ICleanPossibleByRow cleanPossibleByRow;

		[OneTimeSetUp]
		public void CreateService() {
			cleanPossibleByRow = new CleanPossibleByRow();
		}

		[Test]
		public void ExecuteWithSmallFieldTest() {
			var field = TestFieldHelper.GetSmallTestFieldWithPossible();
			cleanPossibleByRow.Execute(field);
			cleanPossibleByRow.Execute(field);
			cleanPossibleByRow.Execute(field);

			field.Cells[0, 0].ShouldBeFinal(2);
			field.Cells[0, 1].ShouldBeNotFinal(field.MaxValue, 3);
			field.Cells[0, 2].ShouldBeNotFinal(field.MaxValue, 1, 4);
			field.Cells[0, 3].ShouldBeNotFinal(field.MaxValue, 1);
			field.Cells[1, 0].ShouldBeNotFinal(field.MaxValue, 1, 4);
			field.Cells[1, 1].ShouldBeNotFinal(field.MaxValue, 4);
			field.Cells[1, 2].ShouldBeFinal(3);
			field.Cells[1, 3].ShouldBeNotFinal(field.MaxValue, 2);
			field.Cells[2, 0].ShouldBeNotFinal(field.MaxValue, 4);
			field.Cells[2, 1].ShouldBeNotFinal(field.MaxValue, 2, 4);
			field.Cells[2, 2].ShouldBeNotFinal(field.MaxValue, 1);
			field.Cells[2, 3].ShouldBeNotFinal(field.MaxValue, 1, 3);
			field.Cells[3, 0].ShouldBeNotFinal(field.MaxValue, 3);
			field.Cells[3, 1].ShouldBeFinal(1);
			field.Cells[3, 2].ShouldBeNotFinal(field.MaxValue, 2);
			field.Cells[3, 3].ShouldBeFinal(4);
		}

		[Test]
		public void ExecuteWithFieldTest() {
			var field = TestFieldHelper.GetTestFieldWithPossible();
			cleanPossibleByRow.Execute(field);
			cleanPossibleByRow.Execute(field);
			cleanPossibleByRow.Execute(field);

			field.Cells[0, 0].ShouldBeNotFinal(field.MaxValue, 2, 4, 5, 6);
			field.Cells[0, 1].ShouldBeFinal(8);
			field.Cells[0, 2].ShouldBeNotFinal(field.MaxValue, 2, 4, 6);
			field.Cells[0, 3].ShouldBeFinal(9);
			field.Cells[0, 4].ShouldBeFinal(3);
			field.Cells[0, 5].ShouldBeNotFinal(field.MaxValue, 1, 6);
			field.Cells[0, 6].ShouldBeNotFinal(field.MaxValue, 1, 2, 5);
			field.Cells[0, 7].ShouldBeNotFinal(field.MaxValue, 1, 2, 5);
			field.Cells[0, 8].ShouldBeFinal(7);
			field.Cells[1, 0].ShouldBeNotFinal(field.MaxValue, 2, 3, 6, 7, 9);
			field.Cells[1, 1].ShouldBeNotFinal(field.MaxValue, 1, 2, 3, 6, 9);
			field.Cells[1, 2].ShouldBeNotFinal(field.MaxValue, 2, 3, 6, 7, 9);
			field.Cells[1, 3].ShouldBeFinal(4);
			field.Cells[1, 4].ShouldBeNotFinal(field.MaxValue, 6, 7);
			field.Cells[1, 5].ShouldBeFinal(5);
			field.Cells[1, 6].ShouldBeFinal(8);
			field.Cells[1, 7].ShouldBeNotFinal(field.MaxValue, 1, 2, 3);
			field.Cells[1, 8].ShouldBeNotFinal(field.MaxValue, 1, 3, 6, 9);
			field.Cells[2, 0].ShouldBeNotFinal(field.MaxValue, 3, 5, 6, 7, 9);
			field.Cells[2, 1].ShouldBeNotFinal(field.MaxValue, 1, 3, 5, 6, 9);
			field.Cells[2, 2].ShouldBeNotFinal(field.MaxValue, 3, 6, 7, 9);
			field.Cells[2, 3].ShouldBeNotFinal(field.MaxValue, 1, 2, 6, 7);
			field.Cells[2, 4].ShouldBeNotFinal(field.MaxValue, 6, 7, 8);
			field.Cells[2, 5].ShouldBeNotFinal(field.MaxValue, 1, 6, 8);
			field.Cells[2, 6].ShouldBeNotFinal(field.MaxValue, 1, 3, 5, 9);
			field.Cells[2, 7].ShouldBeFinal(4);
			field.Cells[2, 8].ShouldBeNotFinal(field.MaxValue, 1, 3, 5, 6, 9);
			field.Cells[3, 0].ShouldBeNotFinal(field.MaxValue, 3, 5, 6, 7);
			field.Cells[3, 1].ShouldBeNotFinal(field.MaxValue, 3, 5, 6);
			field.Cells[3, 2].ShouldBeFinal(1);
			field.Cells[3, 3].ShouldBeNotFinal(field.MaxValue, 3, 6, 7);
			field.Cells[3, 4].ShouldBeNotFinal(field.MaxValue, 4, 6, 7, 8);
			field.Cells[3, 5].ShouldBeFinal(2);
			field.Cells[3, 6].ShouldBeNotFinal(field.MaxValue, 3, 5, 7);
			field.Cells[3, 7].ShouldBeFinal(9);
			field.Cells[3, 8].ShouldBeNotFinal(field.MaxValue, 3, 5, 8);
			field.Cells[4, 0].ShouldBeFinal(8);
			field.Cells[4, 1].ShouldBeNotFinal(field.MaxValue, 3, 5, 6, 9);
			field.Cells[4, 2].ShouldBeNotFinal(field.MaxValue, 3, 6, 7, 9);
			field.Cells[4, 3].ShouldBeNotFinal(field.MaxValue, 3, 6, 7);
			field.Cells[4, 4].ShouldBeFinal(1);
			field.Cells[4, 5].ShouldBeNotFinal(field.MaxValue, 3, 6, 9);
			field.Cells[4, 6].ShouldBeNotFinal(field.MaxValue, 3, 4, 5, 7);
			field.Cells[4, 7].ShouldBeNotFinal(field.MaxValue, 3, 5, 7);
			field.Cells[4, 8].ShouldBeFinal(2);
			field.Cells[5, 0].ShouldBeNotFinal(field.MaxValue, 2, 3, 7, 9);
			field.Cells[5, 1].ShouldBeFinal(4);
			field.Cells[5, 2].ShouldBeNotFinal(field.MaxValue, 2, 3, 7, 9);
			field.Cells[5, 3].ShouldBeFinal(5);
			field.Cells[5, 4].ShouldBeNotFinal(field.MaxValue, 7, 8, 9);
			field.Cells[5, 5].ShouldBeNotFinal(field.MaxValue, 3, 8, 9);
			field.Cells[5, 6].ShouldBeFinal(6);
			field.Cells[5, 7].ShouldBeNotFinal(field.MaxValue, 1, 3, 7, 8);
			field.Cells[5, 8].ShouldBeNotFinal(field.MaxValue, 1, 3, 8);
			field.Cells[6, 0].ShouldBeNotFinal(field.MaxValue, 2, 3, 4, 6, 9);
			field.Cells[6, 1].ShouldBeFinal(7);
			field.Cells[6, 2].ShouldBeNotFinal(field.MaxValue, 2, 3, 4, 6, 8, 9);
			field.Cells[6, 3].ShouldBeNotFinal(field.MaxValue, 1, 3, 6);
			field.Cells[6, 4].ShouldBeNotFinal(field.MaxValue, 5, 6, 9);
			field.Cells[6, 5].ShouldBeNotFinal(field.MaxValue, 1, 6, 9);
			field.Cells[6, 6].ShouldBeNotFinal(field.MaxValue, 2, 3, 4, 9);
			field.Cells[6, 7].ShouldBeNotFinal(field.MaxValue, 2, 3, 8);
			field.Cells[6, 8].ShouldBeNotFinal(field.MaxValue, 3, 4, 8, 9);
			field.Cells[7, 0].ShouldBeNotFinal(field.MaxValue, 2, 3, 6, 9);
			field.Cells[7, 1].ShouldBeNotFinal(field.MaxValue, 2, 3, 6, 9);
			field.Cells[7, 2].ShouldBeFinal(5);
			field.Cells[7, 3].ShouldBeFinal(8);
			field.Cells[7, 4].ShouldBeNotFinal(field.MaxValue, 6, 9);
			field.Cells[7, 5].ShouldBeFinal(4);
			field.Cells[7, 6].ShouldBeNotFinal(field.MaxValue, 1, 2, 3, 7, 9);
			field.Cells[7, 7].ShouldBeNotFinal(field.MaxValue, 1, 2, 3, 7);
			field.Cells[7, 8].ShouldBeNotFinal(field.MaxValue, 1, 3, 9);
			field.Cells[8, 0].ShouldBeFinal(1);
			field.Cells[8, 1].ShouldBeNotFinal(field.MaxValue, 3, 9);
			field.Cells[8, 2].ShouldBeNotFinal(field.MaxValue, 3, 4, 8, 9);
			field.Cells[8, 3].ShouldBeNotFinal(field.MaxValue, 3);
			field.Cells[8, 4].ShouldBeFinal(2);
			field.Cells[8, 5].ShouldBeFinal(7);
			field.Cells[8, 6].ShouldBeNotFinal(field.MaxValue, 3, 4, 5, 9);
			field.Cells[8, 7].ShouldBeFinal(6);
			field.Cells[8, 8].ShouldBeNotFinal(field.MaxValue, 3, 4, 5, 8, 9);
		}
	}
}