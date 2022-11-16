﻿using System;
using System.Globalization;
using System.Windows;
using SudokuSolution.Wpf.Common.Converters.Base;

namespace SudokuSolution.Wpf.Common.Converters {
	public class BoolToBoldFontWeightConverter : MarkupConverterBase {
		protected override object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
			return value is bool boolValue ? boolValue ? FontWeights.Bold : FontWeights.Normal : throw new ArgumentException($"{nameof(PercentToDoubleConverter)} only for bool values");
		}

		protected override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
			throw new NotImplementedException();
		}
	}
}