using System;
using System.Linq;

namespace SudokuSolution.Domain.Entities;

public class Cell : ICloneable
{
	private int? _final;
	private readonly int _maxValue;
	private readonly bool[] _possible;

	public bool HasFinal => _final.HasValue;

	public int Final
	{
		get
		{
			if (!_final.HasValue)
				throw new InvalidOperationException("Final value missing");

			return _final.Value;
		}
		set
		{
			if (value <= 0 || value > _maxValue)
				throw new InvalidOperationException("Final value is invalid");

			_final = value;
			for (var index = 0; index < _maxValue; index++)
				_possible[index] = false;
		}
	}

	public bool this[int number]
	{
		get => _possible[number - 1];
		set
		{
			if (!_final.HasValue)
				_possible[number - 1] = value;
		}
	}

	internal Cell(int maxValue)
	{
		_maxValue = maxValue;
		_possible = Enumerable.Repeat(true, maxValue).ToArray();
	}

	public object Clone()
	{
		var cell = new Cell(_maxValue);

		if (_final.HasValue)
			cell.Final = _final.Value;

		for (var index = 0; index < _maxValue; index++)
			cell._possible[index] = _possible[index];

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

		if (_final != cell._final || _maxValue != cell._maxValue)
			return false;

		for (var index = 0; index < _maxValue; index++)
		{
			if (_possible[index] != cell._possible[index])
				return false;
		}

		return true;
	}

	public override int GetHashCode()
	{
		return 0;
	}
}