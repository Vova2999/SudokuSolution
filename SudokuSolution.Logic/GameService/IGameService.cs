using System.Collections.Generic;
using SudokuSolution.Domain.Entities;

namespace SudokuSolution.Logic.GameService {
	public interface IGameService {
		IEnumerable<Field> Solve(Field field);
		FieldEnumeratorAsync StartSolve(Field field);
	}
}