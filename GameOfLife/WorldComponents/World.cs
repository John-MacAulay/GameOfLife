using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace GameOfLife.WorldComponents
{
    public class World
    {
        public int CurrentGenerationNumber { get; private set; }
        public List<Cell> Cells { get; }

        public int? Periodicity { get; private set; }
        public int? GenerationStartOfPeriodicity { get; private set; }

        public int Length { get; }
        public int Height { get; }

        public World(int length, int height)
        {
            Length = length;
            Height = height;

            Cells = new List<Cell>();
            for (var rowPosition = 0; rowPosition < Height; rowPosition++)
            {
                for (var columnPosition = 0; columnPosition < Length; columnPosition++)
                {
                    var cellToAdd = new Cell(new Position(columnPosition, rowPosition));
                    Cells.Add(cellToAdd);
                }
            }
        }

        [JsonConstructor]
        public World(int length, int height, List<Cell> cells, int currentGenerationNumber,
            int? periodicity, int? generationStartOfPeriodicity)
        {
            Height = height;
            Length = length;
            Cells = cells;
            CurrentGenerationNumber = currentGenerationNumber;
            Periodicity = periodicity;
            GenerationStartOfPeriodicity = generationStartOfPeriodicity;
        }

        public bool IsEmpty()
        {
            return Cells.TrueForAll(cell => !cell.IsAlive);
        }

        public Cell CellAtThisWorldPosition(Position position)
        {
            return Cells.First(cell => cell.Position == position);
        }

        public void IncrementGenerationNumber()
        {
            CurrentGenerationNumber++;
        }

        public void SetPeriodicity(int? periodicity)
        {
            Periodicity = periodicity;
        }

        public List<Position> RetrieveLiveCellPositions()
        {
            var liveCell = Cells.Where(cell => cell.IsAlive == true).ToList();
            return liveCell.Select(cell => cell.Position).ToList();
        }

        public void SetPeriodicityStartGeneration(int? generationStartOfPeriodicity)
        {
            GenerationStartOfPeriodicity = generationStartOfPeriodicity;
        }
    }
}
