using System;
using System.Globalization;

using System;
using System.Threading;
using System.Diagnostics.Metrics;
using System.Threading.Tasks.Sources;
using System.ComponentModel.Design.Serialization;


namespace Tetris
{
    public class Program
    {
       
        public static void Main(string[] args)
        {
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
                    Console.WriteLine("My Emperor... I've Failed Youuuuuu!!");
                    Console.WriteLine("www.youtube.com/watch?v=sDipbctxGC4");
                  
                   
                    Console.WriteLine("        .\r\n       -.\\_.--._.______.-------.___.---------.___\r\n       )`.                                       `-._\r\n      (                                              `---.\r\n      /o                                                  `.\r\n     (                                                      \\\r\n   _.'`.  _                                                  L\r\n   .'/| \"\" \"\"\"\"._                                            |\r\n      |          \\             |                             J\r\n                  \\-._          \\                             L\r\n                  /   `-.        \\                            J\r\n                 /      /`-.      )_                           `\r\n                /    .-'    `    J  \"\"\"\"-----.`-._             |\\            \r\n              .'   .'        L   F            `-. `-.___        \\`.\r\n           ._/   .'          )  )                `-    .'\"\"\"\"`.  \\)\r\n__________((  _.'__       .-'  J              _.-'   .'        `. \\\r\n                   \"\"\"\"\"\"\"((  .'--.__________(   _.-'___________)..|----------------._____\r\n                            \"\"                \"\"\"               ``U'\r\n");
                    Console.WriteLine("Brett Mariani 2025");

                    input = Console.ReadLine();
                   
                    
                    break;

                }
                

                Grid.ClearFullRows();
                Tetrominos.DrawBoard();
                Thread.Sleep(Math.Max(50, 400 - (Grid.score * 10)));
                Tetrominos.MovePiece(0, 1);
            
            }
            Console.SetCursorPosition(0, 0);
            goto Start;
        }
    }
}