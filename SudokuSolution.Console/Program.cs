using System.Linq;
using Ninject;
using SudokuSolution.Console.ConsoleGameProvider;

namespace SudokuSolution.Console {
	public static class Program {
		public static void Main(string[] args) {
			var container = new StandardKernel(new Module());
			container.Get<IConsoleGameProvider>().Start(args.FirstOrDefault());
		}
	}
}