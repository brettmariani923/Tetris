namespace Tetris.TetrominoFactory.Tetromino.Type
{
    public class Shapes
    {
        public static (int x, int y)[][] piecesPool = new (int, int)[][]
        {
            new [] { (0,0), (1,0), (0,1), (1,1) },
            new [] { (0,0), (-1,0), (1,0), (2,0) },
            new [] { (0,0), (-1,0), (1,0), (0,1) },
            new [] { (0,0), (-1,0), (1,0), (1,1) },
            new [] { (0,0), (-1,0), (1,0), (-1,1) },
            new [] { (0,0), (-1,0), (1,0), (0,1) }
        };
    }
}