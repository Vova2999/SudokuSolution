using System.Windows;
using GalaSoft.MvvmLight;

namespace SudokuSolution.Wpf.Common.Base {
	public abstract class ViewModel : ViewModelBase, IViewModel {
		private FrameworkElement view;

		public abstract object Header { get; }

		public virtual FrameworkElement View {
			get => view;
			set {
				if (Set(ref view, value) && view != null)
					view.DataContext = this;
			}
		}
	}
}