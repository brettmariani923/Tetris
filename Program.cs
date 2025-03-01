﻿using System;
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
                    Console.SetCursorPosition(0,0);
                    Console.WriteLine("G A M E  O V E R");
                    Console.WriteLine("Press space bar to continue");
                    break;
                    input = Console.ReadLine();
                    if (input == " ")
                    { goto Start; }

                    
                }

                Grid.ClearFullRows();
                Tetrominos.DrawBoard();
                Thread.Sleep(500);
                Tetrominos.MovePiece(0, 1);
                
            }
            Console.SetCursorPosition(0, 2);
            goto Start;
        }
    }
}