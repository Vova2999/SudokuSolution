using System;
using System.Collections.Generic;
using GalaSoft.MvvmLight.Messaging;
using Grace.DependencyInjection;
using Grace.Factory;
using SudokuSolution.Logic.FieldActions.CleanPossible;
using SudokuSolution.Logic.FieldActions.CleanPossible.CleanPossibleByColumn;
using SudokuSolution.Logic.FieldActions.CleanPossible.CleanPossibleByFinal;
using SudokuSolution.Logic.FieldActions.CleanPossible.CleanPossibleByRow;
using SudokuSolution.Logic.FieldActions.SetFinal;
using SudokuSolution.Logic.FieldActions.SetFinal.SetFinalForColumn;
using SudokuSolution.Logic.FieldActions.SetFinal.SetFinalForRow;
using SudokuSolution.Logic.FieldActions.SetFinal.SetFinalForSinglePossible;
using SudokuSolution.Logic.FieldActions.SetFinal.SetFinalForSquare;
using SudokuSolution.Logic.FieldActions.SetRandomFinalAndSplitField;
using SudokuSolution.Logic.FieldService;
using SudokuSolution.Logic.GameService;
using SudokuSolution.Wpf.Common.Dispatcher;
using SudokuSolution.Wpf.Common.MessageBox;
using SudokuSolution.Wpf.Common.View;
using SudokuSolution.Wpf.Views.Main.Logic;

namespace SudokuSolution.Wpf;

public class Locator : ILocatorService
{
	private readonly DependencyInjectionContainer container;
	private static readonly Lazy<Locator> Lazy = new(() => new Locator());

	public static Locator Current => Lazy.Value;
	public static string FactoryName => "IFactory";

	private Locator()
	{
		container = new DependencyInjectionContainer();
		container.Configure(RegisterDependencies);
	}

	private static void RegisterDependencies(IExportRegistrationBlock registration)
	{
		registration.ExportInterfaceFactories(type => type.Name == FactoryName);

		RegisterLogic(registration);
		RegisterServices(registration);
	}

	private static void RegisterLogic(IExportRegistrationBlock registration)
	{
		RegisterSingleton<IGameService, GameService>(registration);
		RegisterSingleton<IFieldService, FieldService>(registration);

		RegisterSingleton<ICleanPossibleFacade, CleanPossibleFacade>(registration);
		RegisterSingleton<ICleanPossibleByFinal, CleanPossibleByFinal>(registration);
		RegisterSingleton<ICleanPossibleByRow, CleanPossibleByRow>(registration);
		RegisterSingleton<ICleanPossibleByColumn, CleanPossibleByColumn>(registration);

		RegisterSingleton<ISetFinalFacade, SetFinalFacade>(registration);
		RegisterSingleton<ISetFinalForSinglePossible, SetFinalForSinglePossible>(registration);
		RegisterSingleton<ISetFinalForRow, SetFinalForRow>(registration);
		RegisterSingleton<ISetFinalForColumn, SetFinalForColumn>(registration);
		RegisterSingleton<ISetFinalForSquare, SetFinalForSquare>(registration);

		RegisterSingleton<ISetRandomFinalAndSplitField, SetRandomFinalAndSplitField>(registration);
	}

	private static void RegisterServices(IExportRegistrationBlock registration)
	{
		RegisterSingleton<IMainWindowProvider, MainWindowProvider>(registration);

		RegisterSingleton<IViewService, ViewService>(registration);
		RegisterSingleton<IDispatcherHelper, DispatcherHelperAdapter>(registration);
		RegisterSingleton<IMessageBoxService, MessageBoxService>(registration);

		RegisterSingleton<IMessenger, Messenger>(registration);
	}

	private static void RegisterSingleton<TFrom, TTo>(IExportRegistrationBlock registrationBlock) where TTo : TFrom
	{
		registrationBlock.Export<TTo>().As<TFrom>().Lifestyle.Singleton();
	}

	public object GetService(Type serviceType)
	{
		return container.Locate(serviceType);
	}
	public bool CanLocate(Type type, ActivationStrategyFilter consider = null, object key = null)
	{
		return container.CanLocate(type, consider, key);
	}
	public object Locate(Type type)
	{
		return container.Locate(type);
	}
	public object LocateOrDefault(Type type, object defaultValue)
	{
		return container.LocateOrDefault(type, defaultValue);
	}
	public T Locate<T>()
	{
		return container.Locate<T>();
	}
	public T LocateOrDefault<T>(T defaultValue = default)
	{
		return container.LocateOrDefault(defaultValue);
	}
	public List<object> LocateAll(Type type, object extraData = null, ActivationStrategyFilter consider = null, IComparer<object> comparer = null)
	{
		return container.LocateAll(type, extraData, consider, comparer);
	}
	public List<T> LocateAll<T>(Type type = null, object extraData = null, ActivationStrategyFilter consider = null, IComparer<T> comparer = null)
	{
		return container.LocateAll(type, extraData, consider, comparer);
	}
	public bool TryLocate<T>(out T value, object extraData = null, ActivationStrategyFilter consider = null, object withKey = null, bool isDynamic = false)
	{
		return container.TryLocate(out value, extraData, consider, withKey, isDynamic);
	}
	public bool TryLocate(Type type, out object value, object extraData = null, ActivationStrategyFilter consider = null, object withKey = null, bool isDynamic = false)
	{
		return container.TryLocate(type, out value, extraData, consider, withKey, isDynamic);
	}
	public object LocateByName(string name, object extraData = null, ActivationStrategyFilter consider = null)
	{
		return container.LocateByName(name, extraData, consider);
	}
	public List<object> LocateAllByName(string name, object extraData = null, ActivationStrategyFilter consider = null)
	{
		return container.LocateAllByName(name, extraData, consider);
	}
	public bool TryLocateByName(string name, out object value, object extraData = null, ActivationStrategyFilter consider = null)
	{
		return container.TryLocateByName(name, out value, extraData, consider);
	}
	// ReSharper disable MethodOverloadWithOptionalParameter
	public object Locate(Type type, object extraData = null, ActivationStrategyFilter consider = null, object withKey = null, bool isDynamic = false)
	{
		return container.Locate(type, extraData, consider, withKey, isDynamic);
	}
	public T Locate<T>(object extraData = null, ActivationStrategyFilter consider = null, object withKey = null, bool isDynamic = false)
	{
		return container.Locate<T>(extraData, consider, withKey, isDynamic);
	}
	// ReSharper restore MethodOverloadWithOptionalParameter
}