using NAudio.Wave;

namespace FFMpegSlideshowMaker
{
	public static class MediaReader
	{
		public static TimeSpan GetMediaTimeSpan(string filename) {
			var reader = new Mp3FileReader(filename);
			return reader.TotalTime;
		}

		public static double GetMediaDuration(string filename) {
			double duration = 0.0;
			using (var fs = File.OpenRead(filename)) {
				var frame = Mp3Frame.LoadFromStream(fs);
				while (frame != null) {
					if (frame.ChannelMode == ChannelMode.Mono) {
						duration += (double)frame.SampleCount * 2.0 / (double)frame.SampleRate;
					} else {
						duration += (double)frame.SampleCount * 4.0 / (double)frame.SampleRate;
					}
					frame = Mp3Frame.LoadFromStream(fs);
				}
			}
			return duration;
		}
	}
}