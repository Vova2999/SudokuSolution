using System;
using System.Globalization;
using SudokuSolution.Wpf.Common.Converters.Base;

namespace SudokuSolution.Wpf.Converters {
	public class FieldLineOpacityMultiConverter : MarkupMultiValueConverterBase {
		protected override object Convert(object[] values, Type targetType, object parameter, CultureInfo culture) {
			return values[0] is not int size || values[1] is not int index || values[2] is not double darkLineOpacity || values[3] is not double lightLineOpacity
				? throw new ArgumentException($"Invalid values of {nameof(FieldLineOpacityMultiConverter)}")
				: (index + 1) % size == 0
					? darkLineOpacity
					: lightLineOpacity;
		}

		protected override object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) {
			throw new NotImplementedException();
		}
	}
}