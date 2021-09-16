var configuration = Argument("configuration", "Debug");
var msbuildToolPath = Argument("msbuildToolPath", @"C:\Program Files (x86)\Microsoft Visual Studio\2019\BuildTools\MSBuild\Current\Bin\msbuild.exe");

Task("Restore")
	.Does(() => 
	{
		NuGetRestore("Linker-Cake.sln");
	}
	);


Task("Build")
	.IsDependentOn("Restore")
	.Does(() => 
	{
		//DotNetCoreBuild("Linker-Cake.sln",  new DotNetCoreBuildSettings {Configuration = configuration});

		MSBuild("Linker-Cake.sln",
            new MSBuildSettings {
              ToolPath = msbuildToolPath,
              Verbosity = Verbosity.Quiet ,
              ToolVersion = MSBuildToolVersion.VS2019,
              Configuration = configuration,
              NodeReuse = false
            }
            .AddFileLogger(new MSBuildFileLogger
                    {
                        Verbosity = Verbosity.Verbose,

                    })
            //.SetNoConsoleLogger(true)
            .SetMaxCpuCount(0)
            .WithTarget("Clean")
            .WithTarget("Build"));

	});

RunTarget("Build");