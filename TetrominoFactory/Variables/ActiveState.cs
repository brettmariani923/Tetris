namespace Tetris.TetrominoFactory.Variables
{
    public class ActiveState
    {
        public (int x, int y)[] CurrentPiece { get; set; }
        public (int x, int y) PositionOfPiece { get; set; }
        public int CurrentPieceIndex { get; set; }
    }
}