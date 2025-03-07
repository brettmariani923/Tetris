using System;
using System.Globalization;

using System;
using System.Threading;
using System.Diagnostics.Metrics;
using System.Threading.Tasks.Sources;
using System.ComponentModel.Design.Serialization;
using NAudio.Wave;
using System.Diagnostics;


namespace Tetris
{
    public class Program
    {

        private static IWavePlayer waveOut;
        private static AudioFileReader audioFile;

        public static void Main(string[] args)
        {
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "3-32 Museum - Welcome to the Museum!.mp3");
            PlayBackgroundMusic(filePath);

            waveOut?.Stop();

        Start:
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
                 
                    Console.WriteLine("www.youtube.com/watch?v=sDipbctxGC4");
                  
                   
                    Console.WriteLine("        .\r\n       -.\\_.--._.______.-------.___.---------.___\r\n       )`.                                       `-._\r\n      (                                              `---.\r\n      /o                                                  `.\r\n     (                                                      \\\r\n   _.'`.  _                                                  L\r\n   .'/| \"\" \"\"\"\"._                                            |\r\n      |          \\             |                             J\r\n                  \\-._          \\                             L\r\n                  /   `-.        \\                            J\r\n                 /      /`-.      )_                           `\r\n                /    .-'    `    J  \"\"\"\"-----.`-._             |\\            \r\n              .'   .'        L   F            `-. `-.___        \\`.\r\n           ._/   .'          )  )                `-    .'\"\"\"\"`.  \\)\r\n__________((  _.'__       .-'  J              _.-'   .'        `. \\\r\n                   \"\"\"\"\"\"\"((  .'--.__________(   _.-'___________)..|----------------._____\r\n                            \"\"                \"\"\"               ``U'\r\n");
                    Console.WriteLine("Brett Mariani 2025");

                    input = Console.ReadLine();
                   
                    
                    break;

                }

                if (!Tetrominos.pause)
                {
                    Grid.ClearFullRows();
                    Tetrominos.DrawBoard();
                    Thread.Sleep(Math.Max(50, 400 - (Grid.score * 10)));
                    Tetrominos.MovePiece(0, 1);
                }
                else
                {
                    
                    Thread.Sleep(100);
                }
            }
            Console.SetCursorPosition(0, 0);
            goto Start;
        }
        private static void PlayBackgroundMusic(string filePath)
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    Console.WriteLine("Error: MP3 file not found at " + filePath);
                    return;
                }

                waveOut = new WaveOutEvent();
                audioFile = new AudioFileReader(filePath);

                // Loop the music when it stops
                waveOut.PlaybackStopped += (s, e) =>
                {
                    audioFile.Position = 0; // Restart from the beginning
                    waveOut.Play();
                };

                waveOut.Init(audioFile);
                waveOut.Play();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error playing sound: {ex.Message}");
            }
        }

    }
}