using NUnit.Framework;
using SudokuSolution.Logic.FieldActions.CleanPossibleByFinal;
using SudokuSolution.Test.Helpers;

namespace SudokuSolution.Test.Logic.FieldActions {
	[TestFixture]
	public class CleanPossibleByFinalTests {
		private CleanPossibleByFinal cleanPossibleByFinal;

		[OneTimeSetUp]
		public void CreateService() {
			cleanPossibleByFinal = new CleanPossibleByFinal();
		}

		[Test]
		public void ExecuteWithFieldTest() {
			var field = TestFieldHelper.GetTestField();
			cleanPossibleByFinal.Execute(field);
		}
	}
}