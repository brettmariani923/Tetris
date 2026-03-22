using Tetris.GameGrid;
using Tetris.Inputs;
using Tetris.TetrominoFactory;
using Tetris.TetrominoFactory.Tetromino.Behavior;
using Tetris.TetrominoFactory.Variables;

namespace Tetris
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.CursorVisible = false;
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Music.PlayBackgroundMusic();

            var grid = new GridClearing();
            var config = new BoardConfiguration();
            var activeState = new ActiveState();

            var createRandom = new CreateRandom(activeState, config);
            var placement = new Placement(activeState, grid);
            var clearing = new Clearing(grid, config);
            var movement = new Movement(activeState, config, clearing, grid, placement, createRandom);
            var rotation = new Rotation(activeState, config, grid);

            var pauseScreen = new PauseScreen(grid);
            var pause = new Pause(pauseScreen);
            var playerAction = new PlayerAction(movement, rotation, pause);

            var drawBoard = new DrawBoard(grid, movement);
            var gameOver = new GameOver(grid, config);

            createRandom.NewPiece();

            Thread inputThread = new Thread(() =>
            {
                while (true)
                {
                    playerAction.ReadInput();
                }
            });

            inputThread.IsBackground = true;
            inputThread.Start();

            while (!gameOver.IsGameOver())
            {
                if (!pause.IsPaused)
                {
                    drawBoard.Draw();
                    Thread.Sleep(Math.Max(80, 400 - (grid.Score * 10)));
                    movement.MovePiece(0, 1);
                }
                else
                {
                    Thread.Sleep(100);
                }
            }

            Console.Clear();
            Console.WriteLine("G A M E  O V E R");
            Console.WriteLine($"Score: {grid.Score}");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("www.youtube.com/watch?v=sDipbctxGC4");
            Console.WriteLine("        .\r\n       -.\\_.--._.______.-------.___.---------.___\r\n       )`.                                       `-._\r\n      (                                              `---.\r\n      /o                                                  `.\r\n     (                                                      \\\r\n   _.'`.  _                                                  L\r\n   .'/| \"\" \"\"\"\"._                                            |\r\n      |          \\             |                             J\r\n                  \\-._          \\                             L\r\n                  /   `-.        \\                            J\r\n                 /      /`-.      )_                           `\r\n                /    .-'    `    J  \"\"\"\"-----.`-._             |\\            \r\n              .'   .'        L   F            `-. `-.___        \\`.\r\n           ._/   .'          )  )                `-    .'\"\"\"\"`.  \\)\r\n__________((  _.'__       .-'  J              _.-'   .'        `. \\\r\n                   \"\"\"\"\"\"\"((  .'--.__________(   _.-'___________)..|----------------._____\r\n                            \"\"                \"\"\"               ``U'\r\n");
            Console.WriteLine("Brett Mariani 2025");
            Console.ReadLine();
        }
    }
}