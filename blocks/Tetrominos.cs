using System.Drawing;
using System.Threading.Tasks.Sources;
using System.Text;

namespace Tetris;

public class Tetrominos
{


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


    static (int x, int y)[] currentPiece;           
    static Random random = new Random();            
    static (int x, int y) positionOfPiece;            
    static int width = 10, height = 20;             
    static bool gameOver = false;
    static int currentPieceIndex;

    public static bool CanMove(int dx, int dy)                     
    {                                                       
        foreach (var (px, py) in currentPiece)              
        {                                                 
            int x = positionOfPiece.x + px + dx;
            int y = positionOfPiece.y + py + dy;
            if (x < 0 || x >= width || y < 0 || y >= height || (y >= 0 && Grid.NewGrid[y, x] != 0))
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
                Grid.NewGrid[y, x] = 1;
                Grid.ColorGrid[y, x] = pieceColor;
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
            if (x < 0 || x >= width || y < 0 || y >= height || (y >= 0 && Grid.NewGrid[y, x] != 0))
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

    private static void DrawBorder()
    {
        int boardWidth = width;
        int boardHeight = height;

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

        DrawBorder();
        Console.SetCursorPosition(0, 0);

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

                if (!isPiece && Grid.NewGrid[y, x] != 0)
                    color = Grid.ColorGrid[y, x];

                Console.ForegroundColor = color;
                Console.Write(isPiece || Grid.NewGrid[y, x] != 0 ? "◯" : ".");

            }

            Console.WriteLine();


        }
        int textX = width + 4;
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
        };
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
        Console.SetCursorPosition(textX +2, textY + 16); if (Grid.score > 120)
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
                Console.WriteLine("🠮 Right Arrow     - Move right ");
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

