namespace Tetris.TetrominoFactory.Variables
{
    public class GridClearing
    {
        public const int Width = 10;
        public const int Height = 20;

        public int[,] NewGrid = new int[Height, Width];
        public ConsoleColor[,] ColorGrid = new ConsoleColor[Height, Width];

        public int Score = 0;
    }
}