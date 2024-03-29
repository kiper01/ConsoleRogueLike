using ConsoleRogueLike.Entities;
using ConsoleRogueLike.Core.Interface;
using System.Drawing;

namespace ConsoleRogueLike.Core
{
    public class UnitSpawner
    {
        private readonly GameObjectFactory _factory;
        private readonly IGameScene _sceneReader;
        private readonly Random _random;

        public UnitSpawner(GameObjectFactory factory, IGameScene sceneReader, Random random) => (_factory, _sceneReader, _random) = (factory, sceneReader, random);

        public void SpawnUnits(int numMelees)
        {
            SpawnMelees(numMelees);
            SpawnExit();
        }

        private void SpawnMelees(int numMelees)
        {
            for (int i = numMelees; i != 0;)
            {
                int x = _random.Next(_sceneReader.Width);
                int y = _random.Next(_sceneReader.Height);
                Vector point = new Vector(x, y);

                if (_sceneReader.IsPointFree(point))
                {
                    _factory.CreateMelee(point, _sceneReader, _random);
                    --i;
                }
            }
        }

        private void SpawnExit()
        {
            for (int i = 1; i != 0;)
            {
                int x = _random.Next(_sceneReader.Width);
                int y = _random.Next(_sceneReader.Height);
                Vector point = new Vector(x, y);

                if (_sceneReader.IsPointFree(point))
                {
                    _factory.CreateExit(point, _sceneReader);
                    --i;
                }
            }
        }
    }
}
