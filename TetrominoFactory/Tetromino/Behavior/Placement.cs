using Tetris.TetrominoFactory.Variables;
using static Tetris.TetrominoFactory.Tetromino.Type.Colors;

namespace Tetris.TetrominoFactory.Tetromino.Behavior
{
    public class Placement
    {
        private readonly ActiveState activeState;
        private readonly GridClearing grid;

        public Placement(ActiveState activeState, GridClearing grid)
        {
            this.activeState = activeState;
            this.grid = grid;
        }

        public void PlacePiece()
        {
            ConsoleColor pieceColor = pieceColors[activeState.CurrentPieceIndex];

            foreach (var (px, py) in activeState.CurrentPiece)
            {
                int x = activeState.PositionOfPiece.x + px;
                int y = activeState.PositionOfPiece.y + py;

                if (y >= 0)
                {
                    grid.NewGrid[y, x] = 1;
                    grid.ColorGrid[y, x] = pieceColor;
                }
            }
        }
    }
}
