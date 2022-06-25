using SudokuSolution.Domain.Entities;

namespace SudokuSolution.Logic.FieldActions.SetFinalValueByColumn {
	public interface ISetFinalValueByColumn {
		void Execute(Field field);
	}
}