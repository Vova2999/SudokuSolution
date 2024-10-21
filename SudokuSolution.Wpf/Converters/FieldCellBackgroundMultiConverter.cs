using System;
using System.Globalization;
using System.Windows.Media;
using SudokuSolution.Wpf.Common.Converters.Base;

namespace SudokuSolution.Wpf.Converters;

public class FieldCellBackgroundMultiConverter : MarkupMultiValueConverterBase
{
	protected override object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
	{
		if (values[0] is not int size)
			throw new ArgumentException($"Invalid values of {nameof(FieldLineOpacityMultiConverter)}");

		var sqrtOfSize = (int) Math.Sqrt(size);

		return values[1] is not int row || values[2] is not int column || values[3] is not Brush darkCellBackground || values[4] is not Brush lightCellBackground
			? throw new ArgumentException($"Invalid values of {nameof(FieldLineOpacityMultiConverter)}")
			: (row / sqrtOfSize % 2 != 0) ^ (column / sqrtOfSize % 2 != 0)
				? darkCellBackground
				: lightCellBackground;
	}

	protected override object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
	{
		throw new NotImplementedException();
	}
}