namespace Tetris;

public abstract class assignments
{
    public abstract Position WhichSpaces { get; } //covers spaces the block will 
    //be in on the grid while in play
    
    public abstract Position StartingPoint { get; }
    //define where we want to blocks to start falling from
    
    public abstract int WhichBlock { get; }
    //Prolly will use random here. make a list or something for tetrominos then use next()
    //to pick at random
}