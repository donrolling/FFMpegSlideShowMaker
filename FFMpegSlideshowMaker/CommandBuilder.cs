using System.Text;

namespace FFMpegSlideshowMaker
{
	public static class CommandBuilder
	{
		private static string? _audioFilename;

		private const int framerate = 30;
		private const int duration = 2;

		public static string BuildSlideShowCommand(string path) {
			// count number of images

			var images = Directory
				.EnumerateFiles(path)
				.Where(file => Constants.ImageFileTypes.Contains(Path.GetExtension(file).ToLower()))
				.Select(a => Path.GetFileName(a));
			var imageCount = images.Count();
			if (imageCount == 0) {
				return string.Empty;
			}

			// audio length
			_audioFilename = Directory
				.EnumerateFiles(path)
				.Where(file => Constants.AudioFileTypes.Contains(Path.GetExtension(file).ToLower()))
				.FirstOrDefault();
			if (string.IsNullOrWhiteSpace(_audioFilename)) {
				return string.Empty;
			}
			var audioTimeSpan = MediaReader.GetMediaTimeSpan(_audioFilename);

			// get image view time in seconds
			var imageViewTime = (int)Math.Round(audioTimeSpan.TotalSeconds / imageCount, 0, MidpointRounding.AwayFromZero);

			return CreateSlideShowCommand(images, imageCount, imageViewTime);
		}

		private static string CreateSlideShowCommand(IEnumerable<string> imageNames, int imageCount, int imageViewTime) {
			var first = new StringBuilder();
			var second = new StringBuilder();
			var third = new StringBuilder();

			var i = 0;
			foreach (var imageName in imageNames) {
				var firstCommand = $"-loop 1 -t {imageViewTime} -i {imageName} `\r\n";
				first.Append(firstCommand);
				// don't know what d=125 does
				// don't know what st=4 does
				// don't know what d=1 does
				// don't know how to get fade in and out
				if (i < imageCount - 1) {
					var secondCommand = $"[{i+1}]fade=d={duration}:t=in:alpha=1,setpts=PTS-STARTPTS+{((i + 1) * imageViewTime)}/TB[f{i}]; `\r\n ";
					//var secondCommand = $"[{i}:v]zoompan=z='if(lte(zoom,1.0),1.5,max(1.001,zoom-0.0015))':d=125,fade=t=out:st=4:d={duration}[v{i}];";
					second.Append(secondCommand);
				}
				if (i == 0) {
					var thirdCommand = $"[{i}][f{i}]overlay[bg{i + 1}];";
					third.Append(thirdCommand);
				} else if (i < imageCount - 2) {
					var thirdCommand = $"[bg{i}][f{i}]overlay[bg{i + 1}];";
					third.Append(thirdCommand);
				} else if (i == imageCount - 1) {
					var thirdCommand = $"[bg{i-1}][f{i-1}]overlay";
					third.Append(thirdCommand);
				}
				i++;
			}

			var command = new StringBuilder();
			command.AppendLine("ffmpeg `");
			command.Append(first);
			command.AppendLine("-filter_complex `");
			command.Append("\"");
			command.Append(second);
			command.Append(third);
			var end = $",format={Constants.VideoFormat}\" -map \"[v]\" -r {framerate} {Constants.OutputSlideShowVideoFileName}";
			command.AppendLine(end);
			return command.ToString();
		}

		public static string BuildMusicCommand() {
			if (string.IsNullOrWhiteSpace(_audioFilename)) {
				return string.Empty;
			}
			return $"ffmpeg -i {Constants.OutputSlideShowVideoFileName} -i {_audioFilename} -c copy -map 0:v:0 -map 1:a:0 -shortest {Constants.OutputFinalVideoFileName}";
		}
	}
}