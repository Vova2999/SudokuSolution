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
				for (var index = 0; index < maxValue; index++)
					possible[index] = false;
			}
		}

		public bool this[int number] {
			get => possible[number - 1];
			set => possible[number - 1] = value;
		}

		internal void SetMaxValue(int newMaxValue) {
			maxValue = newMaxValue;
			possible = Enumerable.Repeat(true, newMaxValue).ToArray();
		}
	}
}