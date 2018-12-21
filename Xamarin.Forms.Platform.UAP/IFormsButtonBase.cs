using Windows.UI.Xaml.Media;

namespace Xamarin.Forms.Platform.UWP
{
	public interface IFormsButtonBase
	{
		Brush BackgroundColor { set; }

		int BorderRadius { set; }
	}
}