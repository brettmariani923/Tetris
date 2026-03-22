using Tetris.GameGrid;

namespace Tetris.Inputs
{
    public class Pause
    {
        private readonly PauseScreen render;

        public bool IsPaused { get; private set; }

        public Pause(PauseScreen render)
        {
            this.render = render;
        }

        public void PauseLoop()
        {
            IsPaused = true;
            render.DrawPauseScreen();

            while (IsPaused)
            {
                var key = Console.ReadKey(true).Key;

                if (key == ConsoleKey.Spacebar)
                {
                    IsPaused = false;
                    Console.Clear();
                }
            }
        }
    }
}