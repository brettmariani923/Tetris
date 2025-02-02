namespace Tetris;

public class Tetrominos
{
    //would LINQ work for movements? for multi dimensonal arrays
    //should I add these all to a list, and then randomly select from the list to generate
    //pieces?
    //could use a switch statement to determine 
    
    public int[,] TheBlarg = new int[3, 3]
    {
        { 1, 0, 1 },
        { 0, 1, 1 },
        { 1, 1, 0 },
    };
    public int[,] Square = new int[2, 2]
    {
        {1, 1 },
        {1, 1 }
    };

    public int[,] Tblock = new int[3, 3]
    {
        { 0, 0, 0 },
        { 0, 1, 0 },
        { 1, 1, 1 }
    };

    public int[,] Long = new int[4, 4]
    {
        { 1, 0, 0, 0 },
        { 1, 0, 0, 0 },
        { 1, 0, 0, 0 },
        { 1, 0, 0, 0 }
    };

    public int[,] Sblock = new int[3, 3]
    {
        { 0, 0, 0},
        { 0, 1, 1},
        { 1, 1, 0}
    };

    public int[,] Zblock = new int[3, 3]
    {
        { 0, 0, 0 },
        { 1, 1, 0 },
        { 0, 1, 1 }
    };

    public int[,] Jblock = new int[3, 3]
    {
        { 0, 0, 0 },
        { 1, 0, 0 },
        { 1, 1, 1 }
    };

    public int[,] Lblock = new int[3, 3]
    {
        { 0, 0, 0 },
        { 0, 0, 1 },
        { 1, 1, 1 }
    };

}