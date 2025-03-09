using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Timers;

namespace Tetris;

public class Grid 
{
    public static int Width = 20;
    public static int Height = 10;
    public static int[,] NewGrid = new int[Width, Height];
    public static ConsoleColor[,] ColorGrid = new ConsoleColor[20, 10];
    static Random random = new Random();
    public Position? StartPoint;
    public int NewPiece;
    private static int x;
    public static int score = 0;


    public int this[int w, int h]               //this was based off of something i found online to help me out with how I would make it.
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
        get => random.Next(Tetrominos.piecesPool.Length);
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
    
   

    public static bool IsRowEmpty(int w)
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
            
            score++;
            
        }
    }




    public static void ClearBoard()
    {
        for (int h = 0; h < Width; h++)
        {
            for (int w = 0; w < Height; w++)
            {
                NewGrid[h, w] = 0;
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

    public static bool IsGameOver()                             //Added a check
    {                                                          
        for (int h = 0; h < Height; h++)                         // doesn't trigger early
        {
            if (NewGrid[0, h] != 0)
            {
                return true;
            }
        }
        return false;
    }

  
}