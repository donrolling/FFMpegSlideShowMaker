using FFMpegSlideshowMaker;
using System.Diagnostics;

var terminalRoot = Environment.CurrentDirectory;
Console.WriteLine($"Terminal Root: {terminalRoot}");
var runPowershell = false;
//terminalRoot = @"C:\Users\donro\OneDrive\Pictures\MidJourney\SpiderWebArt\VideoTest";
//var ps = PowerShell.Create();
//ps.AddCommand("write-host");
//ps.AddArgument("test");
//var xs = ps.Invoke();
//foreach (var x in xs) {
//    Console.WriteLine("{0}", x);
//}

var slideShowCommand = CommandBuilder.BuildSlideShowCommand(terminalRoot);
var musicCommand = CommandBuilder.BuildMusicCommand();
Console.WriteLine("FFMpeg Command Builder");

if (runPowershell) {
	if (!string.IsNullOrWhiteSpace(slideShowCommand)) {
		Console.WriteLine("Running Command...");
		var startInfo = new ProcessStartInfo();
		//startInfo.WorkingDirectory = terminalRoot;
		//startInfo.CurrentDirectory = terminalRoot;
		startInfo.FileName = "powershell.exe";
		var proc0 = Process.Start(startInfo);
		var proc1 = Process.Start(slideShowCommand);
		var proc2 = Process.Start(musicCommand);
		Console.WriteLine("Complete!");
	} else {
		Console.WriteLine("Command was empty. Probably no images or audio files found.");
	}
} else {
	Console.WriteLine("-----------------------------------------");
	Console.WriteLine(slideShowCommand);
	Console.WriteLine("-----------------------------------------");
	Console.WriteLine(musicCommand);
}