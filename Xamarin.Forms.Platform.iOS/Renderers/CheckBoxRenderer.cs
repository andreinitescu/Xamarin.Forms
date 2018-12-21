using System;
using System.ComponentModel;
using System.Diagnostics;
using UIKit;
using SizeF = CoreGraphics.CGSize;

namespace Xamarin.Forms.Platform.iOS
{
	public class CheckBoxRenderer : ViewRenderer<CheckBox, UIButton>
	{
		const float _titleLeftPadding = 5;
		const float _checkBoxSize = 25;
		const float _insetPadding = _titleLeftPadding + _checkBoxSize;
		CheckBoxCALayer _layer;

		TextButtonColorTracker<CheckBox> _txtBtnManager;

		// This looks like it should be a const under iOS Classic,
		// but that doesn't work under iOS 
		// ReSharper disable once BuiltInTypeReferenceStyle
		// Under iOS Classic Resharper wants to suggest this use the built-in type ref
		// but under iOS that suggestion won't work
		readonly nfloat _minimumButtonHeight = 44; // Apple docs 

		public bool IsDisposed { get; private set; }

		public CheckBoxRenderer() : base()
		{
			BorderElementManager.Init(this);
			ButtonElementManager.Init(this);
		}

		public override SizeF SizeThatFits(SizeF size)
		{
			var result = base.SizeThatFits(size);

			if (result.Height < _minimumButtonHeight)
			{
				result.Height = _minimumButtonHeight;
			}

			return result;
		}

		protected override void Dispose(bool disposing)
		{
			if (IsDisposed)
				return;

			if (Control != null)
			{
				Control.TouchUpInside -= OnButtonTouchUpInside;
				BorderElementManager.Dispose(this);
				ButtonElementManager.Dispose(this);
			}

			if (_txtBtnManager != null)
			{
				_txtBtnManager.Dispose();
				_txtBtnManager = null;
			}

			IsDisposed = true;

			base.Dispose(disposing);
		}

		protected override void OnElementChanged(ElementChangedEventArgs<CheckBox> e)
		{
			base.OnElementChanged(e);

			if (e.NewElement != null)
			{
				if (Control == null)
				{
					SetNativeControl(CreateNativeControl());

					Debug.Assert(Control != null, "Control != null");

					Control.TouchUpInside += OnButtonTouchUpInside;

					if (_txtBtnManager == null)
					{
						_txtBtnManager = new TextButtonColorTracker<CheckBox>(this);
					}

					_layer = new CheckBoxCALayer(Element, Control);
					Control.Layer.AddSublayer(_layer);
					_layer.SetNeedsLayout();
					_layer.SetNeedsDisplay();
				}

				UpdateCheck();
				SizeToFit();
				ComputeEdgeInset();
			}
		}

		protected override UIButton CreateNativeControl()
		{
			return new UIButton(UIButtonType.System)
			{
				HorizontalAlignment = UIControlContentHorizontalAlignment.Left,

			};
		}

		protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged(sender, e);

			if (e.PropertyName == VisualElement.IsEnabledProperty.PropertyName)
			{
				_layer.SetNeedsDisplay();
			}
			else if (e.PropertyName == CheckBox.PaddingProperty.PropertyName)
			{
				ComputeEdgeInset();
			}
			else if (e.PropertyName == CheckBox.IsCheckedProperty.PropertyName)
			{
				UpdateCheck();
			}
		}

		protected override void SetAccessibilityLabel()
		{
			// If we have not specified an AccessibilityLabel and the AccessibilityLabel is currently bound to the Title,
			// exit this method so we don't set the AccessibilityLabel value and break the binding.
			// This may pose a problem for users who want to explicitly set the AccessibilityLabel to null, but this
			// will prevent us from inadvertently breaking UI Tests that are using Query.Marked to get the dynamic Title 
			// of the Button.

			var elemValue = (string)Element?.GetValue(AutomationProperties.NameProperty);
			if (string.IsNullOrWhiteSpace(elemValue) && Control?.AccessibilityLabel == Control?.Title(UIControlState.Normal))
				return;

			base.SetAccessibilityLabel();
		}

		protected virtual void UpdateCheck()
		{
			_layer?.SetNeedsDisplay();
		}

		void ComputeEdgeInset()
		{
			if (Control == null)
			{
				return;
			}

			Control.ContentEdgeInsets = new UIEdgeInsets(
				top: 0,
				left: 0,
				bottom: 0,
				right: -_insetPadding * 2
			);

			Control.TitleEdgeInsets = new UIEdgeInsets(
				top: 0,
				left: _insetPadding,
				bottom: 0,
				right: -_insetPadding);
		}

		void OnButtonTouchUpInside(object sender, EventArgs eventArgs)
		{
			((IToggleButtonController)Element).SetIsChecked(!Element.IsChecked);
		}
	}
}