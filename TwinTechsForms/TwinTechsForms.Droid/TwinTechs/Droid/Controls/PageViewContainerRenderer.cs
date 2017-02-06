using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using TwinTechs.Controls;
using TwinTechs.Droid.Controls;
using Android.Views;
using TwinTechs.Droid.Extensions;
using TwinTechs.Extensions;
using Android.App;
using Android;
using System.Reflection;

[assembly: ExportRenderer ( typeof ( PageViewContainer ), typeof ( PageViewContainerRenderer ) )]
namespace TwinTechs.Droid.Controls {
	public class PageViewContainerRenderer : ViewRenderer<PageViewContainer, Android.Views.View> {

		private Page _currentPage;
		private bool _contentNeedsLayout;

		// We must have this method or else the Xamarin build system will remove code that it thinks is unused
		// https://forums.xamarin.com/discussion/comment/198852/%23Comment_198852
		public new static void Init ( ) { }

		protected override void OnElementChanged ( ElementChangedEventArgs<PageViewContainer> e ) {

			System.Diagnostics.Debug.WriteLine ( $"PageViewContainerRenderer.OnElementChanged() - Droid" );

			base.OnElementChanged ( e );
			var pageViewContainer = e.NewElement as PageViewContainer;
			if (e.NewElement != null) {
				ChangePage ( e.NewElement.Content );
			}
			else {
				ChangePage ( null );
			}
		}

		protected override void OnElementPropertyChanged ( object sender, System.ComponentModel.PropertyChangedEventArgs e ) {

			System.Diagnostics.Debug.WriteLine ( $"PageViewContainerRenderer.OnElementPropertyChanged() - Droid - e.PropertyName = { e.PropertyName }" );

			base.OnElementPropertyChanged ( sender, e );

			if (e.PropertyName == "Content") {
				ChangePage ( Element.Content );
			}
		}

		protected override void OnLayout ( bool changed, int l, int t, int r, int b ) {

			System.Diagnostics.Debug.WriteLine ( $"PageViewContainerRenderer.OnLayout() - Droid = { l } { t } { r } { b }" );

			base.OnLayout ( changed, l, t, r, b );

			if (( changed || _contentNeedsLayout ) && this.Control != null) {

				if (_currentPage != null) {

					System.Diagnostics.Debug.WriteLine ( $"PageViewContainerRenderer.OnLayout() - Droid - Element.Width = { Element.Width } Element.Height = { Element.Height }" );

					_currentPage.Layout ( new Rectangle ( 0, 0, Element.Width, Element.Height ) );
				}

				var msw = MeasureSpec.MakeMeasureSpec ( r - l, MeasureSpecMode.Exactly );
				var msh = MeasureSpec.MakeMeasureSpec ( b - t, MeasureSpecMode.Exactly );

				System.Diagnostics.Debug.WriteLine ( $"PageViewContainerRenderer.OnLayout() - Droid - msw = { msw } msh = { msh }" );

				this.Control.Measure ( msw, msh );
				this.Control.Layout ( 0, 0, r, b );
				_contentNeedsLayout = false;

				OnLayout ( false, l, t, r, b );
			}
		}

		private void ChangePage ( Page page ) {

			System.Diagnostics.Debug.WriteLine ( $"PageViewContainerRenderer.ChangePage() - Droid" );

			//TODO handle current page
			if (page != null) {
			
				var parentPage = Element.GetParentPage ( );
				page.Parent = parentPage;

				var existingRenderer = page.GetRenderer ( );

				if (existingRenderer == null) {

					var renderer = Platform.CreateRenderer ( page );
					page.SetRenderer ( renderer );
					existingRenderer = page.GetRenderer ( );
				}

				_contentNeedsLayout = true;
				SetNativeControl ( existingRenderer.ViewGroup );
				Invalidate ( );
				//TODO update the page
				_currentPage = page;
			}
			else {

				//TODO - update the page
				_currentPage = null;
			}

			if (_currentPage == null) {

				//have to set somethign for android not to get pissy
				var view = new Android.Views.View ( this.Context );
				view.SetBackgroundColor ( Element.BackgroundColor.ToAndroid ( ) );
				SetNativeControl ( view );
			}
		}

	}
}