using System;
using System.Threading;
using NAudio.Wave;
using System.IO;
using System.Diagnostics;

namespace Tetris
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Music.PlayBackgroundMusic();

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

                    Console.ReadLine();
                 
                }

                if (!Tetrominos.pause)
                { 
                    Grid.ClearFullRows();
                    Grid.DrawBoard();
                    Thread.Sleep(Math.Max(100, 400 - (Grid.score * 10)));
                    Tetrominos.MovePiece(0, 1);
                }
                else
                {
                    Thread.Sleep(100);
                }
            }       
        }

    }
}