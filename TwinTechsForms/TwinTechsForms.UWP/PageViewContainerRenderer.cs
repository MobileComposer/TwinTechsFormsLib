using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwinTechs.Controls;
using TwinTechs.Extensions;
using TwinTechsForms.UWP.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.UWP;
using Windows.UI.Xaml.Media;
using Windows.UI;
using Windows.Foundation;

[assembly: ExportRenderer(typeof(PageViewContainer), typeof(PageViewContainerRenderer))]
namespace TwinTechsForms.UWP.Controls
{
    //public class PageViewContainerRenderer : ViewRenderer<PageViewContainer, FrameworkElementContainer>
    //public class PageViewContainerRenderer : ViewRenderer<PageViewContainer, Windows.UI.Xaml.Controls.Frame>
    public class PageViewContainerRenderer : ViewRenderer<PageViewContainer, Windows.UI.Xaml.Controls.Page> // keep things simple for now
    {
        public PageViewContainerRenderer() { }

        public static void Init() { }

        Windows.UI.Xaml.Controls.Page windowsPage;
        //Windows.UI.Xaml.Controls.Frame windowsFrame;

        protected override void OnElementChanged(ElementChangedEventArgs<PageViewContainer> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                //var container = new FrameworkElementContainer();
                windowsPage = new Windows.UI.Xaml.Controls.Page();
                windowsPage.Background = new SolidColorBrush(Colors.Red);
                //windowsFrame = new Windows.UI.Xaml.Controls.Frame();

                //var test = windowsPage.Frame; // this is null here because no Frame is hosting this page yet.

                //SetNativeControl(windowsFrame);

                SetNativeControl(windowsPage);
                //this.Children.Add(windowsPage); // this doesn't seem to do anything
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == "Content" || e.PropertyName == "Renderer")
            {
                if (Element?.Content != null)
                    Device.BeginInvokeOnMainThread(() => ChangePage(Element.Content)); // We must do this this on the main thread when Element.Content is a NavigationPage
            }
        }

        private void ChangePage(Page pvcNavPageToDisplay)
        {
            var parentPage = Element.GetParentPage(); // this is the _mainContentPage of the App class
            pvcNavPageToDisplay.Parent = parentPage; // give this homeless page a Parent.

            pvcNavPageToDisplay.Title = "Jabooty";

            // Get the renderer of _mainContentPage, the parent page
            var parentPageRenderer = Platform.GetRenderer(parentPage);// as IVisualElementRenderer; // type: Xamarin.Forms.Platform.UWP.IVisualElementRenderer {Xamarin.Forms.Platform.UWP.PageRenderer}

            if (parentPageRenderer != null)
            {
                try
                {
                    // Try #1: Show a frame
                    //var pageFrame = new Windows.UI.Xaml.Controls.Frame() { Background = new SolidColorBrush(Colors.Orange) };
                    //Control.Content = pageFrame;
                    //Control.Frame // this is null
                    //pageFrame.Navigate(windowsPage.GetType()); // add the Page to this frame




                    // Try #2: GetOrCreate Renderer for NavPage to Disiplay, then set the Control.Content property to is ContainerElement
                    var pageRenderer = pvcNavPageToDisplay.GetOrCreateRenderer(); // this will create on for it before it's displayed
                    //pageRenderer.Element.HeightRequest = pageRenderer.Element.WidthRequest = 300; // this doesn't seem to do anything
                    var uwpPageControl = pageRenderer.ContainerElement as Xamarin.Forms.Platform.UWP.PageControl; // ContainerElement is a Xamarin.Forms.Platform.UWP.PageControl
                    Control.Name = "windowsPageControl";
                    //var parent = Control.Parent; // this is null - is that okay?
                    Control.Content = uwpPageControl;

                    var page = this.Children[0] as Windows.UI.Xaml.Controls.Page;
                    var pageContent = page.Content;

                    //Control.Frame.Navigate(uwpPageControl.GetType()); // frame is null here
                    //Control.Height = Control.Width = 300; // this works
                    this.ArrangeNativeChildren = true;
                    Control.UpdateLayout();


                    // Try #3: Use a Frame as the Control instead
                    //var pgr = pvcNavPageToDisplay.GetOrCreateRenderer();
                    //var element = pgr.Element; // this is the TwinTechs.MyNavigationPage, type is a Xamarin.Forms.VisualElement
                    //var uwpPageControl = pgr.ContainerElement as Xamarin.Forms.Platform.UWP.PageControl;
                    //Control.Content = uwpPageControl;
                    //Control.Navigate(uwpPageControl.GetType());


                    // Try #4: get renderer of pvcNavPageToDisplay.CurrentPage - this doesn't show anything, just the pick background color of the PVC view
                    //var navPage = pvcNavPageToDisplay as NavigationPage;
                    //var current = navPage.CurrentPage; // this is TwinTechs.MyPage
                    //var contentPageRenderer = current.GetOrCreateRenderer();
                    //var uwpPageControl = contentPageRenderer.ContainerElement as Xamarin.Forms.Platform.UWP.PageControl;
                    //Control.Content = uwpPageControl;
                    //Control.UpdateLayout();

                    // Try #5: Use the ParentPage as the Control.Content instead  - this doesn't show anything, just the pick background color of the PVC view
                    //var uwpPageControl = parentPageRenderer.ContainerElement as Xamarin.Forms.Platform.UWP.PageControl;
                    //Control.Name = "windowsPageControl";
                    //Control.Content = uwpPageControl;

                }
                catch (Exception ex)
                {

                }
            }
        }

        //protected override Windows.Foundation.Size ArrangeOverride(Windows.Foundation.Size finalSize)
        //{
        //    //windowsPage.Arrange(new Windows.Foundation.Rect(0, 0, finalSize.Width, finalSize.Height));
        //    var fs = finalSize.Width;
        //    var fsh = finalSize.Height;


        //    windowsPage.Arrange(new Windows.Foundation.Rect(0, 0, 500, 500));

        //    return finalSize;
        //}

        protected override Windows.Foundation.Size ArrangeOverride(Windows.Foundation.Size finalSize)
        {
            windowsPage.Arrange(new Windows.Foundation.Rect(0, 0, finalSize.Width, finalSize.Height));
            return finalSize;
        }
    }
}
