using System;
using System.Collections.Generic;

using Xamarin.Forms;
using TwinTechs.Controls;

namespace TwinTechs.Example.PageInPage
{
	public partial class NavigationPageInPage : ContentPage
	{
		int _index;
		NavigationPage _navigationPage;

		Page _rootPage;

		public NavigationPageInPage ()
		{
			InitializeComponent ();
			_rootPage = CreatePage (Color.Red);
			_navigationPage = new NavigationPage (_rootPage);

			// this is the difference between PageInPageSample and this page.
			// Here the Content prop is being set in the ctor
			PageContainer.Content = _navigationPage;
		}

		ContentPage CreatePage (Color backgroundColor)
		{
			_index++;
			var button = new Button () {
				Text = "next Page",
			};
			button.Clicked += (sender, e) => {
				var page = CreatePage (Color.Green);
				_navigationPage.PushAsync (page);
			};

			var contentPage = new ContentPage () {
				Content = new StackLayout () {
					BackgroundColor = backgroundColor,
					Children = {
						new Label () {
							Text = "page " + _index,
						},

					}
				}
			};
			return contentPage;
		}



	}
}

