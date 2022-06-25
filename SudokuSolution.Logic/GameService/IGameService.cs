using SudokuSolution.Domain.Entities;

namespace SudokuSolution.Logic.GameService {
	public interface IGameService {
		Field[] Solve(Field game);

		// 1. Заполнить Possible пустых ячеек // FillPossibleValues
		// 2. Пройтись по имеющимся Final и убрать лишние Possible // CleanPossibleValues
		// 3. В каждом квадрате выставить те ячейки, значения которых возможны только в одной ячейке // SetFinalValueBySquare
		// 4. В каждой строке выставить те ячейки, значения которых возможны только в одной строке // SetFinalValueByRow
		// 5. В каждом столбце выставить те ячейки, значения которых возможны только в одном столбце // SetFinalValueByColumn
		// 6. Повторять 2-5 пункты, пока они меняют доску
		// 7. Клонировать доску и зарандомить любую нерешенную ячейку путем поиска в глубину (решений может быть несколько) // SetRandomFinalValueAndSplitField
		// 8. Повторять 2-7 пункты, пока не будут найдены все решения
	}
}