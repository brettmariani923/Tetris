using NAudio.Wave;

namespace Tetris
{
    public class Music
    {
        private static IWavePlayer waveOut;
        private static AudioFileReader audioFile;
        private static string[] playlist =
        {
            "7pm.mp3",
            "9pm.mp3",
            "3-32 Museum - Welcome to the Museum!.mp3",
        };
        private static Random random = new Random();

        public static void PlayBackgroundMusic()
        {
            try
            {
                PlayRandomSong();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error playing sound: {ex.Message}");
            }
        }

        public static void PlayRandomSong()
        {
            if (waveOut != null)
            {
                waveOut.Dispose();
                audioFile.Dispose();
            }

            string projectDirectory = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.FullName;

            string musicDirectory = Path.Combine(projectDirectory, "Assets", "Audio");

            int randomIndex = random.Next(playlist.Length);
            string filePath = Path.Combine(musicDirectory, playlist[randomIndex]);

            waveOut = new WaveOutEvent();
            audioFile = new AudioFileReader(filePath);

            waveOut.PlaybackStopped += (s, e) => PlayRandomSong();

            waveOut.Init(audioFile);
            waveOut.Play();
        }
    }
}
