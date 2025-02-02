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
        //Height and width holder, this is going to be necessary for multiple dimensions
        //This is going to help determine where the block starts, where it
        //is in game, much more. 
    }
}

