using Tetris.TetrominoFactory.Variables;

namespace Tetris.GameGrid
{
    public class PauseScreen
    {
        private readonly GridClearing grid;

        public PauseScreen(GridClearing grid)
        {
            this.grid = grid;
        }

        public void DrawPauseScreen()
        {
            Console.Clear();
            Console.WriteLine("           P A U S E ");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("======== TETRIS CONTROLS ========");
            Console.WriteLine("⬅  Left Arrow      - Move left ");
            Console.WriteLine("🠮 Right Arrow      - Move right ");
            Console.WriteLine("⬇  Down Arrow      - Soft drop  ");
            Console.WriteLine("⬆  Up Arrow        - Rotate piece  ");
            Console.WriteLine("Spacebar           - P A U S E");
            Console.WriteLine("=================================");
            Console.WriteLine(". . . . . . .");
            Console.WriteLine();
            Console.WriteLine($"  Score: {grid.Score}");
            Console.WriteLine("    _._     _,-'\"\"`-._\r\n     (,-.`._,'(       |\\`-/|\r\n         `-.-' \\ )-`( , o o)\r\n             `-    \\`_`\"'-");
        }
    }
}