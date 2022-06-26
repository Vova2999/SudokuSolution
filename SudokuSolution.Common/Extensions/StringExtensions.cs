using JetBrains.Annotations;

namespace SudokuSolution.Common.Extensions {
	public static class StringExtensions {
		[ContractAnnotation("null => true")]
		public static bool IsNullOrEmpty(this string str) {
			return string.IsNullOrEmpty(str);
		}

		[ContractAnnotation("null => true")]
		public static bool IsNullOrWhiteSpace(this string str) {
			return string.IsNullOrWhiteSpace(str);
		}

		[ContractAnnotation("null => false")]
		public static bool IsSignificant(this string str) {
			return !string.IsNullOrEmpty(str);
		}
	}
}