using System.Windows.Input;
using GalaSoft.MvvmLight.CommandWpf;
using SudokuSolution.Common.Extensions;
using SudokuSolution.Domain.Entities;
using SudokuSolution.Wpf.Common.Base;
using SudokuSolution.Wpf.Controls.Field;
using SudokuSolution.Wpf.Controls.Settings;
using SudokuSolution.Wpf.Extensions;
using SudokuSolution.Wpf.Views.Solved;

namespace SudokuSolution.Wpf.Views.Main;

public class MainViewModel : ViewModel<MainWindow>
{
	public override object Header => string.Empty;

	private bool isSettingsOpened;

	private ICommand calculateCommand;
	private ICommand calculateAllCommand;
	private ICommand openSettingsCommand;
	private ICommand closeSettingsCommand;
	private ICommand handleMouseMoveCommand;

	private readonly SolvedViewModel.IFactory solvedViewModelFactory;

	public bool IsSettingsOpened
	{
		get => isSettingsOpened;
		set => Set(ref isSettingsOpened, value);
	}

	public FieldViewModel FieldViewModel { get; private set; }
	public SettingsViewModel SettingsViewModel { get; private set; }

	public ICommand CalculateCommand => calculateCommand ??= new RelayCommand(OnCalculate);
	public ICommand CalculateAllCommand => calculateAllCommand ??= new RelayCommand(OnCalculateAll);
	public ICommand OpenSettingsCommand => openSettingsCommand ??= new RelayCommand(OnOpenSettings);
	public ICommand CloseSettingsCommand => closeSettingsCommand ??= new RelayCommand(OnCloseSettings);
	public ICommand HandleMouseMoveCommand => handleMouseMoveCommand ??= new RelayCommand<MouseEventArgs>(OnHandleMouseMove);

	public MainViewModel(FieldViewModel fieldViewModel,
						 SettingsViewModel settingsViewModel,
						 SolvedViewModel.IFactory solvedViewModelFactory)
	{
		this.solvedViewModelFactory = solvedViewModelFactory;
		FieldViewModel = fieldViewModel;
		SettingsViewModel = settingsViewModel;
	}

	private void OnCalculate()
	{
		var startField = CreateField();
		solvedViewModelFactory.Create(startField, SettingsViewModel.MaxSolved, false).OpenDialogInUi();
	}

	private void OnCalculateAll()
	{
		var startField = CreateField();
		solvedViewModelFactory.Create(startField, SettingsViewModel.MaxSolved, true).OpenDialogInUi();
	}

	private Field CreateField()
	{
		var field = new Field(SettingsViewModel.Size);
		FieldViewModel.Cells.ForEach((row, cells) =>
			cells.ForEach((column, cell) =>
			{
				if (cell.Value.HasValue)
					field.Cells[row, column].Final = cell.Value.Value;
			}));
		return field;
	}

	private void OnOpenSettings()
	{
		IsSettingsOpened = true;
	}

	private void OnCloseSettings()
	{
		IsSettingsOpened = false;
	}

	private static void OnHandleMouseMove(MouseEventArgs obj)
	{
		obj.Handled = true;
	}

	public override void Cleanup()
	{
		FieldViewModel?.Cleanup();
		FieldViewModel = null;
		SettingsViewModel?.Cleanup();
		SettingsViewModel = null;
		base.Cleanup();
	}
}