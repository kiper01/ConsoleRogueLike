using ConsoleRogueLike.Core.Interface;

namespace ConsoleRogueLike.Entities
{
    public class GameEnemy : GameObject
    {
        private readonly IGameScene _sceneReader;
        public int Health { get; protected set; }

        public GameEnemy(char symbol, Vector point, int health, IGameScene sceneReader)
            : base(symbol, point) => (Health, _sceneReader) = (health, sceneReader);

        public virtual void Move(Vector direction)
        {
            Vector newPoint = Point + direction;

            if (_sceneReader.IsPointFree(newPoint))
            {
                Point = newPoint;
            }
        }

        public virtual void TakeDamage(int damageAmount)
        {
            Health -= damageAmount;
            if (Health <= 0)
            {
                _sceneReader.RemoveGameObject(this);
            }
        }

        public void AddHealth(int damageAmount)
        {
            Health += damageAmount;
        }
    }
}
