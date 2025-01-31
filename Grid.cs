public class Grid
{
    public static int width = 10;
    public static int height = 20;
    public static int[,] grid = new int[height, width];

    public static void CreateGrid()
    {
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                Console.Write(grid[y, x] + " ");
            }
            Console.WriteLine();
        }
        //need to find a way to index parts of the grid 
    }
}