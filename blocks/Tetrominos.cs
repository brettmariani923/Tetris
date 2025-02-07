namespace Tetris;

public class Tetrominos
{
     public static (int x, int y)[][] pieces = new (int, int)[][]
    {
        //for anyone reading this who might not understand right away,
        //the coordinate describe where the blocks are going from a 
        //center point (0,0) based on x, y coordinates
        //so for example in the square piece, (0,0) is the first block
        //(1,0) is describing one block to the right on the x axis
        //(0,1) is describing one block directly below on the y axis
        //(1,1) describes a block one to the right and one down from it
        
        new [] { (0,0), (1,0), (0,1), (1,1) }, //square
        
        new [] { (0,0), (-1,0), (1,0), (2,0) }, //long
        
        new [] { (0,0), (-1,0), (1,0), (0,1) }, //t piece
        
        new [] { (0,0), (-1,0), (1,0), (1,1) }, //j block
        
        new [] { (0,0), (-1,0), (1,0), (-1,1) }, //l block
        
        new[] { (0,0), (-1, -1), (1, 0), (2, 2) }, //The blarg (supposed to make you say "blarg" in anger)
        
        new[] { (0,0), (0,-1), (0,-2), (0, -3), (0, 1), (0, 2), (0, 3) } //The trolls bridge
    };
        
    static (int x, int y)[] currentPiece;
    static Random random = new Random();
    static (int x, int y) position;
    static int width = 10, height = 20;
    static bool gameOver = false;
      
    public static void NewPiece()
    {
        currentPiece = pieces[random.Next(pieces.Length)];
        position = (width / 2, 0);
        if (!CanMove(0, 0)) gameOver = true;
    }
    
    static bool CanMove(int dx, int dy)
    {
        foreach (var (px, py) in currentPiece)
        {
            int x = position.x + px + dx;
            int y = position.y + py + dy;
            if (x < 0 || x >= width || y < 0 || y >= height || (y >= 0 && Grid.NewGrid[y, x] != 0))
                return false;
        }
        return true;
    }

    static void PlacePiece()
    {
        foreach (var (px, py) in currentPiece)
        {
            int x = position.x + px;
            int y = position.y + py;
            if (y >= 0) Grid.NewGrid[y, x] = 1;
        }
    }
    
    public static void MovePiece(int dx, int dy)
    {
        if (CanMove(dx, dy))
        {
            position.x += dx;
            position.y += dy;
        }
        else if (dy > 0)
        {
            PlacePiece();
            Grid.ClearFullRows();
            NewPiece();
        }
    }
    
    public static void DrawBoard()
    {
        Console.SetCursorPosition(0, 0);
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                bool isPiece = false;
                foreach (var (px, py) in currentPiece)
                {
                    if (position.x + px == x && position.y + py == y)
                    {
                        isPiece = true;
                        break;
                    }
                }
                Console.Write(isPiece || Grid.NewGrid[y, x] != 0 ? "B" : ".");
            }
            Console.WriteLine();
        }
    }
    
    public static void ReadInput()
    {
        while (!gameOver)
        {
            var key = Console.ReadKey(true).Key;
            if (key == ConsoleKey.LeftArrow) MovePiece(-1, 0);
            if (key == ConsoleKey.RightArrow) MovePiece(1, 0);
            if (key == ConsoleKey.DownArrow) MovePiece(0, 1);
            if (key == ConsoleKey.UpArrow) MovePiece(0, -1);
        }
    }
}