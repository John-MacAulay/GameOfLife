using System.Text.Json.Serialization;

namespace GameOfLife.WorldComponents
{
    public class Cell
    {
        public Position Position { get;}
        public bool IsAlive { get; private set; }
        public int NumberOfLiveNeighbours { get; private set; }

        public Cell(Position position)
        {
            Position = position;
        }

        [JsonConstructor]
        public Cell(Position position, bool isAlive, int numberOfLiveNeighbours)
        {
            Position = position;
            IsAlive = isAlive;
            NumberOfLiveNeighbours = numberOfLiveNeighbours;
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
