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
    public class PageViewContainerRenderer : ViewRenderer<PageViewContainer, Windows.UI.Xaml.Controls.Page> // keep things simple for now
    {
        public PageViewContainerRenderer() { }

        public static void Init() { }


        Windows.UI.Xaml.Controls.Page windowsPage;

        protected override void OnElementChanged(ElementChangedEventArgs<PageViewContainer> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                //var container = new FrameworkElementContainer();
                windowsPage = new Windows.UI.Xaml.Controls.Page();

                var test = windowsPage.Frame; // this is null here because no Frame is hosting this page yet.
                windowsPage.Background = new SolidColorBrush(Colors.Red);

                SetNativeControl(windowsPage);
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == "Content" || e.PropertyName == "Renderer")
            {
                if (Element?.Content != null)
                    //ChangePage(Element.Content);
                    // We must do this this on the main thread when Element.Content is a NavigationPage
                    Device.BeginInvokeOnMainThread(() => ChangePage(Element.Content));
            }
        }

        private void ChangePage(Page pvcPageToDisplay)
        {
            var parentPage = Element.GetParentPage(); // this is the _mainContentPage of the App class
            pvcPageToDisplay.Parent = parentPage; // find a Parent page for this homeless page.  

            var pgr = pvcPageToDisplay.GetOrCreateRenderer();


            var pageRenderer = Platform.GetRenderer(pvcPageToDisplay); // this will be null the first time, because it hasn't been rendered yet

            // Get the renderer of _mainContentPage, the parent page
            var parentPageRenderer = Platform.GetRenderer(parentPage);// as IVisualElementRenderer; // type: Xamarin.Forms.Platform.UWP.IVisualElementRenderer {Xamarin.Forms.Platform.UWP.PageRenderer}

            if (parentPageRenderer != null)
            {
                // goal: get the frame of _mainContentPage Windows.UI.Xaml.Controls.Page

                try
                {
                    var pageFrame = new Windows.UI.Xaml.Controls.Frame() { Background = new SolidColorBrush(Colors.Orange) };
                    Control.Content = pageFrame;
                    //Control.Frame // this is null
                    var height = windowsPage.Height;
                    var width = windowsPage.Width;


                    pageFrame.Navigate(windowsPage.GetType()); // add the Page to this frame
                }
                catch (Exception ex)
                {

                }

                //Windows.UI.Xaml.Window.Current.Content = frame;
            }

        }


        protected override Windows.Foundation.Size ArrangeOverride(Windows.Foundation.Size finalSize)
        {
            windowsPage.Arrange(new Windows.Foundation.Rect(0, 0, finalSize.Width, finalSize.Height));
            return finalSize;
        }
    }
}
