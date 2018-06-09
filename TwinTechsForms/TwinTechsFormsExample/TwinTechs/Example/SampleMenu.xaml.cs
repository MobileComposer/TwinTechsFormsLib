using System;
using System.Collections.Generic;

using Xamarin.Forms;
using TwinTechs.Example.PageInPage;

namespace TwinTechs.Example
{
	public partial class SampleMenu : ContentPage
	{
		public SampleMenu ()
		{
			InitializeComponent ();
		}

		#region Page in page

		void OnPageInPageSimple (object sender, object args)
		{
			var pageInPage = new PageInPageSample ();
			Navigation.PushAsync (pageInPage);
			
		}

		void OnEmbeddedNavigationPage (object sender, object args)
		{
			var navigationPageInPage = new NavigationPageInPage ();
			Navigation.PushAsync (navigationPageInPage);
			
		}

		#endregion
	}
}

