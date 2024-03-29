using ConsoleRogueLike.Entities;
using ConsoleRogueLike.Core.Interface;
using System;

namespace ConsoleRogueLike.Core
{
    public class GameObjectFactory
    {
       
        public delegate void GameObjectCreatedHandler(GameObject gameObject);
        public event GameObjectCreatedHandler OnGameObjectCreated = delegate { };

        public Player CreatePlayer(Vector point, IGameScene sceneReader)
        {
            var player = new Player('P', point, 50, sceneReader);
            OnGameObjectCreated?.Invoke(player);
            return player;
        }

        public void CreateMelee(Vector point, IGameScene sceneReader, Random random)
        {
            var melee = new Melee('M', point, 100, sceneReader, random);
            OnGameObjectCreated?.Invoke(melee);
        }

        public void CreateWall(Vector point)
        {
            var wall = new GameObject('#', point);
            OnGameObjectCreated?.Invoke(wall);
        }

        public void CreateExit(Vector point, IGameScene sceneReader)
        {
            var exit = new Exit('E', point, sceneReader);
            OnGameObjectCreated?.Invoke(exit);
        }
    }
}
