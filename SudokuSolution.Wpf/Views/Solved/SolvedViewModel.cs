﻿using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight.CommandWpf;
using SudokuSolution.Common.Extensions;
using SudokuSolution.Domain.Entities;
using SudokuSolution.Wpf.Common.Base;
using SudokuSolution.Wpf.Common.MessageBox;
using SudokuSolution.Wpf.Controls.Field;

namespace SudokuSolution.Wpf.Views.Solved {
	public class SolvedViewModel : ViewModel<SolvedWindow> {
		public override object Header => string.Empty;

		private int currentSolved;
		private int? totalSolvedCount;

		private ICommand loadedCommand;
		private ICommand prevSolvedCommand;
		private ICommand nextSolvedCommand;
		private ICommand contentRenderedCommand;
		private ICommand handleMouseMoveCommand;

		private readonly IMessageBoxService messageBoxService;

		private readonly Field startField;
		private readonly IEnumerator<Field> solvedFields;
		private readonly bool solveAllFields;

		private readonly List<Field> fields;

		public int CurrentSolved {
			get => currentSolved;
			set => Set(ref currentSolved, value);
		}

		public int? TotalSolvedCount {
			get => totalSolvedCount;
			set => Set(ref totalSolvedCount, value);
		}

		public FieldViewModel FieldViewModel { get; private set; }

		public ICommand LoadedCommand => loadedCommand ??= new RelayCommand(OnLoaded);
		public ICommand PrevSolvedCommand => prevSolvedCommand ??= new RelayCommand(OnPrevSolved, CanPrevSolved);
		public ICommand NextSolvedCommand => nextSolvedCommand ??= new RelayCommand(OnNextSolved, CanNextSolved);
		public ICommand ContentRenderedCommand => contentRenderedCommand ??= new RelayCommand(OnContentRendered);
		public ICommand HandleMouseMoveCommand => handleMouseMoveCommand ??= new RelayCommand<MouseEventArgs>(OnHandleMouseMove);

		public SolvedViewModel(IMessageBoxService messageBoxService,
							   FieldViewModel fieldViewModel,
							   Field startField,
							   IEnumerable<Field> solvedFields,
							   bool solveAllFields) {
			this.messageBoxService = messageBoxService;
			this.startField = startField;
			this.solvedFields = solvedFields.GetEnumerator();
			this.solveAllFields = solveAllFields;
			FieldViewModel = fieldViewModel;

			fields = new List<Field>();
		}

		private void OnLoaded() {
			FieldViewModel.LockSelectedMenu = true;

			if (solveAllFields) {
				while (solvedFields.MoveNext())
					fields.Add(solvedFields.Current);

				TotalSolvedCount = fields.Count;
			}
			else {
				if (solvedFields.MoveNext())
					fields.Add(solvedFields.Current);
				else
					TotalSolvedCount = 0;
			}

			if (TotalSolvedCount == 0)
				return;

			CurrentSolved = 1;
			LoadCurrentField();
		}

		private void LoadCurrentField() {
			var field = fields[CurrentSolved - 1];

			if (FieldViewModel.Size != field.MaxValue)
				FieldViewModel.RefreshField(field.MaxValue);

			FieldViewModel.Cells.ForEach((row, cells) => cells.ForEach((column, cell) => {
				cell.Value = field.Cells[row, column].Final;
				cell.IsBoldFont = startField.Cells[row, column].HasFinal;
			}));
		}

		private void OnPrevSolved() {
			CurrentSolved--;
			LoadCurrentField();
		}

		private bool CanPrevSolved() {
			return CurrentSolved > 1;
		}

		private void OnNextSolved() {
			if (CurrentSolved == fields.Count) {
				if (!solvedFields.MoveNext()) {
					TotalSolvedCount = fields.Count;
					return;
				}

				fields.Add(solvedFields.Current);
			}

			CurrentSolved++;
			LoadCurrentField();
		}

		private bool CanNextSolved() {
			return !TotalSolvedCount.HasValue || CurrentSolved < TotalSolvedCount;
		}

		private void OnContentRendered() {
			if (TotalSolvedCount != 0)
				return;

			messageBoxService.Show("Решений нет!", "", MessageBoxButton.OK, MessageBoxImage.Warning);
			TypedView.Close();
		}

		private static void OnHandleMouseMove(MouseEventArgs obj) {
			obj.Handled = true;
		}

		public override void Cleanup() {
			FieldViewModel?.Cleanup();
			FieldViewModel = null;
			base.Cleanup();
		}

		public interface IFactory {
			SolvedViewModel Create(Field startField, IEnumerable<Field> solvedFields, bool solveAllFields);
		}
	}
}