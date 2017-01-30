using System;
using Xamarin.Forms;

namespace TwinTechs {

	public class MyPage : ContentPage {

		private StackLayout _buttonStackLayout;
		private Button _backButton;
		private Button _nextButton;

		public MyPage ( ) {

			BackgroundColor = Color.FromHex ( getRandColor ( ) );

			_backButton = new Button {
				Text = "GO BACK!",
			};
			_backButton.Clicked += HandleBackButtonClicked;

			_nextButton = new Button {
				Text = "CREATE NEW PAGE!",
			};
			_nextButton.Clicked += HandleNextButtonClicked;

			_buttonStackLayout = new StackLayout {
				Orientation = StackOrientation.Vertical,
				HorizontalOptions = LayoutOptions.Center,
				VerticalOptions = LayoutOptions.Center,
				Children = { _backButton, _nextButton }
			};

			Content = _buttonStackLayout;
		}

		private async void HandleBackButtonClicked( Object sender, EventArgs e ) {

			await App.CurrentNavigationPage.PopAsync ( true );
		}

		private async void HandleNextButtonClicked ( object sender, EventArgs e ) {
			
			await App.CurrentNavigationPage.PushAsync ( new MyPage ( ) );
		}

		private string getRandColor ( ) {
			Random rnd = new Random ( );
			string hexOutput = String.Format ( "{0:X}", rnd.Next ( 0, 0xFFFFFF ) );
			while (hexOutput.Length < 6)
				hexOutput = "0" + hexOutput;
			return hexOutput;
		}
	}
}