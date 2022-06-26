using System.Linq;
using Ninject;
using SudokuSolution.Console.ConsoleGameProvider;

namespace SudokuSolution.Console {
	public static class Program {
		public static void Main(string[] args) {
			var container = new StandardKernel(new Module());
			container.Get<IConsoleGameProvider>().Start(args.FirstOrDefault());

			System.Console.WriteLine("Нажмите любую клавишу для завершения работы");
			System.Console.ReadKey();
		}
	}
}