namespace Tetris;

public abstract class BlockAssignments
{
    public abstract Position WhichSpaces { get; } //covers spaces the block will 
    //be in while on the grid in play
    
    public abstract Position StartingPoint { get; }
    //define where we want to blocks to start falling from
    
    public abstract int WhichBlock { get; }
    //Prolly will use random here. make a list or something for tetrominos then use next()
    //to pick at random
}