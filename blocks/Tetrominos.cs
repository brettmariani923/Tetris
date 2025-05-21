using System.Drawing;
using System.Threading.Tasks.Sources;
using System.Text;

namespace Tetris;

public class Tetrominos
{
    static (int x, int y)[] currentPiece;
    static Random random = new Random();
    static (int x, int y) positionOfPiece;
    static int width = 10, height = 20;
    static bool gameOver = false;
    static int currentPieceIndex;

    public static (int x, int y)[][] piecesPool = new (int, int)[][]
   {
        new [] { (0,0), (1,0), (0,1), (1,1) }, 
                    
        new [] { (0,0), (-1,0), (1,0), (2,0) }, 
        
        new [] { (0,0), (-1,0), (1,0), (0,1) }, 
        
        new [] { (0,0), (-1,0), (1,0), (1,1) }, 
        
        new [] { (0,0), (-1,0), (1,0), (-1,1) }, 
        
        new[] { (0,0), (-1,0), (1, 0), (0, 1) }, 
    };

    public static ConsoleColor[] pieceColors =
    {
    ConsoleColor.Yellow,
    ConsoleColor.Cyan,
    ConsoleColor.Magenta,
    ConsoleColor.Blue,
    ConsoleColor.DarkYellow,
    ConsoleColor.Green,
    ConsoleColor.Red
    };

    public static bool CanMove(int dx, int dy)                     
    {                                                       
        foreach (var (px, py) in currentPiece)              
        {                                                 
            int x = positionOfPiece.x + px + dx;
            int y = positionOfPiece.y + py + dy;
            if (x < 0 || x >= width || y < 0 || y >= height || (y >= 0 && Grid.newGrid[y, x] != 0))
                return false;
        }
        return true;
    }

    public static void NewPiece()                                                                         
    {
        currentPieceIndex = random.Next(piecesPool.Length);
        currentPiece = piecesPool[currentPieceIndex];
        positionOfPiece = ((width / 2), 0);
        if (!CanMove(0, 0)) gameOver = true;
    }

    static void PlacePiece()                                                                                                                      
    {                                                                         
        ConsoleColor pieceColor = pieceColors[currentPieceIndex];                                                                              

        foreach (var (px, py) in currentPiece)
        {
            int x = positionOfPiece.x + px;
            int y = positionOfPiece.y + py;
            if (y >= 0)
            {
                Grid.newGrid[y, x] = 1;
                Grid.colorGrid[y, x] = pieceColor;
            }
        }
    }

    public static void MovePiece(int dx, int dy)        
    {                                                   
        if (CanMove(dx, dy))                            
        {
            positionOfPiece.x += dx;
            positionOfPiece.y += dy;
        }
        else if (dy > 0)
        {
            PlacePiece();
            Grid.ClearFullRows();
            NewPiece();
        }
    }

    public static bool CanRotate((int x, int y)[] rotatedPiece)
    {
        foreach (var (px, py) in rotatedPiece)
        {
            int x = positionOfPiece.x + px;
            int y = positionOfPiece.y + py;
            if (x < 0 || x >= width || y < 0 || y >= height || (y >= 0 && Grid.newGrid[y, x] != 0))
                return false;
        }
        return true;
    }

    public static void RotatePiece()
    {
        var rotatedPiece = currentPiece
            .Select(cell => (-cell.y, cell.x))
            .ToArray();

        if (CanRotate(rotatedPiece))
            currentPiece = rotatedPiece;
    }

    public static void PieceMover()
    {
        for (int y = 0; y < height; y++)
        {
            Console.SetCursorPosition(1, y + 1);
            for (int x = 0; x < width; x++)
            {
                bool isPiece = false;
                ConsoleColor color = ConsoleColor.White;

                foreach (var (px, py) in currentPiece)
                {
                    if (positionOfPiece.x + px == x && positionOfPiece.y + py == y)
                    {
                        isPiece = true;
                        color = pieceColors[currentPieceIndex];
                        break;
                    }
                }
                if (!isPiece && Grid.newGrid[y, x] != 0)
                    color = Grid.colorGrid[y, x];

                Console.ForegroundColor = color;
                Console.Write(isPiece || Grid.newGrid[y, x] != 0 ? "◯" : ".");

            }
            Console.WriteLine();

        }
    }
    public static bool pause = false;
    public static void ReadInput()             
    {                                           
        while (!gameOver)                       
        {   
            var key = Console.ReadKey(true).Key;

            if (key == ConsoleKey.LeftArrow) MovePiece(-1, 0);
            if (key == ConsoleKey.RightArrow) MovePiece(1, 0);
            if (key == ConsoleKey.DownArrow) MovePiece(0, 1);
            if (key == ConsoleKey.UpArrow) RotatePiece();
            if (key == ConsoleKey.Spacebar)
            {
                pause = !pause;
                Console.Clear();
                Console.WriteLine("           P A U S E ");
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("======== TETRIS CONTROLS ========");
                Console.WriteLine("⬅  Left Arrow      - Move left ");
                Console.WriteLine("🠮 Right Arrow      - Move right ");
                Console.WriteLine("⬇  Down Arrow      - Soft drop  ");
                Console.WriteLine("⬆  Up Arrow        - Rotate piece  ");
                Console.WriteLine("Spacebar           - P A U S E");
                Console.WriteLine("=================================");
                Console.WriteLine(". . . . . . .");
                Console.WriteLine();
                Console.WriteLine($"  Score: {Grid.score}");
                Console.WriteLine("    _._     _,-'\"\"`-._\r\n     (,-.`._,'(       |\\`-/|\r\n         `-.-' \\ )-`( , o o)\r\n             `-    \\`_`\"'-");

                while (true)
                {
                    var resumeKey = Console.ReadKey(true).Key;
                    if (resumeKey == ConsoleKey.Spacebar)
                    {
                        pause = false;
                        Console.Clear();
                        break;
                    }
                }
            }  
        }
    }
}

