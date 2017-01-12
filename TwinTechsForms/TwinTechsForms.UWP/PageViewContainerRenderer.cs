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
            var parentPageRenderer = Platform.GetRenderer(parentPage);

            Control.ParentFrameworkElement = parentPageRenderer.ContainerElement;

            // 1/12: idea
            //Control.FrameworkElement = Frame
            Control.PageFrame = new Windows.UI.Xaml.Controls.Frame();

            //var page = parentContainer.Content as Windows.UI.Xaml.Controls.Page;

            //newPageToDisplay.ConvertPageToUIElement(this); // looks like this isn't available on UWP

            //var temp = Window.Current.Content as Frame;

        }
    }
}
