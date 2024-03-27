using System.Text;
using ConsoleRogueLike.Entities;
using ConsoleRogueLike.Core.Interface;

namespace ConsoleRogueLike.Core
{
    public class GameRenderer
    {
        private static char[,]? lastDisplay;

        public static void Render(IGameScene scene)
        {
            Console.CursorVisible = false;

            char[,] display = new char[scene.Height, scene.Width];

            for (int y = 0; y < scene.Height; y++)
            {
                for (int x = 0; x < scene.Width; x++)
                {
                    display[y, x] = ' ';
                }
            }

            int playerHealth = 0;

            foreach (var gameObject in scene.GameObjects)
            {
                if (gameObject != null)
                {
                    display[gameObject.Point.Y, gameObject.Point.X] = gameObject.Symbol;

                    if (gameObject is Player player)
                    {
                        playerHealth = player.Health;
                    }
                }
            }

            for (int y = 0; y < scene.Height; y++)
            {
                for (int x = 0; x < scene.Width; x++)
                {
                    if (lastDisplay == null || display[y, x] != lastDisplay[y, x])
                    {
                        Console.SetCursorPosition(x, y);
                        Console.Write(display[y, x]);
                    }
                }
            }

            lastDisplay = (char[,])display.Clone();

            Console.SetCursorPosition(0, scene.Height);
            Console.Write($"Health: {playerHealth}");
        }
    }
}
