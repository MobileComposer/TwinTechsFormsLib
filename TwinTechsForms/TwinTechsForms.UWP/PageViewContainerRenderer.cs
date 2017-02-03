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
                windowsPage.Background = new SolidColorBrush(Colors.Azure);

                SetNativeControl(windowsPage);
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == "Content" || e.PropertyName == "Renderer")
            {
                if (Element?.Content != null)
                    ChangePage(Element.Content);
            }
        }

        private void ChangePage(Page pvcPageToDisplay)
        {
            var parentPage = Element.GetParentPage(); // this is the _mainContentPage of the App class
            pvcPageToDisplay.Parent = parentPage; // find a Parent page for this homeless page.  

            var pageRenderer = Platform.GetRenderer(pvcPageToDisplay); // this will be null the first time, because it hasn't been rendered yet

            // Get the renderer of _mainContentPage, the parent page
            var parentPageRenderer = Platform.GetRenderer(parentPage);// as IVisualElementRenderer; // type: Xamarin.Forms.Platform.UWP.IVisualElementRenderer {Xamarin.Forms.Platform.UWP.PageRenderer}


            if (parentPageRenderer != null)
            {
                // goal: get the frame of _mainContentPage Windows.UI.Xaml.Controls.Page

                //var temp = Platform.GetRenderer(parentPage) as Xamarin.Forms.Platform.UWP.PageRenderer;


                //Control.Frame // this is null, how does it get set?


                //var canvas = parentPageRenderer.ContainerElement as Windows.UI.Xaml.Controls.Canvas; //no
                var temp1 = parentPageRenderer.ContainerElement;
                var canvas1 = temp1.Parent as Windows.UI.Xaml.Controls.Canvas;
                var frame1 = new Windows.UI.Xaml.Controls.Frame();

                var temp2 = this.ContainerElement;
                var canvas2 = temp1.Parent as Windows.UI.Xaml.Controls.Canvas;
                var frame2 = new Windows.UI.Xaml.Controls.Frame();


                try
                {
                    canvas1.Children.Add(frame1);
                    frame1.Navigate(windowsPage.GetType());

                    //canvas2.Children.Add(frame2);
                    //frame2.Navigate(windowsPage.GetType());
                }
                catch (Exception ex)
                {

                }

                // this shows a white screen over everything
                //var container = new Windows.UI.Xaml.Controls.Canvas { Style = (Windows.UI.Xaml.Style)Windows.UI.Xaml.Application.Current.Resources["RootContainerStyle"] };
                //var frame = new Windows.UI.Xaml.Controls.Frame();
                //container.Children.Add(frame);

                //Windows.UI.Xaml.Window.Current.Content = frame;


                //var temp2 = temp.Control as Windows.UI.Xaml.Controls.Frame; // this is null
                //var temp2 = temp.ContainerElement as Windows.UI.Xaml.Controls.Frame;

                //var windowsPage = temp.Control as Windows.UI.Xaml.Controls.Page; // Control is null here
                //windowsPage.Frame.Navigate(_container.GetType());

                //Control.Frame.Navigate(_container.GetType());

                //var test1 = parentPageRenderer.ContainerElement; // type: Windows.UI.Xaml.FrameworkElement {Xamarin.Forms.Platform.UWP.PageRenderer}
                //var test2 = parentPageRenderer.Element; // type: Xamarin.Forms.VisualElement { TwinTechs.Example.PageInPage.PageInPageSample}



                //Control.ParentFrameworkElement = parentPageRenderer.ContainerElement; // .ContainerElement is a Windows.UI.Xaml.FrameworkElement
                //Control.PageFrame = new Windows.UI.Xaml.Controls.Frame { Content = new Windows.UI.Xaml.Controls.TextBlock { Text = "Test test test test test" } };

                //var controlFrame = Control.Frame; // is this null?

                // 1/12: idea
                //Control.FrameworkElement = Frame
                //Control.PageFrame = new Windows.UI.Xaml.Controls.Frame();
                //Control.PageFrame.Content = new Windows.UI.Xaml.Controls.TextBox { Text = "test!" };

            }

        }


        protected override Windows.Foundation.Size ArrangeOverride(Windows.Foundation.Size finalSize)
        {
            windowsPage.Arrange(new Windows.Foundation.Rect(0, 0, finalSize.Width, finalSize.Height));
            return finalSize;
        }
    }
}
