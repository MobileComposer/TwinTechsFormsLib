using System;

using Xamarin.Forms;
using TwinTechs.Example;
using TwinTechs.Example.GridView;
using TwinTechs.Controls;

namespace TwinTechs
{
    public class App : Application
    {
        private Grid _grid;
        private ContentPage _mainContentPage;

        private StackLayout _tabStackLayout;
        private PageViewContainer _pageViewContainer;

        private BoxView _box1;
        private MyNavigationPage _navPage1;
        private MyPage _contentPage1;

        private BoxView _box2;
        private MyNavigationPage _navPage2;
        private MyPage _contentPage2;

        private static MyNavigationPage _currentNavigationPage;
        public static MyNavigationPage CurrentNavigationPage
        {
            get { return _currentNavigationPage; }
            set { _currentNavigationPage = value; }
        }

		private static IBack _appBack;
		public static IBack AppBack {
			get { return _appBack; }
			set { _appBack = value; }
		}

        public App()
        {
            _grid = new Grid
            {
                RowSpacing = 0,
                ColumnSpacing = 0,
                ColumnDefinitions = new ColumnDefinitionCollection
                {
                    new ColumnDefinition { Width = 100 },
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) }
                },
                RowDefinitions = new RowDefinitionCollection
                {
                    new RowDefinition { Height = new GridLength( 1, GridUnitType.Star) }
                }
            };

            _box1 = new BoxView
            {
                BackgroundColor = Color.Red,
                WidthRequest = 100,
                HeightRequest = 100
            };
            _box1.GestureRecognizers.Add(CreateBoxView1Tap());

            _box2 = new BoxView
            {
                BackgroundColor = Color.Lime,
                WidthRequest = 100,
                HeightRequest = 100
            };
            _box2.GestureRecognizers.Add(CreateBoxView2Tap());

            _tabStackLayout = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                Spacing = 1,
                BackgroundColor = Color.Aqua,
                Children = { _box1, _box2 }
            };

            _pageViewContainer = new PageViewContainer
            {
                BackgroundColor = Color.Pink
            };

            _grid.Children.Add(_tabStackLayout, 0, 0);
            _grid.Children.Add(_pageViewContainer, 1, 0);

            _mainContentPage = new ContentPage
            {
                Content = _grid
            };

            HandleBox1Tapped(); // start app displaying the navigation page for tab #1

			_appBack.AppBackPressed += HandleAppBack;

            MainPage = _mainContentPage;

            // The root page of your application
            //MainPage = new NavigationPage ( new SampleMenu ( ) );
        }

		private void HandleAppBack( object sender, EventArgs e ) {

			_currentNavigationPage.SendBackButtonPressed ( );
		}

        private TapGestureRecognizer CreateBoxView1Tap()
        {
            TapGestureRecognizer tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += HandleBox1Tapped;
            return tapGestureRecognizer;
        }

        private async void HandleBox1Tapped(object sender = null, EventArgs e = null)
        {
            System.Diagnostics.Debug.WriteLine("App.HandleBox1Tapped()");

            if (_navPage1 == null)
            {
                _contentPage1 = new MyPage(0);

                _currentNavigationPage = _navPage1 = new NavigationPage
                {
                    Title = "Nav 1",
                };

                await _navPage1.PushAsync(_contentPage1);
            }
            else
            {
                _currentNavigationPage = _navPage1;
            }

            _pageViewContainer.Content = _navPage1;
        }

        private TapGestureRecognizer CreateBoxView2Tap()
        {
            TapGestureRecognizer tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += HandleBox2Tapped;
            return tapGestureRecognizer;
        }

        private async void HandleBox2Tapped(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("App.HandleBox2Tapped()");

            if (_navPage2 == null)
            {
                _contentPage2 = new MyPage(0);

                _currentNavigationPage = _navPage2 = new NavigationPage
                {
                    Title = "Nav 2",
                };

                await _navPage2.PushAsync(_contentPage2);
            }
            else
            {
                _currentNavigationPage = _navPage2;
            }

            _pageViewContainer.Content = _navPage2;
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}

