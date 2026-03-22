using Tetris.TetrominoFactory.Tetromino.Behavior;

namespace Tetris.Inputs
{
    public class PlayerAction
    {
        private readonly Movement move;
        private readonly Rotation rotation;
        private readonly Pause start;

        public PlayerAction(Movement move, Rotation rotation, Pause start)
        {
            this.move = move;
            this.rotation = rotation;
            this.start = start;
        }

        public void ReadInput()
        {
            var key = Console.ReadKey(true).Key;

            if (key == ConsoleKey.LeftArrow) move.MovePiece(-1, 0);
            if (key == ConsoleKey.RightArrow) move.MovePiece(1, 0);
            if (key == ConsoleKey.DownArrow) move.MovePiece(0, 1);
            if (key == ConsoleKey.UpArrow) rotation.RotatePiece();
            if (key == ConsoleKey.Spacebar) start.PauseLoop();
            
        }
    }
}