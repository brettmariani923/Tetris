using Tetris.TetrominoFactory.Tetromino.Behavior;
using Tetris.TetrominoFactory.Variables;

namespace Tetris.GameGrid
{
    public class DrawBoard
    {
        private readonly GridClearing grid;
        private readonly Movement draw;

        public DrawBoard(GridClearing grid, Movement draw)
        {
            this.grid = grid;
            this.draw = draw;
        }
        public static void DrawBorder()
        {
            int boardWidth = 10;
            int boardHeight = 20;

            int startX = 0;
            int startY = 0;

            Console.ForegroundColor = ConsoleColor.White;

            Console.SetCursorPosition(startX, startY);
            Console.Write("╔" + new string('═', boardWidth) + "╗");

            for (int i = 0; i < boardHeight; i++)
            {
                Console.SetCursorPosition(startX, startY + i + 1);
                Console.Write("║");
                Console.SetCursorPosition(startX + boardWidth + 1, startY + i + 1);
                Console.Write("║");
            }

            Console.SetCursorPosition(startX, startY + boardHeight + 1);
            Console.Write("╚" + new string('═', boardWidth) + "╝");

            Console.ResetColor();
        }

        public void Draw()
        {
            System.Console.OutputEncoding = System.Text.Encoding.UTF8;

            DrawBorder();
            Console.SetCursorPosition(0, 0);
            draw.DrawCurrentFrame();

            int textX = 10 + 4;
            int textY = 2;
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine($"  Score: {grid.Score}");
            Console.WriteLine("    _._     _,-'\"\"`-._\r\n     (,-.`._,'(       |\\`-/|\r\n         `-.-' \\ )-`( , o o)\r\n             `-    \\`_`\"'-");

            Console.SetCursorPosition(textX, textY); if (grid.Score > 10)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Nice!");
            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(textX, textY + 2); if (grid.Score > 20)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Great job!");
            }
        ;
            Console.ForegroundColor = ConsoleColor.White;

            Console.SetCursorPosition(textX, textY + 4); if (grid.Score > 30)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Excellent!");
            }
            Console.ForegroundColor = ConsoleColor.White;

            Console.SetCursorPosition(textX, textY + 6); if (grid.Score > 40)
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("Fantastic!");
            }
            Console.ForegroundColor = ConsoleColor.White;

            Console.SetCursorPosition(textX, textY + 8); if (grid.Score > 50)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Increrdible!!");
            }
            Console.ForegroundColor = ConsoleColor.White;

            Console.SetCursorPosition(textX, textY + 10); if (grid.Score > 60)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Out of this world!!");
            }
            Console.ForegroundColor = ConsoleColor.White;

            Console.SetCursorPosition(textX, textY + 12); if (grid.Score > 70)
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("Dino-mite!");
            }
            Console.SetCursorPosition(textX, textY + 14); if (grid.Score > 80)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("🦕");
            }
            Console.SetCursorPosition(textX + 2, textY + 14); if (grid.Score > 90)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("🦕");
            }
            Console.SetCursorPosition(textX + 4, textY + 14); if (grid.Score > 100)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("🦕");
            }
            Console.SetCursorPosition(textX, textY + 16); if (grid.Score > 110)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("🦕");
            }
            Console.SetCursorPosition(textX + 2, textY + 16); if (grid.Score > 120)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("🦕");
            }
            Console.SetCursorPosition(textX + 4, textY + 16); if (grid.Score > 130)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("🦕");
            }

            Console.SetCursorPosition(textX, textY + 18); if (grid.Score > 170)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("You're Unstoppable! 🦖");
            }

            Console.ForegroundColor = ConsoleColor.White;

        }


    }
}
