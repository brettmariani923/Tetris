public class Grid
{
    public static int Width;
    public static int Height;
    public int[,] grid = new int[Width, Height];
    //declaring variables and a multidimensional grid to create the gameboard
    //variables will be 10 and 20 respectively, but want to ask to make sure
    //defining them upfront wont cause issues later when I need to incorporate
    //pieces

    public int this[int x, int y]
    {
        get => grid[x, y];
        set => grid[x, y] = value;
    }
    // nask if she thinks there is a better way to use a different way to iterate through the
    // values for what I'm trying to do.
    // Like if I should use a foreach or something else
    
}