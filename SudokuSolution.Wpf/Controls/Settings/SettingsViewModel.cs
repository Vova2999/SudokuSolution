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

	private int size;
	private int[] allowedSizes;
	private int maxSolved;
	private string maxSolvedString;

	private ICommand handleMouseMoveCommand;

	private readonly IMessenger messenger;

	public int Size
	{
		get => size;
		set
		{
			if (Set(ref size, value))
				OnSizeChanged();
		}
	}

	public int[] AllowedSizes
	{
		get => allowedSizes;
		set => Set(ref allowedSizes, value);
	}

	public int MaxSolved
	{
		get => maxSolved;
		set
		{
			if (Set(ref maxSolved, value))
				MaxSolvedString = value.ToString();
		}
	}

	public string MaxSolvedString
	{
		get => maxSolvedString;
		set
		{
			if (value.IsNullOrEmpty())
				value = "0";

			if (IsValidMaxSolvedStringValue(value))
			{
				Set(ref maxSolvedString, value);
				MaxSolved = int.Parse(value);
			}
		}
	}

	public ICommand HandleMouseMoveCommand => handleMouseMoveCommand ??= new RelayCommand<MouseEventArgs>(OnHandleMouseMove);

	public SettingsViewModel(IMessenger messenger)
	{
		this.messenger = messenger;

		AllowedSizes = Constants.AllowedSizes;
		Size = Constants.StartedSize;

		MaxSolved = Constants.StartedMaxSolved;
	}

	private void OnSizeChanged()
	{
		messenger.Send(new SizeChangedMessage(Size));
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
		messenger.Unregister(this);
		base.Cleanup();
	}
}