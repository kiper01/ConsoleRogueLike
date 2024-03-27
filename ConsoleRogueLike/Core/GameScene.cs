using ConsoleRogueLike.Entities;
using ConsoleRogueLike.Core.Interface;

namespace ConsoleRogueLike.Core
{
    public class GameScene : IGameScene
    {
        private readonly List<GameObject> _gameObjects = new();
        public IReadOnlyList<GameObject> GameObjects => _gameObjects;

        public int Width { get; private set; }
        public int Height { get; private set; }

        private readonly GameObjectFactory _factory;
        private readonly Random _random;

        public GameScene(int width, int height, GameObjectFactory factory, Random random)
        {
            Width = width;
            Height = height;
            _gameObjects = new List<GameObject>(width * height);
            _random = random;
            _factory = factory;

            _factory.OnGameObjectCreated += AddGameObject;

            Init();
        }

        private void Init()
        {
            _gameObjects.Clear();

            MazeGenerator generator = new(_factory, Width, Height, _random);
            UnitSpawner generator2 = new(_factory, this, _random);

            generator.GenerateMaze();
            generator2.SpawnUnits(3);
        }

        public void Finished()
        {
            var player = _gameObjects.OfType<Player>().FirstOrDefault();
            
            Init();

            if (player != null)
            {
                player.SetStartPoint();
                player.AddHealth(10);
                
                _gameObjects.Add(player);
            }
        }

        public void AddGameObject(GameObject gameObject)
        {
            if (GameObjects.Any(obj => obj != null && obj.Point.X == gameObject.Point.X && obj.Point.Y == gameObject.Point.Y))
            {
                throw new InvalidOperationException("point is already occupied.");
            }

            _gameObjects.Add(gameObject);
        }

        public void RemoveGameObject(GameObject gameObject)
        {
            _gameObjects.Remove(gameObject);
        }

        public bool IsPointFree(Vector point)
        {
            return !_gameObjects.Any(gameObject => gameObject.Point.Equals(point));
        }

        public List<GameObject> GetObjectsAround(Vector point)
        {
            var offsets = new List<Vector>
            {
                new Vector(0, -1),
                new Vector(0, 1), 
                new Vector(-1, 0),
                new Vector(1, 0)
            };

            return offsets
                .Select(offset => point + offset)
                .Where(pos => pos.X >= 0 && pos.X < Width && pos.Y >= 0 && pos.Y < Height)
                .SelectMany(pos => _gameObjects.Where(obj => obj.Point.Equals(pos)))
                .ToList();
        }

        public GameObject? GetObjectAtPoint(Vector point)
        {
            return _gameObjects.FirstOrDefault(obj => obj.Point.Equals(point));
        }

        public void GameOver()
        {
            _gameObjects.Clear();
            Console.WriteLine("Game Over");
        }
    }
}
