using System;
using Xamarin.Forms;
namespace TwinTechs {

	public class MyNavigationPage : NavigationPage {

		public MyNavigationPage ( )
        {
            BackgroundColor = Color.Olive;
		}

		protected override bool OnBackButtonPressed ( ) {

			System.Diagnostics.Debug.WriteLine ( "MyNavigationPage.OnBackButtonPressed()" );

			if( this.Navigation.NavigationStack.Count == 1 ) {

				App.AppBack.AppExit ( );
				return false;
			}

			return base.OnBackButtonPressed ( );
		}
	}
}