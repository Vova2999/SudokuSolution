using System.Collections.Generic;
using SudokuSolution.Domain.Entities;
using SudokuSolution.Wpf.Common.MessageBox;

namespace SudokuSolution.Wpf.Views.Solved {
	public class DemoModel : SolvedViewModel {
		public DemoModel() : base(
			Locator.Current.Locate<IMessageBoxService>(),
			new Controls.Field.DemoModel(),
			new List<Field>(),
			false) {
			CurrentSolved = 5;
			TotalSolvedCount = 20;
		}
	}
}