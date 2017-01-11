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
    public class PageViewContainerRenderer : ViewRenderer<PageViewContainer, FrameContainer>
    {
        public PageViewContainerRenderer() { }

        public static void Init() { }

        protected override void OnElementChanged(ElementChangedEventArgs<PageViewContainer> e)
        {
            base.OnElementChanged(e);

            var container = new FrameContainer();
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

            //newPageToDisplay.ConvertPageToUIElement(this);

            //var temp = Window.Current.Content as Frame;
            var temp = Window.Current.Content as Windows.UI.Xaml.Controls.Frame;
        }
    }
}
