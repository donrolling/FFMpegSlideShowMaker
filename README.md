# FFMpeg Slideshow Makeer

Need FFMpeg installed

For now, this tool runs under several assumptions. 

1. You have FFMpeg installed
2. You install the CLI tool
3. You have a directory that has images in the order that you want them displayed, named so that they are already in order.
4. You have an mp3 file in the same directory
5. You will use PowerShell to cd into the directory
6. You run the command ffmpegss
7. It gives you two strings. Copy and paste the first, then the second into PowerShell. 
8. This will give you an mp4 with evenly distributed images that have a 2 second fade into the next image. It should be just a tiny bit longer than the audio due to rounding.

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
