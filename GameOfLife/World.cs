using System.Collections.Generic;

namespace GameOfLife
{
    public class World
    {
        public List<Cell> Cells { get; }

        public int Length { get; }
        public int Height { get; }

        public World(int length, int height)
        {
            Length = length;
            Height = height;

            Cells = new List<Cell>();
            for (var rowPosition = 0; rowPosition < Height; rowPosition++)
            {
                for (var columnPosition = 0; columnPosition <Length; columnPosition++)
                {
                    var cellToAdd = new Cell(new Position(rowPosition, columnPosition));
                    Cells.Add(cellToAdd);
                }
            }
        }

        public bool IsEmpty()
        {
            return Cells.TrueForAll(cell =>!cell.IsAlive );
        }
    }
}
