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
using Xamarin.Forms.Platform.UAP;
using Windows.UI.Xaml;

[assembly: ExportRenderer(typeof(PageViewContainer), typeof(PageViewContainerRenderer))]
namespace TwinTechsForms.UWP.Controls
{
    public class PageViewContainerRenderer : ViewRenderer<PageViewContainer, FrameworkElementContainer>
    {
        public PageViewContainerRenderer() { }

        public static void Init() { }

        protected override void OnElementChanged(ElementChangedEventArgs<PageViewContainer> e)
        {
            base.OnElementChanged(e);

            var container = new FrameworkElementContainer();
            SetNativeControl(container);
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

        private void ChangePage(Page newPageToDisplay)
        {
            newPageToDisplay.Parent = Element.GetParentPage(); // find a Parent page for this homeless page

            var pageRenderer = Platform.GetRenderer(newPageToDisplay);



            var parentPage = Element.GetParentPage();
            var parentPageRenderer = Platform.GetRenderer(parentPage) as IVisualElementRenderer; // type: Xamarin.Forms.Platform.UWP.IVisualElementRenderer {Xamarin.Forms.Platform.UWP.PageRenderer}

            var prContElement = parentPageRenderer.ContainerElement; // type: Windows.UI.Xaml.FrameworkElement {Xamarin.Forms.Platform.UWP.PageRenderer}
            var prElement = parentPageRenderer.Element; // type: Xamarin.Forms.VisualElement { TwinTechs.Example.PageInPage.PageInPageSample}



            Control.ParentFrameworkElement = parentPageRenderer.ContainerElement; // .ContainerElement is a Windows.UI.Xaml.FrameworkElement
            Control.PageFrame = new Windows.UI.Xaml.Controls.Frame { Content = new Windows.UI.Xaml.Controls.TextBlock { Text = "Test test test test test" } };


            // 1/12: idea
            //Control.FrameworkElement = Frame
            //Control.PageFrame = new Windows.UI.Xaml.Controls.Frame();
            //Control.PageFrame.Content = new Windows.UI.Xaml.Controls.TextBox { Text = "test!" };


            

        }
    }
}
