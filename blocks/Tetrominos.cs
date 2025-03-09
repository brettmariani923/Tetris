using System.Drawing;
using System.Threading.Tasks.Sources;

namespace Tetris;

public class Tetrominos
{

    public static (int x, int y)[][] piecesPool = new (int, int)[][]
   {
        //making an array of arrays,
        //for the blocks center point is (0,0) based on x, y coordinates
        //so for example in the square piece, (0,0) is the first block
        //(1,0) is describing one block to the right on the x axis
        //(0,1) is describing one block directly above on the y axis
        //(1,1) describes a block one to the right and one up
        
        new [] { (0,0), (1,0), (0,1), (1,1) }, //square
                    
        
        new [] { (0,0), (-1,0), (1,0), (2,0) }, //long
        
        new [] { (0,0), (-1,0), (1,0), (0,1) }, //t piece
        
        new [] { (0,0), (-1,0), (1,0), (1,1) }, //j block
        
        new [] { (0,0), (-1,0), (1,0), (-1,1) }, //l block
        
        new[] { (0,0), (-1,0), (1, 0), (0, 1) }, //The blarg (supposed to make you say "blarg" in anger)
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


    static (int x, int y)[] currentPiece;           //for pieces
    static Random random = new Random();            //instatiating random object for pieces
    static (int x, int y) positionOfPiece;                 //declares pieces poisiton on the gird
    static int width = 10, height = 20;             //hight and width of gameboard
    static bool gameOver = false;
    static int currentPieceIndex;

    public static bool CanMove(int dx, int dy)                     //couldn't figure this out on my own so I 
    {                                                       //found this part online.
        foreach (var (px, py) in currentPiece)              //defines the elements of each dimension of the piece(x or y)
        {                                                   //to see if the piece is in play, if it is inside the gameboard
            int x = positionOfPiece.x + px + dx;
            int y = positionOfPiece.y + py + dy;
            if (x < 0 || x >= width || y < 0 || y >= height || (y >= 0 && Grid.NewGrid[y, x] != 0))
                return false;
        }
        return true;
    }

    public static void NewPiece()       //picks a random array from the array
                                        //and positions it at the top and center of the board
                                        //if it hits zero it starts over
    {
        currentPieceIndex = random.Next(piecesPool.Length);
        currentPiece = piecesPool[currentPieceIndex];
        positionOfPiece = ((width / 2), 0);
        if (!CanMove(0, 0)) gameOver = true;
    }

    static void PlacePiece()                                                 //couldn't figure this out on my own
                                                                             //so I found this part online. 
    {                                                                           //basically changes pieces from active to set so nex piece can spawn, works by adding the current

        ConsoleColor pieceColor = pieceColors[currentPieceIndex];                    //pieces position (px/py = relative positions of each cell that makes up piece)
                                                                                     //to the game grid [y, x] when it reaches the bottom and marks the cell as occupied (1)

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

    public static void MovePiece(int dx, int dy)        //this just determines if the piece can move or if its set
    {                                                   //if it CanMove, it accepts inputs and adds them to change the position of the piece (dx = horizontal movement of piece, dy vertical movement of piece)
        if (CanMove(dx, dy))                            //if not, it places the piece, checks for full rows, then initiates a new piece
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

    public static void DrawBoard()                  //wrote about half of this, got some help with the part that determines where the piece is.
                                                    //this draws the gameboard and checks if the grid is currently occupied by a piece
    {
        DrawBorder();
        Console.SetCursorPosition(0, 0);

        for (int y = 0; y < height; y++)            //uses a bool to check cells for a piece, if it does it registers true it draws an x
        {
            Console.SetCursorPosition(1, y + 1); //otherwise it does . for empty space
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
                Console.Write(isPiece || Grid.NewGrid[y, x] != 0 ? "X" : ".");

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
    }


    

    public static bool pause = false;



    public static void ReadInput()              //this is for inputs that determine how the piece move
    {                                           //currently i have an up input just so i can play around with it
        while (!gameOver)                       //later i'll change that
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
                Console.WriteLine("            P A U S E ");
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("======== TETRIS CONTROLS ========");
                Console.WriteLine("⬅  Left Arrow      - Move left ");
                Console.WriteLine("➡  Right Arrow     - Move right ");
                Console.WriteLine("⬇  Down Arrow      - Soft drop  ");
                Console.WriteLine("⬆  Up Arrow        - Rotate piece  ");
                Console.WriteLine("Spacebar           - P A U S E");
                Console.WriteLine("=================================");

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

