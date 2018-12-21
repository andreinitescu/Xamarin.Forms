using System;
using System.ComponentModel;
using UIKit;
using Xamarin.Forms.Internals;

namespace Xamarin.Forms.Platform.iOS
{
	internal class TextButtonColorTracker<TButton> : IDisposable
		where TButton : VisualElement, IFontElement, ITextElement, IElementConfiguration<TButton>, ITextButtonElement
	{
		readonly IVisualNativeElementRenderer _renderer;

		bool _disposed;
		UIColor _buttonTextColorDefaultDisabled;
		UIColor _buttonTextColorDefaultHighlighted;
		UIColor _buttonTextColorDefaultNormal;
		bool _useLegacyColorManagement;

		TButton Element => (TButton)_renderer.Element;
		UIButton Control => (UIButton)_renderer.Control;

		internal TextButtonColorTracker(IVisualNativeElementRenderer renderer)
		{
			_renderer = renderer;
			Init(true);

			UpdateText();
			UpdateFont();
			UpdateTextColor();
		}

		public void Dispose()
		{
			Dispose(true);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (_disposed)
				return;

			_disposed = true;

			if (disposing)
			{
				Init(false);
			}
		}

		void Init(bool init)
		{
			if (init)
			{
				_renderer.ControlChanged += OnControlChanged;
				_renderer.ElementPropertyChanged += OnElementPropertyChanged;
			}
			else
			{
				_renderer.ControlChanged -= OnControlChanged;
				_renderer.ElementPropertyChanged -= OnElementPropertyChanged;
			}
		}

		void OnControlChanged(object sender, EventArgs e)
		{
			var renderer = (IVisualNativeElementRenderer)sender;
			var control = (UIButton)renderer.Control;
			var element = (TButton)renderer.Element;

			_useLegacyColorManagement = element.UseLegacyColorManagement();

			_buttonTextColorDefaultNormal = control.TitleColor(UIControlState.Normal);
			_buttonTextColorDefaultHighlighted = control.TitleColor(UIControlState.Highlighted);
			_buttonTextColorDefaultDisabled = control.TitleColor(UIControlState.Disabled);
		}

		void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (e.PropertyName == Button.TextProperty.PropertyName)
				UpdateText();
			else if (e.PropertyName == Button.TextColorProperty.PropertyName)
				UpdateTextColor();
			else if (e.PropertyName == Button.FontProperty.PropertyName)
				UpdateFont();
		}

		void UpdateFont()
		{
			Control.TitleLabel.Font = Element.ToUIFont();
		}

		void UpdateText()
		{
			var newText = Element.Text;

			if (Control.Title(UIControlState.Normal) != newText)
			{
				Control.SetTitle(newText, UIControlState.Normal);
			}
		}

		void UpdateTextColor()
		{
			if (Element.TextColor == Color.Default)
			{
				Control.SetTitleColor(_buttonTextColorDefaultNormal, UIControlState.Normal);
				Control.SetTitleColor(_buttonTextColorDefaultHighlighted, UIControlState.Highlighted);
				Control.SetTitleColor(_buttonTextColorDefaultDisabled, UIControlState.Disabled);
			}
			else
			{
				var color = Element.TextColor.ToUIColor();

				Control.SetTitleColor(color, UIControlState.Normal);
				Control.SetTitleColor(color, UIControlState.Highlighted);
				Control.SetTitleColor(_useLegacyColorManagement ? _buttonTextColorDefaultDisabled : color, UIControlState.Disabled);

				Control.TintColor = color;
			}
		}
	}
}