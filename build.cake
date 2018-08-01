#addin nuget:?package=Cake.SemVer&loaddependencies=true

// Enviroment
var isRunningBitrise = Bitrise.IsRunningOnBitrise;
var isRunningOnWindows = IsRunningOnWindows();

// Arguments.
var target = Argument("target", "Default");
var configuration = "Release";

// Define directories.
var solutionFile = new FilePath("Xamarin.iOS.DGActivityIndicatorViewBinding.sln");
var libProject = new FilePath("src/Xamarin.iOS.DGActivityIndicatorViewBinding/Xamarin.iOS.DGActivityIndicatorViewBinding.csproj");
var iOSSample  = new FilePath("src/Xamarin.iOS.DGActivityIndicatorViewBinding.Sample/Xamarin.iOS.DGActivityIndicatorViewBinding.Sample.csproj");
var artifactsDirectory = new DirectoryPath("artifacts");
var iOSOutputDirectory = "bin/iPhoneSimulator";

// Versioning. Used for all the packages and assemblies for now.
var version = CreateSemVer(1, 0, 1);

Setup((context) =>
{
	Information("Bitrise: {0}", isRunningBitrise);
	Information ("Running on Windows: {0}", isRunningOnWindows);
	Information("Configuration: {0}", configuration);
});

Task("Clean")
	.Does(() =>
	{	
		CleanDirectory(artifactsDirectory);

		MSBuild(solutionFile, settings => settings
				.SetConfiguration(configuration)
				.WithTarget("Clean")
				.SetVerbosity(Verbosity.Minimal));
	});

Task("Restore")
	.Does(() => 
	{
		NuGetRestore(solutionFile);
	});

Task("Build")
	.IsDependentOn("Clean")
	.IsDependentOn("Restore")
	.Does(() =>  
	{	
		MSBuild(libProject, settings => settings
					.SetConfiguration(configuration)
					.WithTarget("Build")
					.SetVerbosity(Verbosity.Minimal));

		MSBuild(iOSSample, settings => settings
					.SetConfiguration(configuration)
					.WithTarget("Build")
					.WithProperty("Platform", "iPhoneSimulator")
					.WithProperty("OutputPath", iOSOutputDirectory)
					.WithProperty("TreatWarningsAsErrors", "false")	
					// For some strange reason, this compiles fine in iPhoneSimulator without AllowUnsafeBlocks from the IDE but here it just won't compile witout it.
					.WithProperty("AllowUnsafeBlocks", "true")	
					.SetVerbosity(Verbosity.Minimal));
	});

Task ("NuGet")
	.IsDependentOn ("Build")
	.WithCriteria(isRunningBitrise)
	.Does (() =>
	{
		Information("Nuget version: {0}", version);
		
  		var nugetVersion = Bitrise.Environment.Repository.GitBranch == "master" ? version.ToString() : version.Change(prerelease: "pre" + Bitrise.Environment.Build.BuildNumber).ToString();
		
		NuGetPack ("./nuspec/Xamarin.iOS.DGActivityIndicatorView.nuspec", 
			new NuGetPackSettings 
				{ 
					Version = nugetVersion,
					Verbosity = NuGetVerbosity.Normal,
					OutputDirectory = artifactsDirectory,
					BasePath = "./",
					ArgumentCustomization = args => args.Append("-NoDefaultExcludes")		
				});	
	});

Task("Default")
	.IsDependentOn("NuGet")
	.Does(() => {});

RunTarget(target);