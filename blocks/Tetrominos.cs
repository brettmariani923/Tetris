namespace Tetris;

//This class manages the game pieces: piece creation, movement, and rotation
//Coordinates with the Grid class to update the game grid
//Handles rendering of active pieces and user input

//"This class encapsulates all logic related to the tetromino pieces — like selecting a random piece, moving it based on player input, and determining if it can be rotated or placed."
public class Tetrominos
{
    static (int x, int y)[] currentPiece; // The current piece being played, defined as a set of coordinates relative to the center. Accepts a tuple of x and y coordinates
    static Random random = new Random();
    static (int x, int y) positionOfPiece; //The variable positionOfPiece represents the top-left grid position of the center point of the current falling tetromino. It acts as the anchor for all the piece’s relative coordinates.
    //The game uses positionOfPiece as the base point, and each block of the tetromino is positioned relative to it using values from currentPiece(like (-1,0), (1,0), etc.).
    //So the actual grid positions of a tetromino block are computed like this:
    static int width = 10, height = 20;
    static bool gameOver = false;
    public static bool pause = false;
    static int currentPieceIndex; // The index of the current piece in the pieces pool, used to identify which piece is currently active

    // The pieces are defined in a 2x2 grid, with the center of the piece at (0,0).
    // The other points are relative to that. So (1,0) is the square one to the right of (0,0),
    // (-1,0) is the square one to the left of (0,0), (0,1) is the square one above (0,0), and (0,-1)
    // is the square one below (0,0), etc.

    //I designed it like this after reading the code for other tetris games, and of the few methods used for methods and rotation 
    //this design best simplified movemnt and rotation.
    public static (int x, int y)[][] piecesPool = new (int, int)[][] //This section holds an array of arrays, where each inner array represents a potential tetromino piece.
   {
        new [] { (0,0), (1,0), (0,1), (1,1) }, 
                    
        new [] { (0,0), (-1,0), (1,0), (2,0) }, 
        
        new [] { (0,0), (-1,0), (1,0), (0,1) }, 
        
        new [] { (0,0), (-1,0), (1,0), (1,1) }, 
        
        new [] { (0,0), (-1,0), (1,0), (-1,1) }, 
        
        new[] { (0,0), (-1,0), (1, 0), (0, 1) }, 
    };
    public static ConsoleColor[] pieceColors = //This section holds an array of colors, where each color corresponds to a piece in the piecesPool array.
    {
    ConsoleColor.Yellow,   // O-piece
    ConsoleColor.Cyan,     // I-piece
    ConsoleColor.Magenta,  // T-piece
    ConsoleColor.Blue,     // J-piece
    ConsoleColor.DarkYellow, // L-piece
    ConsoleColor.Green,    // S-piece
    ConsoleColor.Red       // Z-piece
};


    //The CanMove method checks if the piece can move to a new position.
    //It checks if the new position is within the bounds of the grid and if there are no other pieces in the way.
    //If the piece can move, it returns true. Otherwise, it returns false.
    //It does this by checking each point of the piece against the grid.

    //The canmove method accepts input as dx and dy, which are the changes caused by the user inputs to the x and y coordinates respectively.
    //we start by defining the x variable, which adds the position of the current piece (px, comes from the piece's shape (e.g. (-1,0), (1,0)) with the value of the user input (dx, left, right, up, down).
    //->  if (x < 0 || x >= width || y < 0 || y >= height || (y >= 0 && Grid.newGrid[y, x] != 0))
    public static bool CanMove(int dx, int dy)  //Method that accepts input as dx and dy, which are the changes in the x and y coordinates respectively -
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

