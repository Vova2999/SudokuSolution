﻿using System.Collections.Generic;
using System.Windows.Input;

namespace SudokuSolution.Wpf.Helpers;

public static class KeyHelper
{
	public static Dictionary<Key, int> DigitKeys = new()
	{
		{ Key.D0, 0 },
		{ Key.D1, 1 },
		{ Key.D2, 2 },
		{ Key.D3, 3 },
		{ Key.D4, 4 },
		{ Key.D5, 5 },
		{ Key.D6, 6 },
		{ Key.D7, 7 },
		{ Key.D8, 8 },
		{ Key.D9, 9 },
		{ Key.NumPad0, 0 },
		{ Key.NumPad1, 1 },
		{ Key.NumPad2, 2 },
		{ Key.NumPad3, 3 },
		{ Key.NumPad4, 4 },
		{ Key.NumPad5, 5 },
		{ Key.NumPad6, 6 },
		{ Key.NumPad7, 7 },
		{ Key.NumPad8, 8 },
		{ Key.NumPad9, 9 }
	};
}