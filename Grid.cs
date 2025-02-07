using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Tetris;

public class Grid : BlockPhases
{
    public static int Width;
    public static int Height;
    public static int[,] NewGrid = new int[Width, Height];
    static Random random = new Random();
    public Position? StartPoint;
    public int NewPiece;

    public int this[int w, int h]
    {
        get => NewGrid[w, h];
        set => NewGrid[w, h] = value;
    }

    public Position Start
    {
        get => new (20, 10 / 2);
        set => StartPoint = value;
    }
    
    public int WhichBlock
    {
        get => random.Next(Tetrominos.pieces.Length);
        set => NewPiece = value;
    }

    public bool IsRowFull(int w)
    {
        for (int h = 0; h < Width; h++)
        {
            if (NewGrid[w, h] == 0)
            {
                return false;
            }
        }
        return true;
    }

    public bool IsRowEmpty(int w)
    {
        for (int h = 0; h < Width; h++)
        {
            if (NewGrid[w, h] != 0)
            {
                return false;
            }
        }
        return true;
    }

    public void ClearRow(int w)
    {
        if (IsRowFull(w) == true)
        {
             (NewGrid[w, h] = 0;)
        }
    }
    
}