using GalaSoft.MvvmLight;

namespace SudokuSolution.Wpf.Controls.Field;

public class CellModel : ViewModelBase
{
	private int? _value;
	private bool _isBoldFont;
	private bool _isMenuOpened;

	public int? Value
	{
		get => _value;
		set => Set(ref _value, value);
	}

	public bool IsBoldFont
	{
		get => _isBoldFont;
		set => Set(ref _isBoldFont, value);
	}

	public bool IsMenuOpened
	{
		get => _isMenuOpened;
		set => Set(ref _isMenuOpened, value);
	}
}