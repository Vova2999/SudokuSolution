using System;
using System.Linq;

namespace SudokuSolution.Domain.Entities {
	public struct Cell {
		private int? final;
		private int maxValue;
		private bool[] possible;

		public bool HasFinal => final.HasValue;

		public int Final {
			get {
				if (!final.HasValue)
					throw new InvalidOperationException("Final value missing");

				return final.Value;
			}
			set {
				if (value <= 0 || value > maxValue)
					throw new InvalidOperationException("Final value is invalid");

				final = value;
			}
		}

		public bool this[int number] {
			get => possible[number - 1];
			set => possible[number - 1] = value;
		}

		internal void SetMaxValue(int value) {
			maxValue = value;
			possible = Enumerable.Repeat(true, value).ToArray();
		}
	}
}