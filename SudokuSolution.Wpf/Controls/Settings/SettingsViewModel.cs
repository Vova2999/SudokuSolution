using System.Windows.Input;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using SudokuSolution.Common.Extensions;
using SudokuSolution.Wpf.Common.Base;
using SudokuSolution.Wpf.Messages;

namespace SudokuSolution.Wpf.Controls.Settings;

public class SettingsViewModel : ViewModel<SettingsControl>
{
	public override object Header => string.Empty;

	private int _size;
	private int[] _allowedSizes;
	private int _maxSolved;
	private string _maxSolvedString;

	private ICommand _handleMouseMoveCommand;

	private readonly IMessenger _messenger;

	public int Size
	{
		get => _size;
		set
		{
			if (Set(ref _size, value))
				OnSizeChanged();
		}
	}

	public int[] AllowedSizes
	{
		get => _allowedSizes;
		set => Set(ref _allowedSizes, value);
	}

	public int MaxSolved
	{
		get => _maxSolved;
		set
		{
			if (Set(ref _maxSolved, value))
				MaxSolvedString = value.ToString();
		}
	}

	public string MaxSolvedString
	{
		get => _maxSolvedString;
		set
		{
			if (value.IsNullOrEmpty())
				value = "0";

			if (IsValidMaxSolvedStringValue(value))
			{
				Set(ref _maxSolvedString, value);
				MaxSolved = int.Parse(value);
			}
		}
	}

	public ICommand HandleMouseMoveCommand => _handleMouseMoveCommand ??= new RelayCommand<MouseEventArgs>(OnHandleMouseMove);

	public SettingsViewModel(IMessenger messenger)
	{
		_messenger = messenger;

		AllowedSizes = Constants.AllowedSizes;
		Size = Constants.StartedSize;

		MaxSolved = Constants.StartedMaxSolved;
	}

	private void OnSizeChanged()
	{
		_messenger.Send(new SizeChangedMessage(Size));
	}

	private bool IsValidMaxSolvedStringValue(string value)
	{
		return int.TryParse(value, out _);
	}

	private static void OnHandleMouseMove(MouseEventArgs obj)
	{
		obj.Handled = true;
	}

	public override void Cleanup()
	{
		_messenger.Unregister(this);
		base.Cleanup();
	}
}