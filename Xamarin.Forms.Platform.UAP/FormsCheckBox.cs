using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;

using WContentPresenter = Windows.UI.Xaml.Controls.ContentPresenter;

namespace Xamarin.Forms.Platform.UWP
{
	public class FormsCheckBox : Windows.UI.Xaml.Controls.CheckBox, IFormsButtonBase
	{
		public static readonly DependencyProperty BorderRadiusProperty = DependencyProperty.Register(nameof(BorderRadius), typeof(int), typeof(FormsCheckBox),
			new PropertyMetadata(default(int), OnBorderRadiusChanged));

		public static readonly DependencyProperty BackgroundColorProperty = DependencyProperty.Register(nameof(BackgroundColor), typeof(Brush), typeof(FormsCheckBox),
			new PropertyMetadata(default(Brush), OnBackgroundColorChanged));

		WContentPresenter _contentPresenter;

		public Brush BackgroundColor
		{
			get
			{
				return (Brush)GetValue(BackgroundColorProperty);
			}
			set
			{
				SetValue(BackgroundColorProperty, value);
			}
		}

		public int BorderRadius
		{
			get
			{
				return (int)GetValue(BorderRadiusProperty);
			}
			set
			{
				SetValue(BorderRadiusProperty, value);
			}
		}

		protected override void OnApplyTemplate()
		{
			base.OnApplyTemplate();

			_contentPresenter = GetTemplateChild("ContentPresenter") as WContentPresenter;

			UpdateBackgroundColor();
			UpdateBorderRadius();
		}

		static void OnBackgroundColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			((FormsCheckBox)d).UpdateBackgroundColor();
		}

		static void OnBorderRadiusChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			((FormsCheckBox)d).UpdateBorderRadius();
		}

		void UpdateBackgroundColor()
		{
			if (BackgroundColor == null)
				BackgroundColor = Background;

			if (_contentPresenter != null)
				_contentPresenter.Background = BackgroundColor;
			Background = Color.Transparent.ToBrush();
		}

		void UpdateBorderRadius()
		{
			if (_contentPresenter != null)
				_contentPresenter.CornerRadius = new Windows.UI.Xaml.CornerRadius(BorderRadius);
		}
	}
}