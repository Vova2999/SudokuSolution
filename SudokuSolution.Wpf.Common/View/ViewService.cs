﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using GalaSoft.MvvmLight.Messaging;
using Grace.DependencyInjection;
using SudokuSolution.Wpf.Common.Base;
using SudokuSolution.Wpf.Common.Messages;

namespace SudokuSolution.Wpf.Common.View {
	public class ViewService : IViewService {
		private readonly ILocatorService locatorService;
		private readonly List<Window> openedWindows;

		public ViewService(IMessenger messenger, ILocatorService locatorService) {
			this.locatorService = locatorService;
			openedWindows = new List<Window>();

			// Listen for the close event
			messenger.Register<RequestCloseMessage>(this, OnRequestClose);
		}

		[DebuggerStepThrough]
		public void OpenWindow<TViewModel>() where TViewModel : IViewModel {
			// Create window for that view tabModel.
			var window = CreateWindow<TViewModel>(WindowMode.Window);

			// Open the window.
			window.Show();
		}

		[DebuggerStepThrough]
		public void OpenWindow(IViewModel viewModel) {
			// Create window for that view tabModel.
			var window = CreateWindow(viewModel, WindowMode.Window);

			// Open the window.
			window.Show();
		}

		[DebuggerStepThrough]
		public bool? OpenDialog<TViewModel>() where TViewModel : IViewModel {
			// Create window for that viewModel.
			var window = CreateWindow<TViewModel>(WindowMode.Dialog);

			// Open the window and return the result.
			return window.ShowDialog();
		}

		[DebuggerStepThrough]
		public bool? OpenDialog(IViewModel viewModel) {
			// Create window for that viewModel.
			var window = CreateWindow(viewModel, WindowMode.Dialog);

			// Open the window and return the result.
			return window.ShowDialog();
		}

		[DebuggerStepThrough]
		public Window CreateWindow<TViewModel>(WindowMode windowMode) where TViewModel : IViewModel {
			var viewModel = locatorService.Locate<TViewModel>();

			return CreateWindow(viewModel, windowMode);
		}

		[DebuggerStepThrough]
		public Window CreateWindow(IViewModel viewModel, WindowMode windowMode) {
			var window = (Window) viewModel.View;
			window.DataContext = viewModel;
			window.Closed += OnClosed;

			lock (openedWindows) {
				// Last window opened is considered the 'owner' of the window.
				// May not be 100% correct in some situations but it is more
				// then good enough for handling dialog windows
				if (windowMode == WindowMode.Dialog && openedWindows.Any()) {
					var lastOpened = openedWindows.Last();
					if (lastOpened.IsActive && !Equals(window, lastOpened))
						window.Owner = lastOpened;
				}

				openedWindows.Add(window);
			}

			return window;
		}

		public int GetOpenedWindowsCount() {
			lock (openedWindows)
				return openedWindows.Count;
		}

		private void OnRequestClose(RequestCloseMessage message) {
			var window = openedWindows.SingleOrDefault(w => w.DataContext == message.ViewModel);
			if (window == null)
				return;

			if (message.DialogResult != null)
				window.DialogResult = message.DialogResult;
			else
				window.Close();
		}

		private void OnClosed(object sender, EventArgs e) {
			var window = (Window) sender;
			window.Closed -= OnClosed;

			lock (openedWindows)
				openedWindows.Remove(window);
		}
	}
}