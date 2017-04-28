using System;
using Xamarin.Forms;
using TwinTechs.Ios.Controls;
using Xamarin.Forms.Platform.iOS;
using TwinTechs.Ios.Extensions;
using TwinTechs.Extensions;
using UIKit;
using TwinTechs.Controls;
using System.Diagnostics;
using Foundation;

[assembly: ExportRenderer(typeof(PageViewContainer), typeof(PageViewContainerRenderer))]
namespace TwinTechs.Ios.Controls
{
	public class PageViewContainerRenderer : ViewRenderer<PageViewContainer, ViewControllerContainer>
	{
		public PageViewContainerRenderer() { }

		// We must have this method or else the Xamarin build system will remove code it thinks is unused, this renderer
		// https://forums.xamarin.com/discussion/comment/198852/%23Comment_198852
		public new static void Init() { }

		protected override void OnElementChanged(ElementChangedEventArgs<PageViewContainer> e)
		{
			base.OnElementChanged(e);

			if (Control != null)
			{
				Control.ViewController = null; // TODO: this seems unnecessary. Control.ViewController is already null
			}

			if (e.NewElement != null)
			{
				//var viewControllerContainer = new ViewControllerContainer(Bounds);
				var viewControllerContainer = new ViewControllerContainer();
				SetNativeControl(viewControllerContainer);
			}
		}

		protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged(sender, e);

			if (e.PropertyName == "Content" || e.PropertyName == "Renderer")
			{
				// Don't bother to chage the page if the PVC Content property doesn't have a page in it
				if (Element?.Content != null)
				{
					// We must call this when Element.Content is a NavigationPage otherwise Platform.GetRenderer(parentPage) will return null in ChangePage()
					Device.BeginInvokeOnMainThread (() => ChangePage ( Element != null ? Element.Content : null));
				}
				else {
					Device.BeginInvokeOnMainThread (() => ChangePage ( null ) );
				}
			}
		}

		Page _initializedPage; // is this necessary at all?

		private void ChangePage(Page newPageToDisplay)
		{
			if (newPageToDisplay != null) {
				newPageToDisplay.Parent = Element.GetParentPage ( ); // find a Parent for this homeless page

				// 1/4: old way - don't use this
				//var pageRenderer = newPageToDisplay.GetRenderer();
				var pageRenderer = Platform.GetRenderer ( newPageToDisplay ); // TODO: this seems strange. Page hasn't been rendered yet, so this will always be null here

				UIViewController viewController = null;
				if (pageRenderer != null && pageRenderer.ViewController != null) {
					// this is hit when navigating back from the PageInPageSample page
					viewController = pageRenderer.ViewController;
				}
				else {
					viewController = newPageToDisplay.CreateViewController ( );
				}

				// this returns PageInPageSample again
				var parentPage = Element.GetParentPage ( ); // TODO: Is this the only one that is needed?

				//var renderer = parentPage.GetRenderer ();
				var parentPageRenderer = Platform.GetRenderer ( parentPage );

				// set properties of the ViewControllerContainer
				Control.ParentViewController = parentPageRenderer.ViewController;
				Control.ViewController = viewController; // there is some logic here that happens when this is set

				_initializedPage = newPageToDisplay; // not sure why they're doing this

				//NEED TO GET THE LAYOUT ADJUSTED AFTER A PAGE CHANGE
				LayoutSubviews ( );
			}
			else {

				if (Control != null) {
					Control.ViewController = null;
				}
			}
		}

		public override void LayoutSubviews()
		{
			base.LayoutSubviews();

			var page = Element != null ? Element.Content : null;
			if (page != null)
			{
				page.Layout(new Rectangle(0, 0, Bounds.Width, Bounds.Height));
			}
		}
	}
}