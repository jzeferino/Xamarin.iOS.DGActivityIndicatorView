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

            // Thats a 🔨 way of getting safearea insets.
            var window = UIApplication.SharedApplication.Windows?.FirstOrDefault();
            var topPadding = window?.SafeAreaInsets.Top ?? 0;
            var bottomPadding = window?.SafeAreaInsets.Bottom ?? 0;

            var cols = 4;
            var rows = 9;
            var cellWidth = View.Bounds.Size.Width / cols;
            var cellHeight = (View.Bounds.Size.Height - (topPadding + bottomPadding)) / rows;

            var enumValues = Enum.GetValues(typeof(DGActivityIndicatorAnimationType)).Cast<DGActivityIndicatorAnimationType>();

            for (int i = 0; i < enumValues.Count(); i++)
            {
                var x = i % cols * cellWidth;
                var y = (i / cols * cellHeight) + topPadding;

                var activityIndicatorView = new DGActivityIndicatorView(enumValues.ElementAt(i), UIColor.White)
                {
                    TranslatesAutoresizingMaskIntoConstraints = false
                };
                var label = new UILabel
                {
                    TranslatesAutoresizingMaskIntoConstraints = false,
                    Text = enumValues.ElementAt(i).ToString(),
                    TextColor = UIColor.White,
                    TextAlignment = UITextAlignment.Center
                };
                label.Font = label.Font.WithSize(9f);

                View.AddSubview(activityIndicatorView);
                activityIndicatorView.AddSubview(label);

                activityIndicatorView.HeightAnchor.ConstraintEqualTo(cellHeight).Active = true;
                activityIndicatorView.WidthAnchor.ConstraintEqualTo(cellWidth).Active = true;
                activityIndicatorView.LeadingAnchor.ConstraintEqualTo(View.LeadingAnchor, x).Active = true;
                activityIndicatorView.TopAnchor.ConstraintEqualTo(View.TopAnchor, y).Active = true;

                label.LeadingAnchor.ConstraintEqualTo(activityIndicatorView.LeadingAnchor).Active = true;
                label.TrailingAnchor.ConstraintEqualTo(activityIndicatorView.TrailingAnchor).Active = true;
                label.TopAnchor.ConstraintEqualTo(activityIndicatorView.TopAnchor).Active = true;

                activityIndicatorView.StartAnimating();
            }
        }
    }
}
