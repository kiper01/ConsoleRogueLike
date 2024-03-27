using ConsoleRogueLike.Core.Interface;

namespace ConsoleRogueLike.Entities
{
    public class Exit : GameObject
    {
        private readonly IGameScene _sceneReader;
        public Exit(char symbol, Vector point, IGameScene sceneReader)
            : base(symbol, point)
        {
            _sceneReader = sceneReader;
        }

        public override void Update()
        {
            var objectsAround = _sceneReader.GetObjectsAround(Point);
            Player? player = objectsAround.OfType<Player>().FirstOrDefault();

            if (player != null)
            {
                _sceneReader.Finished();
            }
        }
    }
}
