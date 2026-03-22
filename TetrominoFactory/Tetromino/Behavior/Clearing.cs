using Tetris.TetrominoFactory.Variables;

namespace Tetris.TetrominoFactory.Tetromino.Behavior
{
    public class Clearing
    {
        private readonly GridClearing grid;
        private readonly BoardConfiguration config;

        public Clearing(GridClearing grid, BoardConfiguration config)
        {
            this.grid = grid;
            this.config = config;
        }

        public bool IsRowFull(int row)
        {
            for (int col = 0; col < config.Width; col++)
            {
                if (grid.NewGrid[row, col] == 0)
                {
                    return false;
                }
            }
            return true;
        }

        public void ClearRow(int row)
        {
            if (IsRowFull(row))
            {
                for (int col = 0; col < config.Width; col++)
                {
                    grid.NewGrid[row, col] = 0;
                }
                grid.Score++;
            }
        }

        public void RowDown(int row, int offset)
        {
            for (int col = 0; col < config.Width; col++)
            {
                grid.NewGrid[row + offset, col] = grid.NewGrid[row, col];
                grid.NewGrid[row, col] = 0;
            }
        }

        public int ClearFullRows()
        {
            int clear = 0;

            for (int row = config.Height - 1; row >= 0; row--)
            {
                if (IsRowFull(row))
                {
                    ClearRow(row);
                    clear++;
                }
                else if (clear > 0)
                {
                    RowDown(row, clear);
                }
            }

            return clear;
        }
    }
}