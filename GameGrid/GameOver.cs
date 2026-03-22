using Tetris.TetrominoFactory.Variables;

namespace Tetris.GameGrid
{
    public class GameOver
    {
        private readonly GridClearing grid;
        private readonly BoardConfiguration config;

        public GameOver(GridClearing grid, BoardConfiguration config)
        {
            this.grid = grid;
            this.config = config;
        }

        public bool IsGameOver()
        {
            for (int col = 0; col < config.Width; col++)
            {
                if (grid.NewGrid[0, col] != 0)
                {
                    return true;
                }
            }
            return false;
        }
    }
}