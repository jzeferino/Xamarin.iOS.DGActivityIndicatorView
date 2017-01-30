using ObjCRuntime;

[assembly: LinkWith("libDGActivityIndicatorView.a", LinkTarget.Simulator | LinkTarget.Simulator64 | LinkTarget.ArmV7 | LinkTarget.Arm64,
    SmartLink = true,
    ForceLoad = false,
    Frameworks = "QuartzCore",
    LinkerFlags = "-ObjC")]
