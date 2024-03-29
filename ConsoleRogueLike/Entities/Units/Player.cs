using ConsoleRogueLike.Core.Interface;
using System.Numerics;

namespace ConsoleRogueLike.Entities
{
    public sealed class Player : GameEnemy
    {
        private readonly IGameScene _sceneReader;

        public Player(char symbol, Vector point, int health, IGameScene sceneReader)
             : base(symbol, point, health, sceneReader) => _sceneReader = sceneReader;

        public void SetStartPoint()
        {
            Point = new Vector(1, 1);
        }

        public override void TakeDamage(int damageAmount)
        {
            base.TakeDamage(damageAmount);
            if (Health <= 0)
            {
                _sceneReader.GameOver();
            }
        }
    }
}