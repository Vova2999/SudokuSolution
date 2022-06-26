using Ninject.Modules;
using SudokuSolution.Console.ConsoleGameProvider;
using SudokuSolution.Logic.FieldActions.CleanPossibleByColumn;
using SudokuSolution.Logic.FieldActions.CleanPossibleByFinal;
using SudokuSolution.Logic.FieldActions.CleanPossibleByRow;
using SudokuSolution.Logic.FieldActions.SetFinalForColumn;
using SudokuSolution.Logic.FieldActions.SetFinalForRow;
using SudokuSolution.Logic.FieldActions.SetFinalForSinglePossible;
using SudokuSolution.Logic.FieldActions.SetFinalForSquare;
using SudokuSolution.Logic.FieldActions.SetRandomFinalAndSplitField;
using SudokuSolution.Logic.FieldService;
using SudokuSolution.Logic.GameService;

namespace SudokuSolution.Console {
	public class Module : NinjectModule {
		public override void Load() {
			Kernel?.Bind<IConsoleGameProvider>().To<ConsoleGameProvider.ConsoleGameProvider>();

			Kernel?.Bind<IGameService>().To<GameService>();
			Kernel?.Bind<IFieldService>().To<FieldService>();
			Kernel?.Bind<ICleanPossibleByFinal>().To<CleanPossibleByFinal>();
			Kernel?.Bind<ICleanPossibleByRow>().To<CleanPossibleByRow>();
			Kernel?.Bind<ICleanPossibleByColumn>().To<CleanPossibleByColumn>();
			Kernel?.Bind<ISetFinalForSinglePossible>().To<SetFinalForSinglePossible>();
			Kernel?.Bind<ISetFinalForRow>().To<SetFinalForRow>();
			Kernel?.Bind<ISetFinalForColumn>().To<SetFinalForColumn>();
			Kernel?.Bind<ISetFinalForSquare>().To<SetFinalForSquare>();
			Kernel?.Bind<ISetRandomFinalAndSplitField>().To<SetRandomFinalAndSplitField>();
		}
	}
}