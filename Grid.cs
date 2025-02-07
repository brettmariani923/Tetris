using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Tetris;

public class Grid : BlockPhases
{
    public static int Width = 20;
    public static int Height = 10;
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
        get => new (Width = 10 / 2, Height = 20);
        set => StartPoint = value;
    }
    
    public int WhichBlock
    {
        get => random.Next(Tetrominos.pieces.Length);
        set => NewPiece = value;
    }

    

    public bool IsCellEmpty(int w, int h)
    {
        return NewGrid[w, h] == 0;  //w, h will go through cells checking for empties 
    }
    
    public static bool IsRowFull(int w)
    {
        for (int h = 0; h < Height; h++)    //using incrementation to check if rows are full
        {
            if (NewGrid[w, h] == 0)     //if cell is empty row isnt full
            {
                return false;
            }
        }
        return true;
    }
    
    public bool IsRowEmpty(int w)
    {
        for (int h = 0; h < Height; h++)
        {
            if (NewGrid[w, h] != 0)         //if cell isnt empty row is full
            {
                return false;
            }
        }
        return true;
    }

    public static void ClearRow(int w)
    {
        if (IsRowFull(w))
        {
            for (int h = 0; h < Height; h++)
            {
                NewGrid[w, h] = 0;
            }
            
        }
    }

    public static void RowDown(int w, int rowNumber)
    {
        for (int h = 0; h < Height; h++)
        {
            NewGrid[w + rowNumber, h] = NewGrid[w, h];
            NewGrid[w, h] = 0;
        }
    }

    public static int ClearFullRows()
    {
        int clear = 0;

        for (int w = Width - 1; w >= 0; w--)
        {
            if (IsRowFull(w))
            {
                ClearRow(w);
                clear++;
            }
            else if (clear > 0)
            {
                RowDown(w, clear);
            }
        }
        return clear;
    }
    
}