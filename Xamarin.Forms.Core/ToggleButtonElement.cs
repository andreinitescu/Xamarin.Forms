using System;
using System.ComponentModel;

namespace Xamarin.Forms
{
	public interface IToggleButtonController
	{
		void SetIsChecked(bool isChecked);
	}

	[EditorBrowsable(EditorBrowsableState.Never)]
	interface IToggleButtonElement
	{
		event EventHandler Checked;
		event EventHandler Unchecked;

		bool IsChecked { get; set; }

		void RaiseCheckedEvent(bool isChecked);
	}

	static class ToggleButtonElement
	{
		public static readonly BindableProperty IsCheckedProperty = BindableProperty.Create(nameof(IToggleButtonElement.IsChecked), typeof(bool), typeof(IToggleButtonElement), false, propertyChanged: (b, o, n) => OnIsCheckedChanged((IToggleButtonElement)b, (bool)n));

		static void OnIsCheckedChanged(IToggleButtonElement btn, bool isChecked)
		{
			btn.IsChecked = isChecked;
			btn.RaiseCheckedEvent(isChecked);
		}
	}
}