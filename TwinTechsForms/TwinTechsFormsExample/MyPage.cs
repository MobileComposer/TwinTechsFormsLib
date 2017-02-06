using System;
using Xamarin.Forms;

namespace TwinTechs
{

    public class MyPage : ContentPage
    {
        private int _pageIndex;

        private StackLayout _buttonStackLayout;
        private Button _backButton;
        private Button _nextButton;

        public MyPage(int pageIndex)
        {
            _pageIndex = pageIndex;

            Title = "MyPage #" + pageIndex;

            BackgroundColor = Color.FromHex(GetRandColor());

            _backButton = new Button
            {
                Text = "GO BACK!",
                //WidthRequest = 100, // testing these to see if it makes this control appear on UWP
                //HeightRequest = 50,
                //HorizontalOptions = LayoutOptions.CenterAndExpand
            };
            _backButton.Clicked += HandleBackButtonClicked;

            _nextButton = new Button
            {
                Text = "CREATE NEW PAGE!",
                //WidthRequest = 200, // testing these to see if it makes this control appear on UWP
                //HeightRequest = 100,
                //HorizontalOptions = LayoutOptions.CenterAndExpand
            };
            _nextButton.Clicked += HandleNextButtonClicked;

            _buttonStackLayout = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                Padding = 20,
                //HorizontalOptions = LayoutOptions.Center,
                //VerticalOptions = LayoutOptions.Center,

                Children = { _backButton, _nextButton }
            };

            Content = _buttonStackLayout;
        }

        private async void HandleBackButtonClicked(Object sender, EventArgs e)
        {
            await App.CurrentNavigationPage.PopAsync(true);
        }

        private async void HandleNextButtonClicked(object sender, EventArgs e)
        {
            await App.CurrentNavigationPage.PushAsync(new MyPage(++_pageIndex));
        }

        private string GetRandColor()
        {
            Random rnd = new Random();
            string hexOutput = String.Format("{0:X}", rnd.Next(0, 0xFFFFFF));
            while (hexOutput.Length < 6)
                hexOutput = "0" + hexOutput;
            return hexOutput;
        }
    }
}