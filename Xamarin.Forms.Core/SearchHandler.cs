﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Input;

namespace Xamarin.Forms
{
	public class SearchHandler : BindableObject, ISearchHandlerController
	{
		#region ISearchHandlerController

		event EventHandler<ListProxyChangedEventArgs> ISearchHandlerController.ListProxyChanged
		{
			add { _listProxyChanged += value; }
			remove { _listProxyChanged -= value; }
		}

		event EventHandler<ListProxyChangedEventArgs> _listProxyChanged;

		IReadOnlyList<object> ISearchHandlerController.ListProxy => ListProxy;

		ListProxy ListProxy
		{
			get { return _listProxy; }
			set
			{
				if (_listProxy == value)
					return;
				var oldProxy = _listProxy;
				_listProxy = value;
				_listProxyChanged?.Invoke(this, new ListProxyChangedEventArgs(oldProxy, value));
			}
		}

		void ISearchHandlerController.ClearPlaceholderClicked()
		{
			OnClearPlaceholderClicked();
		}

		void ISearchHandlerController.ItemSelected(object obj)
		{
			OnItemSelected(obj);
		}

		void ISearchHandlerController.QueryConfirmed()
		{
			OnQueryConfirmed();
		}

		#endregion ISearchHandlerController

		public static readonly BindableProperty ClearIconHelpTextProperty =
			BindableProperty.Create(nameof(ClearIconHelpText), typeof(string), typeof(SearchHandler), null, BindingMode.OneTime,
				propertyChanged: (b, o, n) => ((SearchHandler)b).UpdateAutomationProperties());

		public static readonly BindableProperty ClearIconNameProperty =
			BindableProperty.Create(nameof(ClearIconName), typeof(string), typeof(SearchHandler), null, BindingMode.OneTime,
				propertyChanged: (b, o, n) => ((SearchHandler)b).UpdateAutomationProperties());

		public static readonly BindableProperty ClearIconProperty =
							BindableProperty.Create(nameof(ClearIcon), typeof(ImageSource), typeof(SearchHandler), null, BindingMode.OneTime);

		public static readonly BindableProperty ClearPlaceholderCommandParameterProperty =
			BindableProperty.Create(nameof(ClearPlaceholderCommandParameter), typeof(object), typeof(SearchHandler), null, BindingMode.OneTime,
				propertyChanged: OnClearPlaceholderCommandParameterChanged);

		public static readonly BindableProperty ClearPlaceholderCommandProperty =
					BindableProperty.Create(nameof(ClearPlaceholderCommand), typeof(ICommand), typeof(SearchHandler), null, BindingMode.OneTime,
				propertyChanged: OnClearPlaceholderCommandChanged);

		public static readonly BindableProperty ClearPlaceholderEnabledProperty =
			BindableProperty.Create(nameof(ClearPlaceholderEnabled), typeof(bool), typeof(SearchHandler), false, BindingMode.OneWay);

		public static readonly BindableProperty ClearPlaceholderHelpTextProperty =
			BindableProperty.Create(nameof(ClearPlaceholderHelpText), typeof(string), typeof(SearchHandler), null, BindingMode.OneTime,
				propertyChanged: (b, o, n) => ((SearchHandler)b).UpdateAutomationProperties());

		public static readonly BindableProperty ClearPlaceholderIconProperty =
					BindableProperty.Create(nameof(ClearPlaceholderIcon), typeof(ImageSource), typeof(SearchHandler), null, BindingMode.OneTime,
						propertyChanged: (b, o, n) => ((SearchHandler)b).UpdateAutomationProperties());

		public static readonly BindableProperty ClearPlaceholderNameProperty =
			BindableProperty.Create(nameof(ClearPlaceholderName), typeof(string), typeof(SearchHandler), null, BindingMode.OneTime,
				propertyChanged: (b, o, n) => ((SearchHandler)b).UpdateAutomationProperties());

		public static readonly BindableProperty CommandParameterProperty =
					BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(SearchHandler), null, BindingMode.OneTime,
				propertyChanged: OnCommandParameterChanged);

		public static readonly BindableProperty CommandProperty =
			BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(SearchHandler), null, BindingMode.OneTime,
				propertyChanged: OnCommandChanged);

		public static readonly BindableProperty DisplayMemberNameProperty =
			BindableProperty.Create(nameof(DisplayMemberName), typeof(string), typeof(SearchHandler), null, BindingMode.OneTime);

		public static readonly BindableProperty IsSearchEnabledProperty =
			BindableProperty.Create(nameof(IsSearchEnabled), typeof(bool), typeof(SearchHandler), true, BindingMode.OneWay);

		public static readonly BindableProperty ItemsSourceProperty =
			BindableProperty.Create(nameof(ItemsSource), typeof(IEnumerable), typeof(SearchHandler), null, BindingMode.OneTime,
				propertyChanged: OnItemsSourceChanged);

		public static readonly BindableProperty ItemTemplateProperty =
			BindableProperty.Create(nameof(ItemTemplate), typeof(DataTemplate), typeof(SearchHandler), null, BindingMode.OneTime);

		public static readonly BindableProperty ItemTemplateSelectorProperty =
			BindableProperty.Create(nameof(ItemTemplateSelector), typeof(IDataTemplateSelector), typeof(SearchHandler), null, BindingMode.OneTime);

		public static readonly BindableProperty PlaceholderProperty =
			BindableProperty.Create(nameof(Placeholder), typeof(string), typeof(SearchHandler), null, BindingMode.OneTime);

		public static readonly BindableProperty QueryIconHelpTextProperty =
			BindableProperty.Create(nameof(QueryIconHelpText), typeof(string), typeof(SearchHandler), null, BindingMode.OneTime,
				propertyChanged: (b, o, n) => ((SearchHandler)b).UpdateAutomationProperties());

		public static readonly BindableProperty QueryIconNameProperty =
			BindableProperty.Create(nameof(QueryIconName), typeof(string), typeof(SearchHandler), null, BindingMode.OneTime,
				propertyChanged: (b, o, n) => ((SearchHandler)b).UpdateAutomationProperties());

		public static readonly BindableProperty QueryIconProperty =
			BindableProperty.Create(nameof(QueryIcon), typeof(ImageSource), typeof(SearchHandler), null, BindingMode.OneTime,
				propertyChanged: (b, o, n) => ((SearchHandler)b).UpdateAutomationProperties());

		public static readonly BindableProperty QueryProperty =
			BindableProperty.Create(nameof(Query), typeof(string), typeof(SearchHandler), null, BindingMode.TwoWay,
				propertyChanged: OnQueryChanged);

		public static readonly BindableProperty SearchBoxVisibilityProperty =
			BindableProperty.Create(nameof(SearchBoxVisibility), typeof(SearchBoxVisiblity), typeof(SearchHandler), SearchBoxVisiblity.Expanded, BindingMode.OneWay);

		public static readonly BindableProperty ShowsResultsProperty =
			BindableProperty.Create(nameof(ShowsResults), typeof(bool), typeof(SearchHandler), false, BindingMode.OneTime);

		private ListProxy _listProxy;

		public ImageSource ClearIcon
		{
			get { return (ImageSource)GetValue(ClearIconProperty); }
			set { SetValue(ClearIconProperty, value); }
		}

		public string ClearIconHelpText
		{
			get { return (string)GetValue(ClearIconHelpTextProperty); }
			set { SetValue(ClearIconHelpTextProperty, value); }
		}

		public string ClearIconName
		{
			get { return (string)GetValue(ClearIconNameProperty); }
			set { SetValue(ClearIconNameProperty, value); }
		}

		public ICommand ClearPlaceholderCommand
		{
			get { return (ICommand)GetValue(ClearPlaceholderCommandProperty); }
			set { SetValue(ClearPlaceholderCommandProperty, value); }
		}

		public object ClearPlaceholderCommandParameter
		{
			get { return GetValue(ClearPlaceholderCommandParameterProperty); }
			set { SetValue(ClearPlaceholderCommandParameterProperty, value); }
		}

		public bool ClearPlaceholderEnabled
		{
			get { return (bool)GetValue(ClearPlaceholderEnabledProperty); }
			set { SetValue(ClearPlaceholderEnabledProperty, value); }
		}

		public string ClearPlaceholderHelpText
		{
			get { return (string)GetValue(ClearPlaceholderHelpTextProperty); }
			set { SetValue(ClearPlaceholderHelpTextProperty, value); }
		}

		public ImageSource ClearPlaceholderIcon
		{
			get { return (ImageSource)GetValue(ClearPlaceholderIconProperty); }
			set { SetValue(ClearPlaceholderIconProperty, value); }
		}

		public string ClearPlaceholderName
		{
			get { return (string)GetValue(ClearPlaceholderNameProperty); }
			set { SetValue(ClearPlaceholderNameProperty, value); }
		}

		public ICommand Command
		{
			get { return (ICommand)GetValue(CommandProperty); }
			set { SetValue(CommandProperty, value); }
		}

		public object CommandParameter
		{
			get { return (object)GetValue(CommandParameterProperty); }
			set { SetValue(CommandParameterProperty, value); }
		}

		public string DisplayMemberName
		{
			get { return (string)GetValue(DisplayMemberNameProperty); }
			set { SetValue(DisplayMemberNameProperty, value); }
		}

		public bool IsSearchEnabled
		{
			get { return (bool)GetValue(IsSearchEnabledProperty); }
			set { SetValue(IsSearchEnabledProperty, value); }
		}

		public IEnumerable ItemsSource
		{
			get { return (IEnumerable)GetValue(ItemsSourceProperty); }
			set { SetValue(ItemsSourceProperty, value); }
		}

		public DataTemplate ItemTemplate
		{
			get { return (DataTemplate)GetValue(ItemTemplateProperty); }
			set { SetValue(ItemTemplateProperty, value); }
		}
		public IDataTemplateSelector ItemTemplateSelector
		{
			get { return (IDataTemplateSelector)GetValue(ItemTemplateSelectorProperty); }
			set { SetValue(ItemTemplateSelectorProperty, value); }
		}

		public string Placeholder
		{
			get { return (string)GetValue(PlaceholderProperty); }
			set { SetValue(PlaceholderProperty, value); }
		}

		public string Query
		{
			get { return (string)GetValue(QueryProperty); }
			set { SetValue(QueryProperty, value); }
		}

		public ImageSource QueryIcon
		{
			get { return (ImageSource)GetValue(QueryIconProperty); }
			set { SetValue(QueryIconProperty, value); }
		}

		public string QueryIconHelpText
		{
			get { return (string)GetValue(QueryIconHelpTextProperty); }
			set { SetValue(QueryIconHelpTextProperty, value); }
		}

		public string QueryIconName
		{
			get { return (string)GetValue(QueryIconNameProperty); }
			set { SetValue(QueryIconNameProperty, value); }
		}

		public SearchBoxVisiblity SearchBoxVisibility
		{
			get { return (SearchBoxVisiblity)GetValue(SearchBoxVisibilityProperty); }
			set { SetValue(SearchBoxVisibilityProperty, value); }
		}

		public bool ShowsResults
		{
			get { return (bool)GetValue(ShowsResultsProperty); }
			set { SetValue(ShowsResultsProperty, value); }
		}

		bool ClearPlaceholderEnabledCore { set => SetValueCore(ClearPlaceholderEnabledProperty, value); }

		bool IsSearchEnabledCore { set => SetValueCore(IsSearchEnabledProperty, value); }

		protected virtual void OnClearPlaceholderClicked()
		{
			var command = ClearPlaceholderCommand;
			var commandParameter = ClearPlaceholderCommandParameter;
			if (command != null && command.CanExecute(commandParameter))
			{
				command.Execute(commandParameter);
			}
		}

		protected virtual void OnItemSelected(object item)
		{
		}

		protected virtual void OnQueryChanged(string oldValue, string newValue)
		{
		}

		protected virtual void OnQueryConfirmed()
		{
			var command = Command;
			var commandParameter = CommandParameter;
			if (command?.CanExecute(commandParameter) == true)
			{
				command.Execute(commandParameter);
			}
		}

		static void OnClearPlaceholderCommandChanged(BindableObject bindable, object oldValue, object newValue)
		{
			var self = (SearchHandler)bindable;
			var oldCommand = (ICommand)oldValue;
			var newCommand = (ICommand)newValue;
			self.OnClearPlaceholderCommandChanged(oldCommand, newCommand);
		}

		static void OnClearPlaceholderCommandParameterChanged(BindableObject bindable, object oldValue, object newValue)
		{
			((SearchHandler)bindable).OnClearPlaceholderCommandParameterChanged();
		}

		static void OnCommandChanged(BindableObject bindable, object oldValue, object newValue)
		{
			var self = (SearchHandler)bindable;
			var oldCommand = (ICommand)oldValue;
			var newCommand = (ICommand)newValue;
			self.OnCommandChanged(oldCommand, newCommand);
		}

		static void OnCommandParameterChanged(BindableObject bindable, object oldValue, object newValue)
		{
			((SearchHandler)bindable).OnCommandParameterChanged();
		}

		static void OnItemsSourceChanged(BindableObject bindable, object oldValue, object newValue)
		{
			var self = (SearchHandler)bindable;
			if (newValue == null)
				self.ListProxy = null;
			else
				self.ListProxy = new ListProxy((IEnumerable)newValue);
		}

		static void OnQueryChanged(BindableObject bindable, object oldValue, object newValue)
		{
			var searchHandler = (SearchHandler)bindable;
			searchHandler.OnQueryChanged((string)oldValue, (string)newValue);
		}

		void CanExecuteChanged(object sender, EventArgs e)
		{
			IsSearchEnabledCore = Command.CanExecute(CommandParameter);
		}

		void ClearPlaceholderCanExecuteChanged(object sender, EventArgs e)
		{
			ClearPlaceholderEnabledCore = ClearPlaceholderCommand.CanExecute(ClearPlaceholderCommandParameter);
		}

		void OnClearPlaceholderCommandChanged(ICommand oldCommand, ICommand newCommand)
		{
			if (oldCommand != null)
			{
				oldCommand.CanExecuteChanged -= ClearPlaceholderCanExecuteChanged;
			}

			if (newCommand != null)
			{
				newCommand.CanExecuteChanged += ClearPlaceholderCanExecuteChanged;
				ClearPlaceholderEnabledCore = ClearPlaceholderCommand.CanExecute(ClearPlaceholderCommandParameter);
			}
			else
			{
				ClearPlaceholderEnabledCore = true;
			}
		}

		void OnClearPlaceholderCommandParameterChanged()
		{
			if (ClearPlaceholderCommand != null)
				ClearPlaceholderEnabledCore = ClearPlaceholderCommand.CanExecute(CommandParameter);
		}

		void OnCommandChanged(ICommand oldCommand, ICommand newCommand)
		{
			if (oldCommand != null)
			{
				oldCommand.CanExecuteChanged -= CanExecuteChanged;
			}

			if (newCommand != null)
			{
				newCommand.CanExecuteChanged += CanExecuteChanged;
				IsSearchEnabledCore = Command.CanExecute(CommandParameter);
			}
			else
			{
				IsSearchEnabledCore = true;
			}
		}

		void OnCommandParameterChanged()
		{
			if (Command != null)
				IsSearchEnabledCore = Command.CanExecute(CommandParameter);
		}

		void UpdateAutomationProperties()
		{
			var queryIcon = QueryIcon;
			var clearIcon = ClearIcon;
			var clearPlaceholderIcon = ClearPlaceholderIcon;

			if (queryIcon != null)
			{
				var queryIconName = QueryIconName;
				var queryIconHelpText = QueryIconHelpText;
				if (queryIconName != null)
					AutomationProperties.SetName(queryIcon, queryIconName);

				if (queryIconHelpText != null)
					AutomationProperties.SetHelpText(queryIcon, queryIconHelpText);
			}

			if (clearIcon != null)
			{
				var clearIconName = ClearIconName;
				var clearIconHelpText = ClearIconHelpText;
				if (clearIconName != null)
					AutomationProperties.SetName(clearIcon, clearIconName);

				if (clearIconHelpText != null)
					AutomationProperties.SetHelpText(clearIcon, clearIconHelpText);
			}

			if (clearPlaceholderIcon != null)
			{
				var clearPlaceholderName = ClearPlaceholderName;
				var clearPlacholderHelpText = ClearPlaceholderHelpText;
				if (clearPlaceholderName != null)
					AutomationProperties.SetName(clearPlaceholderIcon, clearPlaceholderName);

				if (clearPlacholderHelpText != null)
					AutomationProperties.SetHelpText(clearPlaceholderIcon, clearPlacholderHelpText);
			}
		}
	}
}