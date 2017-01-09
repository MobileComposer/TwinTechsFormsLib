﻿using System;
using Xamarin.Forms;
using TwinTechs.Ios.Controls;
using Xamarin.Forms.Platform.iOS;
using TwinTechs.Ios.Extensions;
using TwinTechs.Extensions;
using UIKit;
using TwinTechs.Controls;
using System.Diagnostics;

[assembly: ExportRenderer(typeof(PageViewContainer), typeof(PageViewContainerRenderer))]
namespace TwinTechs.Ios.Controls
{
	public class PageViewContainerRenderer : ViewRenderer<PageViewContainer, ViewControllerContainer>
	{
		public PageViewContainerRenderer()
		{
		}

		// We must have this method or else the Xamarin build system will remove code that it thinks is unused
		// https://forums.xamarin.com/discussion/comment/198852/%23Comment_198852
		public new static void Init() { }

		protected override void OnElementChanged(ElementChangedEventArgs<PageViewContainer> e)
		{
			base.OnElementChanged(e);

			if (Control != null)
			{
				Control.ViewController = null;
			}

			if (e.NewElement != null)
			{
				var viewControllerContainer = new ViewControllerContainer(Bounds);
				SetNativeControl(viewControllerContainer);
			}
		}

		Page _initializedPage;

		void ChangePage(Page page)
		{
			if (page != null)
			{
				page.Parent = Element.GetParentPage();

				// 1/4: old way - don't use this
				//var pageRenderer = page.GetRenderer ();
				var pageRenderer = Platform.GetRenderer(page);

				UIViewController viewController = null;
				if (pageRenderer != null && pageRenderer.ViewController != null)
				{
					viewController = pageRenderer.ViewController;
				}
				else
				{
					viewController = page.CreateViewController();
				}
				var parentPage = Element.GetParentPage();

				//var renderer = parentPage.GetRenderer ();
				var renderer = Platform.GetRenderer(parentPage);

				Control.ParentViewController = renderer.ViewController;
				Control.ViewController = viewController;
				_initializedPage = page;
			}
			else
			{
				if (Control != null)
				{
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

		protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged(sender, e);
			if (e.PropertyName == "Content" || e.PropertyName == "Renderer")
			{
				Device.BeginInvokeOnMainThread(() => ChangePage(Element != null ? Element.Content : null));
			}
		}

	}
}

