using Tetris.TetrominoFactory.Variables;

namespace Tetris.TetrominoFactory.Tetromino.Behavior
{
    public class Rotation
    {
        private readonly ActiveState activeState;
        private readonly BoardConfiguration configuration;
        private readonly GridClearing grid;

        public Rotation(ActiveState activeState, BoardConfiguration configuration, GridClearing grid)
        {
            this.activeState = activeState;
            this.configuration = configuration;
            this.grid = grid;
        }

        public bool CanRotate((int x, int y)[] rotatedPiece)
        {
            foreach (var (px, py) in rotatedPiece)
            {
                int x = activeState.PositionOfPiece.x + px;
                int y = activeState.PositionOfPiece.y + py;

                if (x < 0 || x >= configuration.Width || y < 0 || y >= configuration.Height || (y >= 0 && grid.NewGrid[y, x] != 0))
                    return false;
            }

            return true;
        }

        public void RotatePiece()
        {
            var rotatedPiece = activeState.CurrentPiece
                .Select(cell => (-cell.y, cell.x))
                .ToArray();

            if (CanRotate(rotatedPiece))
                activeState.CurrentPiece = rotatedPiece;
        }
    }
}