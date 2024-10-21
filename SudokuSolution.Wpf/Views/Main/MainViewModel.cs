using System.Linq;
using System.Windows.Input;
using GalaSoft.MvvmLight.CommandWpf;
using SudokuSolution.Common.Extensions;
using SudokuSolution.Domain.Entities;
using SudokuSolution.Logic.GameService;
using SudokuSolution.Wpf.Common.Base;
using SudokuSolution.Wpf.Controls.Field;
using SudokuSolution.Wpf.Controls.Settings;
using SudokuSolution.Wpf.Extensions;
using SudokuSolution.Wpf.Views.Solved;

namespace SudokuSolution.Wpf.Views.Main;

public class MainViewModel : ViewModel<MainWindow>
{
	public override object Header => string.Empty;

	private bool _isSettingsOpened;

	private ICommand _calculateCommand;
	private ICommand _calculateAllCommand;
	private ICommand _openSettingsCommand;
	private ICommand _closeSettingsCommand;
	private ICommand _handleMouseMoveCommand;

	private readonly IGameService _gameService;

	private readonly SolvedViewModel.IFactory _solvedViewModelFactory;

	public bool IsSettingsOpened
	{
		get => _isSettingsOpened;
		set => Set(ref _isSettingsOpened, value);
	}

	public FieldViewModel FieldViewModel { get; private set; }
	public SettingsViewModel SettingsViewModel { get; private set; }

	public ICommand CalculateCommand => _calculateCommand ??= new RelayCommand(OnCalculate);
	public ICommand CalculateAllCommand => _calculateAllCommand ??= new RelayCommand(OnCalculateAll);
	public ICommand OpenSettingsCommand => _openSettingsCommand ??= new RelayCommand(OnOpenSettings);
	public ICommand CloseSettingsCommand => _closeSettingsCommand ??= new RelayCommand(OnCloseSettings);
	public ICommand HandleMouseMoveCommand => _handleMouseMoveCommand ??= new RelayCommand<MouseEventArgs>(OnHandleMouseMove);

	public MainViewModel(
		IGameService gameService,
		FieldViewModel fieldViewModel,
		SettingsViewModel settingsViewModel,
		SolvedViewModel.IFactory solvedViewModelFactory)
	{
		_gameService = gameService;
		_solvedViewModelFactory = solvedViewModelFactory;
		FieldViewModel = fieldViewModel;
		SettingsViewModel = settingsViewModel;
	}

	private void OnCalculate()
	{
		var startField = CreateField();
		var solvedFields = _gameService.Solve(startField).Take(SettingsViewModel.MaxSolved);
		_solvedViewModelFactory.Create(startField, solvedFields, false).OpenDialogInUi();
	}

	private void OnCalculateAll()
	{
		var startField = CreateField();
		var solvedFields = _gameService.Solve(startField).Take(SettingsViewModel.MaxSolved);
		_solvedViewModelFactory.Create(startField, solvedFields, true).OpenDialogInUi();
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