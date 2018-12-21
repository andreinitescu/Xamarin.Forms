using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Platform;

namespace Xamarin.Forms
{
	[RenderWith(typeof(_CheckBoxRenderer))]
	public class CheckBox : View, IFontElement, ITextElement, IBorderElement, IButtonController, IElementConfiguration<CheckBox>, IPaddingElement, IViewController, IButtonElement, ITextButtonElement, IToggleButtonElement, IToggleButtonController
	{
		#region CheckBox specific implementation

		public static readonly BindableProperty IsCheckedProperty = ToggleButtonElement.IsCheckedProperty;

		public event EventHandler Checked;
		public event EventHandler Unchecked;

		public bool IsChecked
		{
			get { return (bool)GetValue(IsCheckedProperty); }
			set { SetValue(IsCheckedProperty, value); }
		}

		void IToggleButtonElement.RaiseCheckedEvent(bool isChecked)
		{
			if (isChecked)
			{
				Checked?.Invoke(this, EventArgs.Empty);
			}
			else
			{
				Unchecked?.Invoke(this, EventArgs.Empty);
			}
		}

		void IToggleButtonController.SetIsChecked(bool isChecked)
		{
			IsChecked = isChecked;
		}

		#endregion

		const int DefaultBorderRadius = 5;
		const double DefaultSpacing = 10;

		public static readonly BindableProperty CommandProperty = ButtonElement.CommandProperty;

		public static readonly BindableProperty CommandParameterProperty = ButtonElement.CommandParameterProperty;

		public static readonly BindableProperty TextProperty = BindableProperty.Create("Text", typeof(string), typeof(CheckBox), null,
			propertyChanged: (bindable, oldVal, newVal) => ((CheckBox)bindable).InvalidateMeasureInternal(InvalidationTrigger.MeasureChanged));

		public static readonly BindableProperty TextColorProperty = TextElement.TextColorProperty;

		public static readonly BindableProperty FontProperty = FontElement.FontProperty;

		public static readonly BindableProperty FontFamilyProperty = FontElement.FontFamilyProperty;

		public static readonly BindableProperty FontSizeProperty = FontElement.FontSizeProperty;

		public static readonly BindableProperty FontAttributesProperty = FontElement.FontAttributesProperty;

		public static readonly BindableProperty BorderWidthProperty = BindableProperty.Create("BorderWidth", typeof(double), typeof(CheckBox), -1d);

		public static readonly BindableProperty BorderColorProperty = BorderElement.BorderColorProperty;

		public static readonly BindableProperty CornerRadiusProperty = BorderElement.CornerRadiusProperty;

		public static readonly BindableProperty PaddingProperty = PaddingElement.PaddingProperty;

		public Thickness Padding
		{
			get { return (Thickness)GetValue(PaddingElement.PaddingProperty); }
			set { SetValue(PaddingElement.PaddingProperty, value); }
		}

		Thickness IPaddingElement.PaddingDefaultValueCreator()
		{
			return default(Thickness);
		}

		void IPaddingElement.OnPaddingPropertyChanged(Thickness oldValue, Thickness newValue)
		{
			InvalidateMeasureInternal(InvalidationTrigger.MeasureChanged);
		}


		internal static readonly BindablePropertyKey IsPressedPropertyKey = BindableProperty.CreateReadOnly(nameof(IsPressed), typeof(bool), typeof(CheckBox), default(bool));
		public static readonly BindableProperty IsPressedProperty = IsPressedPropertyKey.BindableProperty;


		readonly Lazy<PlatformConfigurationRegistry<CheckBox>> _platformConfigurationRegistry;

		public Color BorderColor
		{
			get { return (Color)GetValue(BorderElement.BorderColorProperty); }
			set { SetValue(BorderElement.BorderColorProperty, value); }
		}

		public int CornerRadius
		{
			get { return (int)GetValue(CornerRadiusProperty); }
			set { SetValue(CornerRadiusProperty, value); }
		}

		public double BorderWidth
		{
			get { return (double)GetValue(BorderWidthProperty); }
			set { SetValue(BorderWidthProperty, value); }
		}

		public ICommand Command
		{
			get { return (ICommand)GetValue(CommandProperty); }
			set { SetValue(CommandProperty, value); }
		}

		public object CommandParameter
		{
			get { return GetValue(CommandParameterProperty); }
			set { SetValue(CommandParameterProperty, value); }
		}

		public Font Font
		{
			get { return (Font)GetValue(FontProperty); }
			set { SetValue(FontProperty, value); }
		}

		public string Text
		{
			get { return (string)GetValue(TextProperty); }
			set { SetValue(TextProperty, value); }
		}

		public Color TextColor
		{
			get { return (Color)GetValue(TextElement.TextColorProperty); }
			set { SetValue(TextElement.TextColorProperty, value); }
		}

		bool IButtonElement.IsEnabledCore
		{
			set { SetValueCore(IsEnabledProperty, value); }
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		public void SendClicked() => ButtonElement.ElementClicked(this, this);

		public bool IsPressed => (bool)GetValue(IsPressedProperty);

		[EditorBrowsable(EditorBrowsableState.Never)]
		void IButtonElement.SetIsPressed(bool isPressed) => SetValue(IsPressedPropertyKey, isPressed);

		[EditorBrowsable(EditorBrowsableState.Never)]
		public void SendPressed() => ButtonElement.ElementPressed(this, this);

		[EditorBrowsable(EditorBrowsableState.Never)]
		public void SendReleased() => ButtonElement.ElementReleased(this, this);

		[EditorBrowsable(EditorBrowsableState.Never)]
		void IButtonElement.PropagateUpClicked() => Clicked?.Invoke(this, EventArgs.Empty);

		[EditorBrowsable(EditorBrowsableState.Never)]
		void IButtonElement.PropagateUpPressed() => Pressed?.Invoke(this, EventArgs.Empty);

		[EditorBrowsable(EditorBrowsableState.Never)]
		void IButtonElement.PropagateUpReleased() => Released?.Invoke(this, EventArgs.Empty);

		public FontAttributes FontAttributes
		{
			get { return (FontAttributes)GetValue(FontAttributesProperty); }
			set { SetValue(FontAttributesProperty, value); }
		}

		public string FontFamily
		{
			get { return (string)GetValue(FontFamilyProperty); }
			set { SetValue(FontFamilyProperty, value); }
		}

		[TypeConverter(typeof(FontSizeConverter))]
		public double FontSize
		{
			get { return (double)GetValue(FontSizeProperty); }
			set { SetValue(FontSizeProperty, value); }
		}

		public event EventHandler Clicked;
		public event EventHandler Pressed;

		public event EventHandler Released;

		public CheckBox()
		{
			_platformConfigurationRegistry = new Lazy<PlatformConfigurationRegistry<CheckBox>>(
				() => new PlatformConfigurationRegistry<CheckBox>(this));
		}

		public IPlatformElementConfiguration<T, CheckBox> On<T>() where T : IConfigPlatform
		{
			return _platformConfigurationRegistry.Value.On<T>();
		}

		protected internal override void ChangeVisualState()
		{
			if (IsEnabled && IsPressed)
			{
				VisualStateManager.GoToState(this, ButtonElement.PressedVisualState);
			}
			else
			{
				base.ChangeVisualState();
			}
		}

		void IFontElement.OnFontFamilyChanged(string oldValue, string newValue) =>
			InvalidateMeasureInternal(InvalidationTrigger.MeasureChanged);

		void IFontElement.OnFontSizeChanged(double oldValue, double newValue) =>
			InvalidateMeasureInternal(InvalidationTrigger.MeasureChanged);

		double IFontElement.FontSizeDefaultValueCreator() =>
			Device.GetNamedSize(NamedSize.Default, this);

		void IFontElement.OnFontAttributesChanged(FontAttributes oldValue, FontAttributes newValue) =>
			InvalidateMeasureInternal(InvalidationTrigger.MeasureChanged);

		void IFontElement.OnFontChanged(Font oldValue, Font newValue) =>
			InvalidateMeasureInternal(InvalidationTrigger.MeasureChanged);

		int IBorderElement.CornerRadiusDefaultValue => (int)CornerRadiusProperty.DefaultValue;

		Color IBorderElement.BorderColorDefaultValue => (Color)BorderColorProperty.DefaultValue;

		double IBorderElement.BorderWidthDefaultValue => (double)BorderWidthProperty.DefaultValue;

		void ITextElement.OnTextColorPropertyChanged(Color oldValue, Color newValue)
		{
		}

		void IBorderElement.OnBorderColorPropertyChanged(Color oldValue, Color newValue)
		{
		}

		void IButtonElement.OnCommandCanExecuteChanged(object sender, EventArgs e) =>
			ButtonElement.CommandCanExecuteChanged(this, EventArgs.Empty);

		bool IBorderElement.IsCornerRadiusSet() => IsSet(CornerRadiusProperty);
		bool IBorderElement.IsBackgroundColorSet() => IsSet(BackgroundColorProperty);
		bool IBorderElement.IsBorderColorSet() => IsSet(BorderColorProperty);
		bool IBorderElement.IsBorderWidthSet() => IsSet(BorderWidthProperty);

		protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			base.OnPropertyChanged(propertyName);

			if (propertyName == VisualProperty.PropertyName ||
				propertyName == BackgroundColorProperty.PropertyName ||
				propertyName == TextColorProperty.PropertyName)
			{
				// Todo fix reset behavior if user sets back 
				if ((this as IVisualController).EffectiveVisual == VisualMarker.Material)
				{
					if (BackgroundColor == Color.Default)
						BackgroundColor = Color.Black;

					if (TextColor == Color.Default)
						TextColor = Color.White;
				}
			}
		}
	}

	//public class ToggleButton<TToggleButton> : View, IFontElement, ITextElement, IBorderElement, IButtonController, IElementConfiguration<TToggleButton>, IPaddingElement, IViewController, IButtonElement, ITextButtonElement, IToggleButtonElement, IToggleButtonController
	//	where TToggleButton : Element
	//{
	//	#region ToggleButton specific implementation

	//	public static readonly BindableProperty IsCheckedProperty = ToggleButtonElement.IsCheckedProperty;

	//	public event EventHandler Checked;
	//	public event EventHandler Unchecked;

	//	public bool IsChecked
	//	{
	//		get { return (bool)GetValue(IsCheckedProperty); }
	//		set { SetValue(IsCheckedProperty, value); }
	//	}

	//	void IToggleButtonElement.RaiseCheckedEvent(bool isChecked)
	//	{
	//		if (isChecked)
	//		{
	//			Checked?.Invoke(this, EventArgs.Empty);
	//		}
	//		else
	//		{
	//			Unchecked?.Invoke(this, EventArgs.Empty);
	//		}
	//	}

	//	void IToggleButtonController.SetIsChecked(bool isChecked)
	//	{
	//		IsChecked = isChecked;
	//	}

	//	#endregion

	//	const int DefaultBorderRadius = 5;
	//	const double DefaultSpacing = 10;

	//	public static readonly BindableProperty CommandProperty = ButtonElement.CommandProperty;

	//	public static readonly BindableProperty CommandParameterProperty = ButtonElement.CommandParameterProperty;

	//	public static readonly BindableProperty TextProperty = BindableProperty.Create("Text", typeof(string), typeof(Button), null,
	//		propertyChanged: (bindable, oldVal, newVal) => ((Button)bindable).InvalidateMeasureInternal(InvalidationTrigger.MeasureChanged));

	//	public static readonly BindableProperty TextColorProperty = TextElement.TextColorProperty;

	//	public static readonly BindableProperty FontProperty = FontElement.FontProperty;

	//	public static readonly BindableProperty FontFamilyProperty = FontElement.FontFamilyProperty;

	//	public static readonly BindableProperty FontSizeProperty = FontElement.FontSizeProperty;

	//	public static readonly BindableProperty FontAttributesProperty = FontElement.FontAttributesProperty;

	//	public static readonly BindableProperty BorderWidthProperty = BindableProperty.Create("BorderWidth", typeof(double), typeof(Button), -1d);

	//	public static readonly BindableProperty BorderColorProperty = BorderElement.BorderColorProperty;

	//	public static readonly BindableProperty CornerRadiusProperty = BorderElement.CornerRadiusProperty;

	//	public static readonly BindableProperty PaddingProperty = PaddingElement.PaddingProperty;

	//	public Thickness Padding
	//	{
	//		get { return (Thickness)GetValue(PaddingElement.PaddingProperty); }
	//		set { SetValue(PaddingElement.PaddingProperty, value); }
	//	}

	//	Thickness IPaddingElement.PaddingDefaultValueCreator()
	//	{
	//		return default(Thickness);
	//	}

	//	void IPaddingElement.OnPaddingPropertyChanged(Thickness oldValue, Thickness newValue)
	//	{
	//		InvalidateMeasureInternal(InvalidationTrigger.MeasureChanged);
	//	}


	//	internal static readonly BindablePropertyKey IsPressedPropertyKey = BindableProperty.CreateReadOnly(nameof(IsPressed), typeof(bool), typeof(Button), default(bool));
	//	public static readonly BindableProperty IsPressedProperty = IsPressedPropertyKey.BindableProperty;


	//	readonly Lazy<PlatformConfigurationRegistry<TToggleButton>> _platformConfigurationRegistry;

	//	public Color BorderColor
	//	{
	//		get { return (Color)GetValue(BorderElement.BorderColorProperty); }
	//		set { SetValue(BorderElement.BorderColorProperty, value); }
	//	}

	//	public int CornerRadius
	//	{
	//		get { return (int)GetValue(CornerRadiusProperty); }
	//		set { SetValue(CornerRadiusProperty, value); }
	//	}

	//	public double BorderWidth
	//	{
	//		get { return (double)GetValue(BorderWidthProperty); }
	//		set { SetValue(BorderWidthProperty, value); }
	//	}

	//	public ICommand Command
	//	{
	//		get { return (ICommand)GetValue(CommandProperty); }
	//		set { SetValue(CommandProperty, value); }
	//	}

	//	public object CommandParameter
	//	{
	//		get { return GetValue(CommandParameterProperty); }
	//		set { SetValue(CommandParameterProperty, value); }
	//	}

	//	public Font Font
	//	{
	//		get { return (Font)GetValue(FontProperty); }
	//		set { SetValue(FontProperty, value); }
	//	}

	//	public string Text
	//	{
	//		get { return (string)GetValue(TextProperty); }
	//		set { SetValue(TextProperty, value); }
	//	}

	//	public Color TextColor
	//	{
	//		get { return (Color)GetValue(TextElement.TextColorProperty); }
	//		set { SetValue(TextElement.TextColorProperty, value); }
	//	}

	//	bool IButtonElement.IsEnabledCore
	//	{
	//		set { SetValueCore(IsEnabledProperty, value); }
	//	}

	//	[EditorBrowsable(EditorBrowsableState.Never)]
	//	public void SendClicked() => ButtonElement.ElementClicked(this, this);

	//	public bool IsPressed => (bool)GetValue(IsPressedProperty);

	//	[EditorBrowsable(EditorBrowsableState.Never)]
	//	void IButtonElement.SetIsPressed(bool isPressed) => SetValue(IsPressedPropertyKey, isPressed);

	//	[EditorBrowsable(EditorBrowsableState.Never)]
	//	public void SendPressed() => ButtonElement.ElementPressed(this, this);

	//	[EditorBrowsable(EditorBrowsableState.Never)]
	//	public void SendReleased() => ButtonElement.ElementReleased(this, this);

	//	[EditorBrowsable(EditorBrowsableState.Never)]
	//	void IButtonElement.PropagateUpClicked() => Clicked?.Invoke(this, EventArgs.Empty);

	//	[EditorBrowsable(EditorBrowsableState.Never)]
	//	void IButtonElement.PropagateUpPressed() => Pressed?.Invoke(this, EventArgs.Empty);

	//	[EditorBrowsable(EditorBrowsableState.Never)]
	//	void IButtonElement.PropagateUpReleased() => Released?.Invoke(this, EventArgs.Empty);

	//	public FontAttributes FontAttributes
	//	{
	//		get { return (FontAttributes)GetValue(FontAttributesProperty); }
	//		set { SetValue(FontAttributesProperty, value); }
	//	}

	//	public string FontFamily
	//	{
	//		get { return (string)GetValue(FontFamilyProperty); }
	//		set { SetValue(FontFamilyProperty, value); }
	//	}

	//	[TypeConverter(typeof(FontSizeConverter))]
	//	public double FontSize
	//	{
	//		get { return (double)GetValue(FontSizeProperty); }
	//		set { SetValue(FontSizeProperty, value); }
	//	}

	//	public event EventHandler Clicked;
	//	public event EventHandler Pressed;

	//	public event EventHandler Released;

	//	public ToggleButton()
	//	{
	//		_platformConfigurationRegistry = new Lazy<PlatformConfigurationRegistry<TToggleButton>>(
	//			() => new PlatformConfigurationRegistry<TToggleButton>(this as TToggleButton));
	//	}

	//	public IPlatformElementConfiguration<T, TToggleButton> On<T>() where T : IConfigPlatform
	//	{
	//		return _platformConfigurationRegistry.Value.On<T>();
	//	}

	//	protected internal override void ChangeVisualState()
	//	{
	//		if (IsEnabled && IsPressed)
	//		{
	//			VisualStateManager.GoToState(this, ButtonElement.PressedVisualState);
	//		}
	//		else
	//		{
	//			base.ChangeVisualState();
	//		}
	//	}

	//	void IFontElement.OnFontFamilyChanged(string oldValue, string newValue) =>
	//		InvalidateMeasureInternal(InvalidationTrigger.MeasureChanged);

	//	void IFontElement.OnFontSizeChanged(double oldValue, double newValue) =>
	//		InvalidateMeasureInternal(InvalidationTrigger.MeasureChanged);

	//	double IFontElement.FontSizeDefaultValueCreator() =>
	//		Device.GetNamedSize(NamedSize.Default, this);

	//	void IFontElement.OnFontAttributesChanged(FontAttributes oldValue, FontAttributes newValue) =>
	//		InvalidateMeasureInternal(InvalidationTrigger.MeasureChanged);

	//	void IFontElement.OnFontChanged(Font oldValue, Font newValue) =>
	//		InvalidateMeasureInternal(InvalidationTrigger.MeasureChanged);

	//	int IBorderElement.CornerRadiusDefaultValue => (int)CornerRadiusProperty.DefaultValue;

	//	Color IBorderElement.BorderColorDefaultValue => (Color)BorderColorProperty.DefaultValue;

	//	double IBorderElement.BorderWidthDefaultValue => (double)BorderWidthProperty.DefaultValue;

	//	void ITextElement.OnTextColorPropertyChanged(Color oldValue, Color newValue)
	//	{
	//	}

	//	void IBorderElement.OnBorderColorPropertyChanged(Color oldValue, Color newValue)
	//	{
	//	}

	//	void IButtonElement.OnCommandCanExecuteChanged(object sender, EventArgs e) =>
	//		ButtonElement.CommandCanExecuteChanged(this, EventArgs.Empty);

	//	bool IBorderElement.IsCornerRadiusSet() => IsSet(CornerRadiusProperty);
	//	bool IBorderElement.IsBackgroundColorSet() => IsSet(BackgroundColorProperty);
	//	bool IBorderElement.IsBorderColorSet() => IsSet(BorderColorProperty);
	//	bool IBorderElement.IsBorderWidthSet() => IsSet(BorderWidthProperty);

	//	protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
	//	{
	//		base.OnPropertyChanged(propertyName);

	//		if (propertyName == VisualProperty.PropertyName ||
	//			propertyName == BackgroundColorProperty.PropertyName ||
	//			propertyName == TextColorProperty.PropertyName)
	//		{
	//			// Todo fix reset behavior if user sets back 
	//			if ((this as IVisualController).EffectiveVisual == VisualMarker.Material)
	//			{
	//				if (BackgroundColor == Color.Default)
	//					BackgroundColor = Color.Black;

	//				if (TextColor == Color.Default)
	//					TextColor = Color.White;
	//			}
	//		}
	//	}
	//}

	//[RenderWith(typeof(_CheckBoxRenderer))]
}

/*[RenderWith(typeof(_CheckBoxRenderer))]
public class CheckBox : Button, IElementConfiguration<CheckBox>
{
	public static readonly BindableProperty IsCheckedProperty = BindableProperty.Create("IsChecked", typeof(bool), typeof(CheckBox), false);

	public event EventHandler Checked;
	public event EventHandler Unchecked;

	readonly Lazy<PlatformConfigurationRegistry<CheckBox>> _platformConfigurationRegistry;

	public bool IsChecked
	{
		get { return (bool)GetValue(IsCheckedProperty); }
		set { SetValue(IsCheckedProperty, value); }
	}

	public CheckBox()
	{
		_platformConfigurationRegistry = new Lazy<PlatformConfigurationRegistry<CheckBox>>(() => new PlatformConfigurationRegistry<CheckBox>(this));
		Clicked += CheckBoxClicked;
	}

	void CheckBoxClicked(object sender, EventArgs e)
	{
		IsChecked = !IsChecked;

		if (IsChecked)
		{
			Checked?.Invoke(this, null);
		}
		else
		{
			Unchecked?.Invoke(this, null);
		}
	}

	IPlatformElementConfiguration<T, CheckBox> IElementConfiguration<CheckBox>.On<T>()
	{
		return _platformConfigurationRegistry.Value.On<T>();
	}
}*/
