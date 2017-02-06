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
using TwinTechs.Gestures;
using System.Threading.Tasks;
using System.Collections.Generic;
using TwinTechs.Controls;
using TwinTechs.Droid.Controls;
using Xamarin.Forms.Platform.Android;

namespace TwinTechsFormsExample.Droid
{
	[Activity (Label = "TwinTechsFormsExample.Droid", Icon = "@drawable/icon", MainLauncher = true, Theme = "@style/MyTheme", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : FormsAppCompatActivity, IBack
	{
		DummyIncludes _dummyIncludes;
		GestureTouchDispatcher _gestureTouchDispatcher;

		public event EventHandler AppBackPressed;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			_gestureTouchDispatcher = new GestureTouchDispatcher (this);
			AppHelper.FastCellCache = FastCellCache.Instance;

			var metrics = Resources.DisplayMetrics;
			AppHelper.ScreenSize = new Xamarin.Forms.Size (ConvertPixelsToDp (metrics.WidthPixels), ConvertPixelsToDp (metrics.HeightPixels));
			GestureRecognizerExtensions.Factory = new NativeGestureRecognizerFactory ();

			App.AppBack = this;

			global::Xamarin.Forms.Forms.Init (this, bundle);

			ViewEffectExtensions.ViewExtensionProvider = new ViewMaskExtensionProvider ();
			LoadApplication (new App ());
		}

		private int ConvertPixelsToDp (float pixelValue)
		{
			var dp = (int)((pixelValue) / Resources.DisplayMetrics.Density);
			return dp;
		}

		public override bool DispatchTouchEvent (MotionEvent ev)
		{
			var didConsumeTouch = _gestureTouchDispatcher.DispatchTouchEvent (ev);
			//TODO - consider not passing this along?
			var isHandledByNormalRouting = base.DispatchTouchEvent (ev);
			return isHandledByNormalRouting;
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

