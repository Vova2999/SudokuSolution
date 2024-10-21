using System.Windows;

namespace SudokuSolution.Wpf.Common.Base;

public interface IViewModel
{
	object Header { get; }
	FrameworkElement View { get; }
}