namespace FFMpegSlideshowMaker
{
	public class Constants
	{
		public const string OutputSlideShowVideoFileName = "out.mp4";
		public const string OutputFinalVideoFileName = "out-music.mp4";
		public const string VideoFormat = "yuv420p[v]";
		
		public static List<string> ImageFileTypes = new List<string> { ".png", ".jpg", ".jpeg" };
		public static List<string> AudioFileTypes = new List<string> { ".mp3" };
		public const string MarryVideoWithAudio = "ffmpeg -i out.mp4 -i audio.mp3 -c copy -map 0:v:0 -map 1:a:0 -shortest out-music.mp4";
	}
}