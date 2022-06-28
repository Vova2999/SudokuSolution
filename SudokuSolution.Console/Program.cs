using System.Linq;
using SudokuSolution.Console.ConsoleGameProvider;

namespace SudokuSolution.Console {
	public static class Program {
		public static void Main(string[] args) {
			Locator.Current.Locate<IConsoleGameProvider>().Start(args.FirstOrDefault());

			System.Console.WriteLine("Нажмите любую клавишу для завершения работы");
			System.Console.ReadKey();
		}
	}
}