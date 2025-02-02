namespace Tetris;

public abstract class BlockOrientation
{
    public abstract Position Start { get; }
    //define where we want to blocks to start falling from

    public abstract Position[][] CurrentSpace { get; } //making a jagged array
    // an array of arrays (each inner array can be a different size, use this for
    // the tetris pieces on game grid)
    //will cover the space a block will move in while on the game grid
    
    public abstract int WhichBlock { get; set; }
    //Prolly will use random here. make a list or something for tetrominos then use
    //Random.Next() to pick at random

    public Position StartPoint;
    

    public BlockOrientation()
    {
         StartPoint = new Position(Start.Height, Start.Width );
    }
    //making a method so to start the blocks in the right position
    
    
}