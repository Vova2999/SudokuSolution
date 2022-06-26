using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using SudokuSolution.Common.Extensions;
using SudokuSolution.Logic.FieldService;
using SudokuSolution.Test.Helpers;

namespace SudokuSolution.Test.Logic {
	[TestFixture]
	public class FieldServiceTest {
		private IFieldService fieldService;

		[OneTimeSetUp]
		public void CreateService() {
			fieldService = new FieldService();
		}

		[Test]
		public void FieldSolvedTest() {
			var field = TestFieldHelper.GetSolvedSmallTestField();
			fieldService.IsSolved(field).Should().BeTrue();
		}

		[Test]
		public void FieldNotSolvedTest() {
			var field = TestFieldHelper.GetSmallTestFieldWithPossible();
			fieldService.IsSolved(field).Should().BeFalse();
		}

		[Test]
		public void FieldFailedOnSolvedTest() {
			var field = TestFieldHelper.GetSolvedSmallTestField();
			fieldService.IsFailed(field).Should().BeFalse();
		}

		[Test]
		public void FieldFailedTest() {
			var field = TestFieldHelper.GetSmallTestFieldWithPossible();
			Enumerable.Range(1, field.MaxValue).ForEach(value => field.Cells[0, 1][value] = false);
			fieldService.IsFailed(field).Should().BeTrue();
		}

		[Test]
		public void FieldNotFailedTest() {
			var field = TestFieldHelper.GetSmallTestFieldWithPossible();
			fieldService.IsFailed(field).Should().BeFalse();
		}
	}
}