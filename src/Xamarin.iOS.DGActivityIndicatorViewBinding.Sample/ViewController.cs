using System;
using System.Linq;
using CoreGraphics;
using UIKit;

namespace Xamarin.iOS.DGActivityIndicatorViewBinding.Sample
{
    public partial class ViewController : UIViewController
    {
        protected ViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            View.BackgroundColor = UIColor.FromRGB(237, 85, 101);

            var cols = 4;
            var rows = 8;
            var cellWidth = (int)(View.Bounds.Size.Width / cols);
            var cellHeight = (int)(View.Bounds.Size.Height / rows);

            var enumValues = Enum.GetValues(typeof(DGActivityIndicatorAnimationType)).Cast<DGActivityIndicatorAnimationType>();

            for (int i = 0; i < enumValues.Count(); i++)
            {
                int x = i % cols * cellWidth;
                int y = i / cols * cellHeight;

                var activityIndicatorView = new DGActivityIndicatorView(enumValues.ElementAt(i), UIColor.White);

                activityIndicatorView.Frame = new CGRect(x, y, cellWidth, cellHeight);
                activityIndicatorView.Bounds = activityIndicatorView.Frame;

                var label = new UILabel(new CGRect(x, y + (cellHeight - activityIndicatorView.Frame.Size.Height), cellWidth, cellHeight));
                label.Text = i.ToString();
                label.TextColor = UIColor.White;
                label.Font = label.Font.WithSize(12f);

                View.AddSubview(activityIndicatorView);
                View.AddSubview(label);

                activityIndicatorView.StartAnimating();
            }

        }

    }
}
