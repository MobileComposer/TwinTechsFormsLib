using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using TwinTechs.Example;
using TwinTechs.Droid.Controls;
using TwinTechs;
using Android.Util;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Collections.Generic;
using TwinTechs.Controls;
using Xamarin.Forms.Platform.Android;

namespace TwinTechsFormsExample.Droid
{
	[Activity (Label = "TwinTechsFormsExample.Droid", Icon = "@drawable/icon", MainLauncher = true, Theme = "@style/MyTheme", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : FormsAppCompatActivity, IBack
	{
		DummyIncludes _dummyIncludes;

		public event EventHandler AppBackPressed;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			var metrics = Resources.DisplayMetrics;
			AppHelper.ScreenSize = new Xamarin.Forms.Size (ConvertPixelsToDp (metrics.WidthPixels), ConvertPixelsToDp (metrics.HeightPixels));

			App.AppBack = this;

			global::Xamarin.Forms.Forms.Init (this, bundle);

			LoadApplication (new App ());
		}

		private int ConvertPixelsToDp (float pixelValue)
		{
			var dp = (int)((pixelValue) / Resources.DisplayMetrics.Density);
			return dp;
		}

		public override void OnBackPressed ( ) {

			System.Diagnostics.Debug.WriteLine ( "MainActivity.OnBackPressed()" );

			AppBackPressed ( null, null );
		}

		public void AppExit ( ) {

			base.OnBackPressed ( );
		}
	}
}

