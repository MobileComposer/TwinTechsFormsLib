using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace TwinTechsForms.UWP
{
    public class FrameContainer : Page //FrameworkElement
    {
        public FrameContainer()
        {
            Background = new SolidColorBrush(Colors.Transparent);
        }

        public Frame ParentFrame { get; set; }

        private Frame _frame;
        public Frame PageFrame
        {
            get { return _frame; }

            set
            {
                _frame = value;

                if (_frame != null)
                    AddFrame();
            }
        }

        private void AddFrame()
        {
            if (ParentFrame == null)
                throw new Exception("No parent frame was found");

            Debug.WriteLine("Frame.CurrentSourcePageType is " + _frame.CurrentSourcePageType);


        }


    }
}
