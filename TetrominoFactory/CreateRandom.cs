using Tetris.TetrominoFactory.Variables;
using static Tetris.TetrominoFactory.Tetromino.Type.Shapes;

namespace Tetris.TetrominoFactory
{
    public class CreateRandom
    {
        private readonly ActiveState activeState;
        private readonly BoardConfiguration configuration;
        private readonly Random random;

        public CreateRandom(ActiveState activeState, BoardConfiguration configuration)
        {
            this.activeState = activeState;
            this.configuration = configuration;
            random = new Random();
        }

        public void NewPiece()
        {
            activeState.CurrentPieceIndex = random.Next(piecesPool.Length);
            activeState.CurrentPiece = piecesPool[activeState.CurrentPieceIndex];
            activeState.PositionOfPiece = (configuration.Width / 2, 0);
        }
    }
}