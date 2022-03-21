using System.Text.Json.Serialization;

namespace GameOfLife
{
    public class Cell
    {
        public int NumberOfLiveNeighbours { get; set; }
        public bool IsAlive { get; set; }
        public Position Position { get; set; }

        public Cell(Position position)
        {
            Position = position;
            
        }

    }
}
