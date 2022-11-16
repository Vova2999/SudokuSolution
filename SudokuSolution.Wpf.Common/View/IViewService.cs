using System;
using System.Windows;
using SudokuSolution.Wpf.Common.Base;

namespace SudokuSolution.Wpf.Common.View {
	public interface IViewService {
		void OpenWindow<TViewModel>() where TViewModel : IViewModel;
		void OpenWindow(IViewModel viewModel);

		bool? OpenDialog<TViewModel>() where TViewModel : IViewModel;
		[Obsolete("Use ViewModelExtensions")]
		bool? OpenDialog(IViewModel viewModel);

		Window CreateWindow<TViewModel>(WindowMode windowMode) where TViewModel : IViewModel;
		Window CreateWindow(IViewModel viewModel, WindowMode windowMode);

		int GetOpenedWindowsCount();
	}
}