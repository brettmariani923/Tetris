using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Timers;

namespace Tetris;

public class Grid 
{
    public static int Width = 20;
    public static int Height = 10;
    public static int[,] NewGrid = new int[Width, Height];
    public static ConsoleColor[,] ColorGrid = new ConsoleColor[20, 10];
    static Random random = new Random();
    public Position? StartPoint;
    public int NewPiece;
    private static int x;
    public static int score = 0;

    public int this[int w, int h]               
    {
        get => NewGrid[w, h];
        set => NewGrid[w, h] = value;
    }

    public Position Start                                   
    {
        get => new (Width = 10 / 2, Height = 20);
        set => StartPoint = value;
    }
    
    public int WhichBlock
    {
        get => random.Next(Tetrominos.piecesPool.Length);
        set => NewPiece = value;
    }

    

    public bool IsCellEmpty(int w, int h)
    {
        return NewGrid[w, h] == 0;   
    }
    
    public static bool IsRowFull(int w)
    {
        for (int h = 0; h < Height; h++)    
        {
            if (NewGrid[w, h] == 0)     
            {
                return false;
            }
        }
        return true;
    }
    
    public static bool IsRowEmpty(int w)
    {
        for (int h = 0; h < Height; h++)
        {
            if (NewGrid[w, h] != 0)         
            {
                return false;
            }
        }
        return true;
    }

    public static void ClearRow(int w)
    {
        if (IsRowFull(w))
        {
            for (int h = 0; h < Height; h++)
            {
                NewGrid[w, h] = 0;
            }
            
            score++;
            
        }
    }

    public static void ClearBoard()
    {
        for (int h = 0; h < Width; h++)
        {
            for (int w = 0; w < Height; w++)
            {
                NewGrid[h, w] = 0;
            }
            
        }
    }

    public static void RowDown(int w, int rowNumber)
    {
        for (int h = 0; h < Height; h++)
        {
            NewGrid[w + rowNumber, h] = NewGrid[w, h];
            NewGrid[w, h] = 0;
        }
    }

    public static int ClearFullRows()
    {
        int clear = 0;

        for (int w = Width - 1; w >= 0; w--)
        {
            if (IsRowFull(w))
            {
                ClearRow(w);
                clear++;
            }
            else if (clear > 0)
            {
                RowDown(w, clear);
            }
        }
        return clear;
    }

    public static bool IsGameOver()                             
    {                                                          
        for (int h = 0; h < Height; h++)                       
        {
            if (NewGrid[0, h] != 0)
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