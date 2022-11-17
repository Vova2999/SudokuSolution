using System.Collections.Generic;
using SudokuSolution.Domain.Entities;
using SudokuSolution.Wpf.Common.MessageBox;

namespace SudokuSolution.Wpf.Views.Solved {
	public class DemoModel : SolvedViewModel {
		public DemoModel() : base(
			DemoLocator.Locate<IMessageBoxService>(),
			new Controls.Field.DemoModel(),
			new Field(1),
			new List<Field>(),
			false) {
			CurrentSolved = 5;
			TotalSolvedCount = 20;
		}
	}
}