using SudokuSolution.Domain.Entities;

namespace SudokuSolution.Logic.FieldActions.SetFinalValueByRow {
	public interface ISetFinalValueByRow {
		void Execute(Field field);
	}
}