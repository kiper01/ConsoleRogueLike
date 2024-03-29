using ConsoleRogueLike.Entities;

namespace ConsoleRogueLike.Core
{
    public class MazeGenerator
    {
        private readonly GameObjectFactory _factory;
        private readonly Random _random;
        private readonly Vector _area;
        private readonly int[,] _maze;

        public MazeGenerator(GameObjectFactory factory, Vector area, Random random)
        {
            _factory = factory;
            _area = area;
            _maze = new int[area.X, area.Y];
            _random = random;
        }

        public void GenerateMaze()
        {
            InitializeMaze();
            PrimAlgorithm();
            AddWallsToScene();
        }

        private void InitializeMaze()
        {
            for (int x = 0; x < _area.X; x++)
                for (int y = 0; y < _area.Y; y++)
                    _maze[x, y] = 1;
        }

        private void PrimAlgorithm()
        {
            var walls = new List<Vector>();

            int startX = _random.Next(1, _area.X - 1);
            int startY = _random.Next(1, _area.Y - 1);
            _maze[startX, startY] = 0;

            AddWallsToList(startX, startY, walls);

            while (walls.Count > 0)
            {
                int randomIndex = _random.Next(0, walls.Count);
                Vector wall = walls[randomIndex];
                walls.RemoveAt(randomIndex);

                int neighbors = CountNeighbors(wall.X, wall.Y);
                if (neighbors == 1)
                {
                    _maze[wall.X, wall.Y] = 0;

                    AddWallsToList(wall.X, wall.Y, walls);
                }
            }
        }

        private int CountNeighbors(int x, int y)
        {
            int count = 0;
            if (_maze[x - 1, y] == 0) count++;
            if (_maze[x + 1, y] == 0) count++;
            if (_maze[x, y - 1] == 0) count++;
            if (_maze[x, y + 1] == 0) count++;
            return count;
        }

        private void AddWallsToList(int x, int y, List<Vector> walls)
        {
            if (x - 1 > 0 && _maze[x - 1, y] == 1) walls.Add(new Vector(x - 1, y));
            if (x + 1 < _area.X - 1 && _maze[x + 1, y] == 1) walls.Add(new Vector(x + 1, y));
            if (y - 1 > 0 && _maze[x, y - 1] == 1) walls.Add(new Vector(x, y - 1));
            if (y + 1 < _area.Y - 1 && _maze[x, y + 1] == 1) walls.Add(new Vector(x, y + 1));
        }

        private void AddWallsToScene()
        {
            for (int y = 0; y < _area.Y; y++)
            {
                for (int x = 0; x < _area.X; x++)
                {
                    if (_maze[x, y] == 1)
                        _factory.CreateWall(new Vector(x, y));
                }
            }
        }
    }
}
