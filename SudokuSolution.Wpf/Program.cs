using System;

namespace SudokuSolution.Wpf;

public class Program
{
	[STAThread]
	private static void Main(string[] args)
	{
		Locator.Current.Locate<App>().Run();
	}
}