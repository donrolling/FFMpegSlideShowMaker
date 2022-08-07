# FFMpeg Slideshow Makeer

Need FFMpeg installed

# CLI Tool

## Project Setup

These three lines were needed in the csproj:
```
<PackAsTool>true</PackAsTool>
<ToolCommandName>ffmpegss</ToolCommandName>
<PackageOutputPath>./nupkg</PackageOutputPath>
```
## Building and deploying

Terminal command must be run in the terminal from the location of the csproj.
I think install works differently if using a non-local nupkg. More on that when I figure it out.

- build solution
- create a nupkg in the PackageOutputPath location 
	> dotnet pack
- install the tool globally
	> dotnet tool install -g --add-source ./nupkg FFMpegSlideshowMaker

**Update the tool**
> dotnet tool update -g --add-source ./nupkg FFMpegSlideshowMaker

**Uninstall the tool**
> dotnet tool uninstall FFMpegSlideshowMaker -g

### LINKS

[dotnet tool install documentation](https://docs.microsoft.com/en-us/dotnet/core/tools/dotnet-tool-install)

[Pluralizer Github link](https://github.com/sarathkcm/Pluralize.NET)
