using ConsoleRogueLike.Core.Interface;
using ConsoleRogueLike.Entities;

namespace ConsoleRogueLike.Core
{
    public class GameLoop
    {
        private readonly GameScene _scene;
        private readonly Player _player;

        public GameLoop(GameScene scene, Player player) => (_scene, _player) = (scene, player);

        public void Run()
        {
            bool isRunning = true;

            GameRenderer.Render(_scene);

            while (isRunning)
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo keyInfo = Console.ReadKey(intercept: true);

                    if (keyInfo.Key == ConsoleKey.Escape)
                    {
                        isRunning = false;
                    }

                    Vector direction = GetDirectionFromKey(keyInfo.Key);
                    if (!(direction.X == 0 && direction.Y ==0))
                    {
                        _player.Move(direction);
                    }

                    Update(ref isRunning, _scene);

                    GameRenderer.Render(_scene);
                }
            }
        }

        private Vector GetDirectionFromKey(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.W:
                    return new Vector(0, -1);
                case ConsoleKey.S:
                    return new Vector(0, 1);
                case ConsoleKey.A:
                    return new Vector(-1, 0);
                case ConsoleKey.D:
                    return new Vector(1, 0);
                default:
                    return new Vector(0, 0);
            }
        }

        public static void Update(ref bool isRunning, IGameScene scene)
        {
            var gameObjectsCopy = new List<GameObject>(scene.GameObjects);

            foreach (var gameObject in gameObjectsCopy)
            {
                gameObject.Update();

                if (gameObject is GameEnemy enemy && enemy.Health < 1)
                {
                    scene.RemoveGameObject(enemy);
                }

                if (gameObject is Player player && player.Health < 1)
                {
                    isRunning = false;
                }

                if (gameObject is Player && gameObject.Point.X == scene.Area.X - 2 && gameObject.Point.Y == scene.Area.Y- 2)
                {
                    scene.AtFinish();
                }
            }
        }
    }
}
