using Foundation;
using UIKit;
using TwinTechs;
using TwinTechs.Example;
using TwinTechs.Ios.Controls;
using TwinTechs.Controls;
using System;

namespace TwinTechsFormsExample.iOS
{
	[Register ("AppDelegate")]
	public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate, IBack
	{
		public event EventHandler AppBackPressed;

		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			App.AppBack = this;

			global::Xamarin.Forms.Forms.Init ();

			AppHelper.ScreenSize = new Xamarin.Forms.Size (UIScreen.MainScreen.Bounds.Size.Width, UIScreen.MainScreen.Bounds.Size.Height);

			LoadApplication (new App ());

			return base.FinishedLaunching (app, options);
		}

		public void AppExit ( ) {
			
		}
	}
}