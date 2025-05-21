using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Timers;

namespace Tetris;

public class Grid
{
    public static int width = 10;
    public static int height = 20;
    public static int[,] newGrid = new int[height, width];
    public static ConsoleColor[,] colorGrid = new ConsoleColor[height, width];
    public int NewPiece;
    public static int score = 0;

    public int this[int row, int col]
    {
        get => newGrid[row, col];
        set => newGrid[row, col] = value;
    }

    public bool IsCellEmpty(int row, int col)
    {
        return newGrid[row, col] == 0;
    }

    public static bool IsRowFull(int row)
    {
        for (int col = 0; col < width; col++)
        {
            if (newGrid[row, col] == 0)
            {
                return false;
            }
        }
        return true;
    }

    public static bool IsRowEmpty(int row)
    {
        for (int col = 0; col < width; col++)
        {
            if (newGrid[row, col] != 0)
            {
                return false;
            }
        }
        return true;
    }

    public static void ClearRow(int row)
    {
        if (IsRowFull(row))
        {
            for (int col = 0; col < width; col++)
            {
                newGrid[row, col] = 0;
            }

            score++;
        }
    }

    public static void ClearBoard()
    {
        for (int row = 0; row < height; row++)
        {
            for (int col = 0; col < width; col++)
            {
                newGrid[row, col] = 0;
            }
        }
    }

    public static void RowDown(int row, int offset)
    {
        for (int col = 0; col < width; col++)
        {
            newGrid[row + offset, col] = newGrid[row, col];
            newGrid[row, col] = 0;
        }
    }

    public static int ClearFullRows()
    {
        int clear = 0;

        for (int row = height - 1; row >= 0; row--)
        {
            if (IsRowFull(row))
            {
                ClearRow(row);
                clear++;
            }
            else if (clear > 0)
            {
                RowDown(row, clear);
            }
        }
        return clear;
    }

    public static bool IsGameOver()
    {
        for (int col = 0; col < width; col++)
        {
            if (newGrid[0, col] != 0)
            {
                return true;
            }
        }
        return false;
    }
    public static void DrawBorder()
    {
        int boardWidth = 10;
        int boardHeight = 20;

        int startX = 0;
        int startY = 0;

        Console.ForegroundColor = ConsoleColor.White;

        Console.SetCursorPosition(startX, startY);
        Console.Write("╔" + new string('═', boardWidth) + "╗");

        for (int i = 0; i < boardHeight; i++)
        {
            Console.SetCursorPosition(startX, startY + i + 1);
            Console.Write("║");
            Console.SetCursorPosition(startX + boardWidth + 1, startY + i + 1);
            Console.Write("║");
        }

        Console.SetCursorPosition(startX, startY + boardHeight + 1);
        Console.Write("╚" + new string('═', boardWidth) + "╝");

        Console.ResetColor();
    }

    public static void DrawBoard()
    {
        System.Console.OutputEncoding = System.Text.Encoding.UTF8;

        Grid.DrawBorder();
        Console.SetCursorPosition(0, 0);
        Tetrominos.PieceMover();

        int textX = 10 + 4;
        int textY = 2;
        Console.ResetColor();
        Console.WriteLine();
        Console.WriteLine($"  Score: {Grid.score}");
        Console.WriteLine("    _._     _,-'\"\"`-._\r\n     (,-.`._,'(       |\\`-/|\r\n         `-.-' \\ )-`( , o o)\r\n             `-    \\`_`\"'-");

        Console.SetCursorPosition(textX, textY); if (Grid.score > 10)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Nice!");
        }
        Console.ForegroundColor = ConsoleColor.White;
        Console.SetCursorPosition(textX, textY + 2); if (Grid.score > 20)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Great job!");
        }
        ;
        Console.ForegroundColor = ConsoleColor.White;

        Console.SetCursorPosition(textX, textY + 4); if (Grid.score > 30)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Excellent!");
        }
        Console.ForegroundColor = ConsoleColor.White;

        Console.SetCursorPosition(textX, textY + 6); if (Grid.score > 40)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Fantastic!");
        }
        Console.ForegroundColor = ConsoleColor.White;

        Console.SetCursorPosition(textX, textY + 8); if (Grid.score > 50)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Increrdible!!");
        }
        Console.ForegroundColor = ConsoleColor.White;

        Console.SetCursorPosition(textX, textY + 10); if (Grid.score > 60)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Out of this world!!");
        }
        Console.ForegroundColor = ConsoleColor.White;

        Console.SetCursorPosition(textX, textY + 12); if (Grid.score > 70)
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Dino-mite!");
        }
        Console.SetCursorPosition(textX, textY + 14); if (Grid.score > 80)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("🦕");
        }
        Console.SetCursorPosition(textX + 2, textY + 14); if (Grid.score > 90)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("🦕");
        }
        Console.SetCursorPosition(textX + 4, textY + 14); if (Grid.score > 100)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("🦕");
        }
        Console.SetCursorPosition(textX, textY + 16); if (Grid.score > 110)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("🦕");
        }
        Console.SetCursorPosition(textX + 2, textY + 16); if (Grid.score > 120)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("🦕");
        }
        Console.SetCursorPosition(textX + 4, textY + 16); if (Grid.score > 130)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("🦕");
        }

        Console.SetCursorPosition(textX, textY + 18); if (Grid.score > 170)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("You're Unstoppable! 🦖");
        }

        Console.ForegroundColor = ConsoleColor.White;

    }

}