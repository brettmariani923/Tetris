namespace Tetris;

public class Tetrominos
{
     public static (int x, int y)[][] pieces = new (int, int)[][]
    {
        //for anyone reading this who might not understand right away,
        //the coordinate describe where the blocks are going from a 
        //center point (0,0) based on x, y coordinates
        //so for example in the square piece, (0,0) is the first block
        //(1,0) is describing one block to the right on the x axis
        //(0,1) is describing one block directly below on the y axis
        //(1,1) describes a block one to the right and one down from it
        
        new [] { (0,0), (1,0), (0,1), (1,1) }, //square
        
        new [] { (0,0), (-1,0), (1,0), (2,0) }, //long
        
        new [] { (0,0), (-1,0), (1,0), (0,1) }, //t piece
        
        new [] { (0,0), (-1,0), (1,0), (1,1) }, //j block
        
        new [] { (0,0), (-1,0), (1,0), (-1,1) }, //l block
        
        new[] { (0,0), (-1, -1), (1, 0), (2, 2) }, //The blarg (supposed to make you say "blarg" in anger)
        
        new[] { (0,0), (-1,0), (-2,0), (-3, 0), (0, 1), (0, 2), (0, 3) } //The trolls bridge
    };
        
 

    
}