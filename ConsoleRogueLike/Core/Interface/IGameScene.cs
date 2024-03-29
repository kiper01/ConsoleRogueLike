using ConsoleRogueLike.Entities;

namespace ConsoleRogueLike.Core.Interface
{
    public interface IGameScene
    {
        IReadOnlyList<GameObject> GameObjects { get; }
        Vector Area { get; }
        bool IsPointFree(Vector point);
        List<GameObject> GetObjectsAround(Vector point);
        GameObject? GetObjectAtPoint(Vector point);
        void AtFinish();
        void RemoveGameObject(GameObject gameObject);
        void GameOver();
    }
}
