using Tetris.TetrominoFactory.Variables;

using static Tetris.TetrominoFactory.Tetromino.Type.Colors;

namespace Tetris.TetrominoFactory.Tetromino.Behavior
{
    public class Movement
    {
        private readonly ActiveState activeState;
        private readonly BoardConfiguration configuration;
        private readonly Clearing clearing;
        private readonly GridClearing check;
        private readonly Placement set;
        private readonly CreateRandom random;

        public Movement(
            ActiveState activeState,
            BoardConfiguration configuration,
            Clearing clearing,
            GridClearing check,
            Placement set,
            CreateRandom random)
        {
            this.activeState = activeState;
            this.configuration = configuration;
            this.clearing = clearing;
            this.check = check;
            this.set = set;
            this.random = random;
        }

        public bool CanMove(int dx, int dy)
        {
            foreach (var (px, py) in activeState.CurrentPiece)
            {
                int x = activeState.PositionOfPiece.x + px + dx;
                int y = activeState.PositionOfPiece.y + py + dy;

                if (x < 0 || x >= configuration.Width || y < 0 || y >= configuration.Height || (y >= 0 && check.NewGrid[y, x] != 0))
                    return false;
            }

            return true;
        }

        public void MovePiece(int dx, int dy)
        {
            if (CanMove(dx, dy))
            {
                activeState.PositionOfPiece =
                (
                    activeState.PositionOfPiece.x + dx,
                    activeState.PositionOfPiece.y + dy
                );
            }
            else if (dy > 0)
            {
                set.PlacePiece();
                clearing.ClearFullRows();
                random.NewPiece();
            }
        }

        public void DrawCurrentFrame()
        {
            for (int y = 0; y < configuration.Height; y++)
            {
                Console.SetCursorPosition(1, y + 1);

                for (int x = 0; x < configuration.Width; x++)
                {
                    bool isPiece = false;
                    ConsoleColor color = ConsoleColor.White;

                    foreach (var (px, py) in activeState.CurrentPiece)
                    {
                        if (activeState.PositionOfPiece.x + px == x && activeState.PositionOfPiece.y + py == y)
                        {
                            isPiece = true;
                            color = pieceColors[activeState.CurrentPieceIndex];
                            break;
                        }
                    }

                    if (!isPiece && check.NewGrid[y, x] != 0)
                        color = check.ColorGrid[y, x];

                    Console.ForegroundColor = color;
                    Console.Write(isPiece || check.NewGrid[y, x] != 0 ? "◯" : ".");
                }

                Console.WriteLine();
            }
        }
    }
}
