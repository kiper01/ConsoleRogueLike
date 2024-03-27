using ConsoleRogueLike.Entities;

namespace ConsoleRogueLike.Entities
{
    public class GameObject
    {
        public char Symbol { get; protected set; }
        public Vector Point { get; protected set; }

        public GameObject(char symbol, Vector point)
        {
            Symbol = symbol;
            Point = point;
        }

        public virtual void Update() {}
    }
}