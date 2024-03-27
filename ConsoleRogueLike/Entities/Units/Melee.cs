using ConsoleRogueLike.Core.Interface;

namespace ConsoleRogueLike.Entities
{
    public sealed class Melee : GameEnemy
    {
        private readonly Random _random;
        private readonly IGameScene _sceneReader;

        public Melee(char symbol, Vector point, int health, IGameScene sceneReader, Random random)
             : base(symbol, point, health, sceneReader)
        {
            _random = random;
            _sceneReader = sceneReader;
        }

        public override void Update()
        {
            var objectsAround = _sceneReader.GetObjectsAround(Point);
            Player? player = objectsAround.OfType<Player>().FirstOrDefault();

            if (player != null)
            {
                Attack(player);
                return;
            }

            var offsets = new List<Vector>
            {
                new Vector(0, -1),
                new Vector(0, 1),
                new Vector(-1, 0),
                new Vector(1, 0)
            };

            var freePoints = offsets
                .Where(offset => !objectsAround.Any(obj => obj.Point.Equals(Point + offset)))
                .ToList();

            if (freePoints.Any())
            {
                Vector direction = freePoints[_random.Next(freePoints.Count)];
                Move(direction);
            }
        }

        public void Attack(Player player)
        {
            player.TakeDamage(10);
        }
    }
}
