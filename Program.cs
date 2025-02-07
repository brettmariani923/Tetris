using System;
using System.Globalization;

using System;
using System.Threading;


namespace Tetris
{
    public class Program
    {
        public static void Main(string[] args)
        {
            bool gameOver = false;
            Console.CursorVisible = false;
            Tetrominos.NewPiece();
            Thread inputThread = new Thread(Tetrominos.ReadInput);
            inputThread.Start();
            while (!gameOver)
            {
                Tetrominos.DrawBoard();
                Thread.Sleep(500);
                Tetrominos.MovePiece(0, 1);
            }
            Console.SetCursorPosition(0, Grid.Height + 2);
            Console.WriteLine("Game Over!");

        }
    }
}