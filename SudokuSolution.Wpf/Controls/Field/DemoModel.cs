using System.Linq;

namespace SudokuSolution.Wpf.Controls.Field {
	public class DemoModel : FieldViewModel {
		public DemoModel() {
			Size = 3;
			Cells = Enumerable.Range(1, Size * Size).Select(_ => Enumerable.Range(1, Size * Size).Select(x => new CellModel { Value = x }).ToArray()).ToArray();
		}
	}
}