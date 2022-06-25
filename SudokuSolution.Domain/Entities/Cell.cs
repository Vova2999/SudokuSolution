using System;
using System.Linq;

namespace SudokuSolution.Domain.Entities {
	public class Cell {
		private int? final;
		private readonly int maxValue;
		private readonly bool[] possible;

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
			set {
				if (!final.HasValue)
					possible[number - 1] = value;
			}
		}

		internal Cell(int newMaxValue) {
			maxValue = newMaxValue;
			possible = Enumerable.Repeat(true, newMaxValue).ToArray();
		}
	}
}