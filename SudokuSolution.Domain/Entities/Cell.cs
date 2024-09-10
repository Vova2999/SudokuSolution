using System;
using System.Linq;

namespace SudokuSolution.Domain.Entities;

public class Cell : ICloneable
{
	private int? final;
	private readonly int maxValue;
	private readonly bool[] possible;

	public bool HasFinal => final.HasValue;

	public int Final
	{
		get
		{
			if (!final.HasValue)
				throw new InvalidOperationException("Final value missing");

			return final.Value;
		}
		set
		{
			if (value <= 0 || value > maxValue)
				throw new InvalidOperationException("Final value is invalid");

			final = value;
			for (var index = 0; index < maxValue; index++)
				possible[index] = false;
		}
	}

	public bool this[int number]
	{
		get => possible[number - 1];
		set
		{
			if (!final.HasValue)
				possible[number - 1] = value;
		}
	}

	internal Cell(int maxValue)
	{
		this.maxValue = maxValue;
		possible = Enumerable.Repeat(true, maxValue).ToArray();
	}

	public object Clone()
	{
		var cell = new Cell(maxValue);

		if (final.HasValue)
			cell.Final = final.Value;

		for (var index = 0; index < maxValue; index++)
			cell.possible[index] = possible[index];

		return cell;
	}

	public override bool Equals(object obj)
	{
		return obj is Cell cell && Equals(cell);
	}

	public bool Equals(Cell cell)
	{
		if (ReferenceEquals(this, cell))
			return true;

		if (final != cell.final || maxValue != cell.maxValue)
			return false;

		for (var index = 0; index < maxValue; index++)
		{
			if (possible[index] != cell.possible[index])
				return false;
		}

		return true;
	}

	public override int GetHashCode()
	{
		return 0;
	}
}