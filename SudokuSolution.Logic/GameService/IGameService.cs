using SudokuSolution.Domain.Entities;

namespace SudokuSolution.Logic.GameService {
	public interface IGameService {
		Field[] Solve(Field game);

		// Заполнить Possible пустых ячеек //
		// Пройтись по имеющимся Final и убрать лишние Possible // CleanPossibleByFinal
		// CleanPossibleByRow
		// CleanPossibleByColumn
		// Выставить те ячейки, в которых только один Possible // SetFinalValueForSinglePossible
		// В каждом квадрате выставить те ячейки, значения которых возможны только в одной ячейке // SetFinalForSquare
		// В каждой строке выставить те ячейки, значения которых возможны только в одной строке // SetFinalForRow
		// В каждом столбце выставить те ячейки, значения которых возможны только в одном столбце // SetFinalForColumn
		// Повторять 2-5 пункты, пока они меняют доску
		// Клонировать доску и зарандомить любую нерешенную ячейку путем поиска в глубину (решений может быть несколько) // SetRandomFinalValueAndSplitField
		// Повторять 2-7 пункты, пока не будут найдены все решения
	}
}