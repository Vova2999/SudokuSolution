using System.Collections.Generic;
using System.Linq;
using SudokuSolution.Logic.FieldActions;

namespace SudokuSolution.Logic.Extensions {
	public static class FieldActionsResultExtensions {
		public static FieldActionsResult GetChangedResultIfAnyIsChanged(this IEnumerable<FieldActionsResult> results) {
			return (results is ICollection<FieldActionsResult> ? results : results.ToArray())
				.Any(result => result == FieldActionsResult.Changed)
					? FieldActionsResult.Changed
					: FieldActionsResult.Nothing;
		}
	}
}