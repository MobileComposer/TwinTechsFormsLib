using System;
using UIKit;
using CoreGraphics;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using System.Threading.Tasks;
using System.Diagnostics;
using CoreImage;

namespace TwinTechs.Ios.Controls
{
	public class ViewControllerContainer : UIView
	{
		//public ViewControllerContainer (CGRect frame) : base (frame) // TODO: this seems unnecessary. Frame is always 0,0 0,0 here
		public ViewControllerContainer()
		{
			BackgroundColor = Color.Transparent.ToUIColor();
		}

		public UIViewController ParentViewController { get; set; }

        UIViewController _viewController;
		// this prop handles the adding and removing of the ViewController to the Parent, and the View as a subview of this UIView
		public UIViewController ViewController
		{
			get { return _viewController; }
			set
			{
				if (_viewController != null)
				{
					RemoveCurrentViewController();
				}
				_viewController = value;

				if (_viewController != null)
				{
					AddViewController();
				}
			}
		}

		private void AddViewController()
		{
			if (ParentViewController == null) {
				throw new Exception ( "No Parent View controller was found" );
			}
			
			CGRect rect = new CGRect ( 0, 0, Frame.Width, Frame.Height );

			this.Bounds = rect;
			this.Frame = rect;

			//ParentViewController.View.Frame = rect;
			//ParentViewController.View.Bounds = rect;
			//ParentViewController.View.BackgroundColor = new UIColor ( 1.0f, 0.0f, 0.0f, 1.0f );

			_viewController.View.Frame = rect;
			_viewController.View.Bounds = rect;

			ParentViewController.AddChildViewController(_viewController); // Parent the new ViewController.  This is the new VC of the new Page to be displayed.

			AddSubview(_viewController.View); // add the new view as a nested view of this UIView

			_viewController.DidMoveToParentViewController(ParentViewController);
		}

        private void RemoveCurrentViewController()
        {
            if (_viewController != null)
            {
                _viewController.WillMoveToParentViewController(null); // Called just before the view controller is added or removed from a container view controller.
                _viewController.View.RemoveFromSuperview();
                _viewController.RemoveFromParentViewController();
            }
        }
	}
}