using GameOfLife.WorldComponents;

namespace GameOfLife.WorldSourcing
{
    public interface IWorldSource
    {
        public World RetrieveWorld();
    }
}
