using System;
using System.Globalization;

using System;
using System.Threading;
using System.Diagnostics.Metrics;


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

            while (true)
            {
                if (Grid.IsGameOver())
                {
                    Console.Clear();
                    Console.WriteLine("G A M E  O V E R");
                    Console.WriteLine($"Score: {Grid.score}");
                    Console.WriteLine("My Emperor... I've Failed Youuuuuu!!");
                   
                    input = Console.ReadLine();
                   
                    
                    break;

                }
                
                Grid.ClearFullRows();
                Tetrominos.DrawBoard();
                Thread.Sleep(500);
                Tetrominos.MovePiece(0, 1);
            
            }
            Console.SetCursorPosition(0, 0);
            goto Start;
        }
    }
}