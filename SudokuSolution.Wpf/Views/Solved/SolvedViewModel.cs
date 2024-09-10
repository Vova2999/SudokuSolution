using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight.CommandWpf;
using SudokuSolution.Common.Execution;
using SudokuSolution.Common.Extensions;
using SudokuSolution.Domain.Entities;
using SudokuSolution.Logic.GameService;
using SudokuSolution.Wpf.Common.Base;
using SudokuSolution.Wpf.Common.Commands;
using SudokuSolution.Wpf.Common.Dispatcher;
using SudokuSolution.Wpf.Common.MessageBox;
using SudokuSolution.Wpf.Controls.Field;

namespace SudokuSolution.Wpf.Views.Solved;

public class SolvedViewModel : ViewModel<SolvedWindow>
{
	public override object Header => string.Empty;

	private bool isBusy;
	private int currentSolved;
	private int? totalSolvedCount;

	private ICommand loadedCommand;
	private ICommand prevSolvedCommand;
	private ICommand nextSolvedCommand;
	private ICommand contentRenderedCommand;
	private ICommand handleMouseMoveCommand;

	private readonly IGameService gameService;
	private readonly IDispatcherHelper dispatcherHelper;
	private readonly IMessageBoxService messageBoxService;

	private readonly Field startField;
	private readonly int maxSolvedCount;
	private readonly bool solveAllFields;

	private List<Field> solvedFields;
	private FieldEnumeratorAsync fieldEnumeratorAsync;

	private readonly ExecutionTracker executionTracker;

	public bool IsBusy
	{
		get => isBusy;
		set => Set(ref isBusy, value);
	}

	public int CurrentSolved
	{
		get => currentSolved;
		set => Set(ref currentSolved, value);
	}

	public int? TotalSolvedCount
	{
		get => totalSolvedCount;
		set => Set(ref totalSolvedCount, value);
	}

	public FieldViewModel FieldViewModel { get; private set; }

	public ICommand LoadedCommand => loadedCommand ??= new RelayCommand(OnLoaded);
	public ICommand PrevSolvedCommand => prevSolvedCommand ??= new RelayCommand(OnPrevSolved, CanPrevSolved);
	public ICommand NextSolvedCommand => nextSolvedCommand ??= new AsyncRelayCommand(OnNextSolvedAsync, CanNextSolved);
	public ICommand ContentRenderedCommand => contentRenderedCommand ??= new AsyncRelayCommand(OnContentRenderedAsync);
	public ICommand HandleMouseMoveCommand => handleMouseMoveCommand ??= new RelayCommand<MouseEventArgs>(OnHandleMouseMove);

	public SolvedViewModel(IGameService gameService,
						   IDispatcherHelper dispatcherHelper,
						   IMessageBoxService messageBoxService,
						   FieldViewModel fieldViewModel,
						   Field startField,
						   int maxSolvedCount,
						   bool solveAllFields)
	{
		this.gameService = gameService;
		this.dispatcherHelper = dispatcherHelper;
		this.messageBoxService = messageBoxService;
		this.startField = startField;
		this.maxSolvedCount = maxSolvedCount;
		this.solveAllFields = solveAllFields;
		FieldViewModel = fieldViewModel;

		executionTracker = new ExecutionTracker(
			() => this.dispatcherHelper.CheckBeginInvokeOnUI(() => IsBusy = true),
			() => this.dispatcherHelper.CheckBeginInvokeOnUI(() => IsBusy = false));
	}

	private void OnLoaded()
	{
		FieldViewModel.LockSelectedMenu = true;
		FieldViewModel.RefreshField(startField.MaxValue);
	}

	private void OnPrevSolved()
	{
		using (executionTracker.TrackExecution())
		{
			CurrentSolved--;
			LoadCurrentField();
		}
	}

	private bool CanPrevSolved()
	{
		return CurrentSolved > 1;
	}

	private async Task OnNextSolvedAsync()
	{
		using (executionTracker.TrackExecution())
		{
			if (CurrentSolved == solvedFields.Count)
			{
				if (CurrentSolved == maxSolvedCount)
				{
					dispatcherHelper.CheckBeginInvokeOnUI(() => TotalSolvedCount = CurrentSolved);
					return;
				}

				var moveNextResult = await fieldEnumeratorAsync.MoveNextAsync().ConfigureAwait(false);
				if (!moveNextResult)
				{
					dispatcherHelper.CheckBeginInvokeOnUI(() => TotalSolvedCount = CurrentSolved);
					return;
				}

				solvedFields.Add(fieldEnumeratorAsync.Current);
			}

			dispatcherHelper.CheckBeginInvokeOnUI(() =>
			{
				CurrentSolved++;
				LoadCurrentField();
			});
		}
	}

	private bool CanNextSolved()
	{
		return !TotalSolvedCount.HasValue || CurrentSolved < TotalSolvedCount;
	}

	private async Task OnContentRenderedAsync()
	{
		using (executionTracker.TrackExecution())
		{
			if (solveAllFields)
			{
				solvedFields = await SolveAllFieldsAsync().ConfigureAwait(false);
			}
			else
			{
				solvedFields = new List<Field>();
				fieldEnumeratorAsync = gameService.StartSolve(startField);
				var moveNextResult = await fieldEnumeratorAsync.MoveNextAsync().ConfigureAwait(false);
				if (moveNextResult)
					solvedFields.Add(fieldEnumeratorAsync.Current);
			}

			dispatcherHelper.CheckBeginInvokeOnUI(() =>
			{
				if (!solvedFields.Any())
				{
					messageBoxService.Show("Решений нет!", "", MessageBoxButton.OK, MessageBoxImage.Warning);
					TypedView.Close();
					return;
				}

				CurrentSolved = 1;
				if (solveAllFields)
					TotalSolvedCount = solvedFields.Count;

				LoadCurrentField();
			});
		}
	}

	private Task<List<Field>> SolveAllFieldsAsync()
	{
		var taskCompletionSource = new TaskCompletionSource<List<Field>>();
		Task.Run(() => taskCompletionSource.SetResult(gameService.Solve(startField).Take(maxSolvedCount).ToList()));
		return taskCompletionSource.Task;
	}

	private void LoadCurrentField()
	{
		var field = solvedFields[CurrentSolved - 1];

		if (FieldViewModel.Size != field.MaxValue)
			FieldViewModel.RefreshField(field.MaxValue);

		FieldViewModel.Cells.ForEach((row, cells) => cells.ForEach((column, cell) =>
		{
			cell.Value = field.Cells[row, column].Final;
			cell.IsBoldFont = startField.Cells[row, column].HasFinal;
		}));
	}

	private static void OnHandleMouseMove(MouseEventArgs obj)
	{
		obj.Handled = true;
	}

	public override void Cleanup()
	{
		FieldViewModel?.Cleanup();
		FieldViewModel = null;
		base.Cleanup();
	}

	public interface IFactory
	{
		SolvedViewModel Create(Field startField, int maxSolvedCount, bool solveAllFields);
	}
}