using System.ComponentModel.Design;
using System.Text.Json.Serialization;

namespace GameOfLife
{
    public class Cell
    {
        public int NumberOfLiveNeighbours { get; private set; }
        public bool IsAlive { get; private set; }
        public Position Position { get;}

        public Cell(Position position)
        {
            Position = position;
        }

        [JsonConstructor]
        public Cell(Position position, bool isAlive)
        {
            Position = position;
            IsAlive = isAlive;
        }
        
        public void ResetNumberOfLiveNeighboursToZero()
        {
            NumberOfLiveNeighbours = 0;
        }

        public void IncrementNumberOfLiveNeighbours()
        {
            NumberOfLiveNeighbours++;
        }

        public void MakeCellAlive()
        {
            IsAlive = true;
        }

        public void KillCell()
        {
            IsAlive = false;
        }
    }
}
