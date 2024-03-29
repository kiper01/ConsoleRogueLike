using ConsoleRogueLike.Entities;
using ConsoleRogueLike.Core;

class Program
{
    static void Main()
    {
        Vector area = new Vector(50, 20);

        Random random = new();

        GameObjectFactory factory = new();
        GameScene scene = new(area, factory, random);

        Player player = factory.CreatePlayer(new Vector(1, 1), scene);

        GameLoop gameLoop = new(scene, player);

        gameLoop.Run();
    }
}
