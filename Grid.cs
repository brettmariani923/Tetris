namespace Tetris;

public class Grid
{
    public static int width = 10;
    public static int height = 20;
    public static int[,] newGrid = new int[height, width];
    public static ConsoleColor[,] colorGrid = new ConsoleColor[height, width];
    public int NewPiece;
    public static int score = 0;

    // The grid is a 2D array of integers, where each cell can be either 0 (empty) or a positive integer (filled).
    // The IsRowFull method checks if a row is full by checking if all cells in that row are filled (not 0).
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

    // The ClearRow method clears a row by setting all cells in that row to 0 (empty).
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

    // The RowDown method shifts all rows above the cleared row down by a specified offset.
    public static void RowDown(int row, int offset)
    {
        for (int col = 0; col < width; col++)
        {
            newGrid[row + offset, col] = newGrid[row, col];
            newGrid[row, col] = 0;
        }
    }

    // The ClearFullRows method checks each row from the bottom to the top and clears it if it's full.
    // It also shifts down any rows above the cleared row by the number of cleared rows.
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

    // The IsGameOver method checks if the game is over by checking if any cell in the top row is filled (not 0).
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

    // The DrawBorder method draws a border around the grid using Unicode characters.
    // It sets the cursor position and writes the border characters to the console.
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

    // The DrawBoard method draws the grid to the console by iterating through each cell in the grid.
    // It sets the cursor position and writes the cell character to the console.
    // It also draws the current piece and the score.
    public static void DrawBoard()
    {
        System.Console.OutputEncoding = System.Text.Encoding.UTF8;

        DrawBorder();
        Console.SetCursorPosition(0, 0);
        Tetrominos.DrawCurrentFrame();

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