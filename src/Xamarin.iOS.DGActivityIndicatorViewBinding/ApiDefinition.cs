using System;
using Foundation;
using ObjCRuntime;
using UIKit;

namespace Xamarin.iOS.DGActivityIndicatorViewBinding
{
    // @interface DGActivityIndicatorView : UIView
    [BaseType(typeof(UIView))]
    interface DGActivityIndicatorView
    {
        // -(id)initWithType:(DGActivityIndicatorAnimationType)type;
        [Export("initWithType:")]
        IntPtr Constructor(DGActivityIndicatorAnimationType type);

        // -(id)initWithType:(DGActivityIndicatorAnimationType)type tintColor:(UIColor *)tintColor;
        [Export("initWithType:tintColor:")]
        IntPtr Constructor(DGActivityIndicatorAnimationType type, UIColor tintColor);

        // -(id)initWithType:(DGActivityIndicatorAnimationType)type tintColor:(UIColor *)tintColor size:(CGFloat)size;
        [Export("initWithType:tintColor:size:")]
        IntPtr Constructor(DGActivityIndicatorAnimationType type, UIColor tintColor, nfloat size);

        // @property (nonatomic) DGActivityIndicatorAnimationType type;
        [Export("type", ArgumentSemantic.Assign)]
        DGActivityIndicatorAnimationType Type { get; set; }

        // @property (nonatomic, strong) UIColor * tintColor;
        [Export("tintColor", ArgumentSemantic.Strong)]
        UIColor TintColor { get; set; }

        // @property (nonatomic) CGFloat size;
        [Export("size")]
        nfloat Size { get; set; }

        // @property (readonly, nonatomic) BOOL animating;
        [Export("animating")]
        bool Animating { get; }

        // -(void)startAnimating;
        [Export("startAnimating")]
        void StartAnimating();

        // -(void)stopAnimating;
        [Export("stopAnimating")]
        void StopAnimating();
    }
}
