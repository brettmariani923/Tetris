namespace Tetris;

public class Tetrominos
{
    static (int x, int y)[] currentPiece;
    static Random random = new Random();
    static (int x, int y) positionOfPiece;
    static int width = 10, height = 20;
    static bool gameOver = false;
    public static bool pause = false;
    static int currentPieceIndex;

    // The pieces are defined in a 2x2 grid, with the center of the piece at (0,0).
    // The other points are relative to that. So (1,0) is the square one to the right of (0,0),
    // (-1,0) is the square one to the left of (0,0), (0,1) is the square one above (0,0), and (0,-1)
    // is the square one below (0,0), etc.
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

    //The CanMove method checks if the piece can move to a new position.
    //It checks if the new position is within the bounds of the grid and if there are no other pieces in the way.
    //If the piece can move, it returns true. Otherwise, it returns false.
    //It does this by checking each point of the piece against the grid.
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

    //The NewPiece method generates a new piece by selecting a random piece from the pieces pool.
    //It sets the position of the new piece to the center of the grid and checks if it can move.
    public static void NewPiece()                                                                         
    {
        currentPieceIndex = random.Next(piecesPool.Length);
        currentPiece = piecesPool[currentPieceIndex];
        positionOfPiece = ((width / 2), 0);
        if (!CanMove(0, 0)) gameOver = true;
    }

    //The PlacePiece method places the current piece on the grid by setting the grid cells to 1
    //and setting the color of the cells. It does this by iterating through each point of the piece
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

    //The MovePiece method moves the current piece by a specified amount in the x and y directions.
    //It checks if the piece can move to the new position and if it can, it updates the position.
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

    //The CanRotate method checks if the piece can be rotated by checking if the new position of the piece
    //is within the bounds of the grid and if there are no other pieces in the way.
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

    //The RotatePiece method rotates the current piece by swapping the x and y coordinates of each point.
    //It checks if the piece can be rotated and if it can, it updates the current piece.
    public static void RotatePiece()
    {
        var rotatedPiece = currentPiece
            .Select(cell => (-cell.y, cell.x))
            .ToArray();

        if (CanRotate(rotatedPiece))
            currentPiece = rotatedPiece;
    }

    //The DrawCurrentFrame method draws the current piece and the grid to the console.
    //It does this by iterating through each point of the board and checking if it is a piece or part of the grid.
    //It sets the color of the piece and the grid and draws it to the console.

    public static void DrawCurrentFrame()
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

    //The ReadInput method reads the input from the user and moves the piece accordingly.
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

