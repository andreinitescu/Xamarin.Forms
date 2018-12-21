using System;

namespace Xamarin.Forms.Controls
{
	public class CheckBoxGallery : ContentPage
	{
		public CheckBoxGallery()
		{
			BackgroundColor = new Color(0.9);

			var normal = new CheckBox { Text = "Normal Button" };
			normal.Effects.Add(Effect.Resolve($"{Issues.Effects.ResolutionGroupName}.BorderEffect"));

			var disabled = new CheckBox { Text = "Disabled Button" };
			var disabledswitch = new Switch();
			disabledswitch.SetBinding(Switch.IsToggledProperty, "IsEnabled");
			disabledswitch.BindingContext = disabled;

			var canTapLabel = new Label
			{
				Text = "Cannot Tap"
			};

			disabled.Clicked += (sender, e) => {
				canTapLabel.Text = "TAPPED!";
			};

			var click = new CheckBox { Text = "Click Button" };
			var rotate = new CheckBox { Text = "Rotate Button" };
			var transparent = new CheckBox { Text = "Transparent Button" };
			string fontName;
			switch (Device.RuntimePlatform)
			{
				default:
				case Device.iOS:
					fontName = "Georgia";
					break;
				case Device.Android:
					fontName = "sans-serif-light";
					break;
				case Device.UWP:
					fontName = "Comic Sans MS";
					break;
			}

			var font = Font.OfSize(fontName, NamedSize.Medium);

			var themedButton = new CheckBox
			{
				Text = "Accent Button",
				BackgroundColor = Color.Accent,
				TextColor = Color.White,
				ClassId = "AccentButton",
				Font = font
			};
			var borderButton = new CheckBox
			{
				Text = "Border Button",
				BorderColor = Color.Black,
				BackgroundColor = Color.Purple,
				BorderWidth = 5,
				CornerRadius = 5
			};
			var timer = new CheckBox { Text = "Timer" };
			var busy = new CheckBox { Text = "Toggle Busy" };
			var alert = new CheckBox { Text = "Alert" };
			var alertSingle = new CheckBox { Text = "Alert Single" };

			themedButton.Clicked += (sender, args) => themedButton.Font = Font.Default;

			alertSingle.Clicked += (sender, args) => DisplayAlert("Foo", "Bar", "Cancel");

			disabled.IsEnabled = false;
			int i = 1;
			click.Clicked += (sender, e) => { click.Text = "Clicked " + i++; };
			rotate.Clicked += (sender, e) => rotate.RelRotateTo(180);
			transparent.Opacity = .5;

			int j = 1;
			timer.Clicked += (sender, args) => Device.StartTimer(TimeSpan.FromSeconds(1), () => {
				timer.Text = "Timer Elapsed " + j++;
				return j < 4;
			});

			bool isBusy = false;
			busy.Clicked += (sender, args) => IsBusy = isBusy = !isBusy;

			alert.Clicked += async (sender, args) => {
				var result = await DisplayAlert("User Alert", "This is a user alert. This is only a user alert.", "Accept", "Cancel");
				alert.Text = result ? "Accepted" : "Cancelled";
			};

			borderButton.Clicked += (sender, args) => borderButton.BackgroundColor = Color.Default;

			Content = new ScrollView
			{
				Content = new StackLayout
				{
					Padding = new Size(20, 20),
					Children = {
						normal,
						new StackLayout {
							Orientation = StackOrientation.Horizontal,
							Children={
								disabled,
								disabledswitch,
							},
						},
						canTapLabel,
						click,
						rotate,
						transparent,
						themedButton,
						borderButton,
						new CheckBox {Text = "Thin Border", BorderWidth = 1, BackgroundColor=Color.White, BorderColor = Color.Black, TextColor = Color.Black},
						new CheckBox {Text = "Thinner Border", BorderWidth = .5, BackgroundColor=Color.White, BorderColor = Color.Black, TextColor = Color.Black},
						new CheckBox {Text = "BorderWidth == 0", BorderWidth = 0, BackgroundColor=Color.White, BorderColor = Color.Black, TextColor = Color.Black},
						timer,
						busy,
						alert,
						alertSingle,
					}
				}
			};
		}
	}
}
