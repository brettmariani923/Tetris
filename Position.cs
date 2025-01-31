namespace Tetris
{
    public class Position 
    {
        public int Height { get; set; }
    
        public int Width { get; set; }

        public Position(int height, int width)
        {
            Height = height;
            Width = width;
        } 
        //This is going to help determine things like where the block starts, where it
        //is in game, etc.
    }
}

