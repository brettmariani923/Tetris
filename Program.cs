using System;
using System.Threading;
using NAudio.Wave;
using System.IO;
using System.Diagnostics;

namespace Tetris
{
    public class Program
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

        public static void Main(string[] args)
        {
            PlayBackgroundMusic();

            string input;
            bool gameOver = false;
            Tetrominos.NewPiece();
            Thread inputThread = new Thread(Tetrominos.ReadInput);
            inputThread.Start();
            Console.SetCursorPosition(0, 0);
            Console.CursorVisible = false;

            while (true)
            {
                if (Grid.IsGameOver())
                {
                    Console.Clear();
                    Console.WriteLine("G A M E  O V E R");
                    Console.WriteLine($"Score: {Grid.score}");
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine("www.youtube.com/watch?v=sDipbctxGC4");
                    Console.WriteLine("        .\r\n       -.\\_.--._.______.-------.___.---------.___\r\n       )`.                                       `-._\r\n      (                                              `---.\r\n      /o                                                  `.\r\n     (                                                      \\\r\n   _.'`.  _                                                  L\r\n   .'/| \"\" \"\"\"\"._                                            |\r\n      |          \\             |                             J\r\n                  \\-._          \\                             L\r\n                  /   `-.        \\                            J\r\n                 /      /`-.      )_                           `\r\n                /    .-'    `    J  \"\"\"\"-----.`-._             |\\            \r\n              .'   .'        L   F            `-. `-.___        \\`.\r\n           ._/   .'          )  )                `-    .'\"\"\"\"`.  \\)\r\n__________((  _.'__       .-'  J              _.-'   .'        `. \\\r\n                   \"\"\"\"\"\"\"((  .'--.__________(   _.-'___________)..|----------------._____\r\n                            \"\"                \"\"\"               ``U'\r\n");
                    Console.WriteLine("Brett Mariani 2025");

                    input = Console.ReadLine();
                    if (input == "yes")
                    {
                        Console.Clear();
                        
                    }
                    else
                        break;
                }

                if (!Tetrominos.pause)
                { 
                    Grid.ClearFullRows();
                    Tetrominos.DrawBoard();
                    Thread.Sleep(Math.Max(86, 400 - (Grid.score * 10)));
                    Tetrominos.MovePiece(0, 1);
                }
                else
                {
                    Thread.Sleep(100);
                }
            }
            Console.SetCursorPosition(0, 0);
       
        }

        private static void PlayBackgroundMusic()
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

        private static void PlayRandomSong()
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