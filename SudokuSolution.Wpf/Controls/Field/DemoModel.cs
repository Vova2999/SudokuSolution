using System.Linq;
using GalaSoft.MvvmLight.Messaging;

namespace SudokuSolution.Wpf.Controls.Field {
	public class DemoModel : FieldViewModel {
		public DemoModel() : base(Locator.Current.Locate<IMessenger>()) {
			Size = Constants.StartedSize;
			Cells = Enumerable.Range(1, Size * Size)
				.Select(_ => Enumerable.Range(1, Size * Size)
					.Select(x => new CellModel { Value = x })
					.ToArray())
				.ToArray();
		}
	}
}