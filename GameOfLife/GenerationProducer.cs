using System.Collections.Generic;
using System.Linq;
using GameOfLife.WorldComponents;

namespace GameOfLife
{
    public class GenerationProducer
    {
        private World World { get; }

        private List<List<Position>> HistoricalLivePositionsLists { get; set; }

        public GenerationProducer(World world)
        {
            World = world;
            HistoricalLivePositionsLists = new List<List<Position>>();
        }

        public void MakeNextGeneration()
        {
            World.IncrementGenerationNumber();
            ResetCurrentLiveNeighboursOfAllCellsToZero();
            UpdateAllCellsNumberOfLiveNeighbours();
            ApplyRulesForNextGenerationLife();
            UpdateWorldBasedOnOscillation();
        }


        private void UpdateWorldBasedOnOscillation()
        {
            if (World.Periodicity != null)
            {
                return;
            }

            int? periodicity = null;
            int? generationStartOfPeriodicity = null;
            var isUnique = true;
            var currentLiveCellPositions = World.RetrieveLiveCellPositions();
            if (HistoricalLivePositionsLists.Count == 0)
            {
                HistoricalLivePositionsLists.Add(currentLiveCellPositions);
            }
            else
            {
                foreach (var list in HistoricalLivePositionsLists
                             .Where(list => list.Count == currentLiveCellPositions.Count)
                             .Where(list => !currentLiveCellPositions.Except(list).Any()))
                {
                    isUnique = false;
                    generationStartOfPeriodicity = HistoricalLivePositionsLists.IndexOf(list);
                }
            }

            if (isUnique)
            {
                HistoricalLivePositionsLists.Add(currentLiveCellPositions);
            }
            else
            {
                periodicity = HistoricalLivePositionsLists.Count - generationStartOfPeriodicity - 1;
            }

            if (periodicity == null) return;
            World.SetPeriodicity(periodicity);
            World.SetPeriodicityStartGeneration(generationStartOfPeriodicity);
        }

        private void ApplyRulesForNextGenerationLife()
        {
            foreach (var cell in World.Cells.Where(cell => cell.NumberOfLiveNeighbours == 3))
            {
                cell.MakeCellAlive();
            }

            foreach (var cell in World.Cells.Where(cell => cell.IsAlive))
            {
                switch (cell.NumberOfLiveNeighbours)
                {
                    case 2:
                        cell.MakeCellAlive();
                        break;
                    case < 2:
                    case > 3:
                        cell.KillCell();
                        break;
                }
            }
        }

        private void UpdateAllCellsNumberOfLiveNeighbours()
        {
            var liveCells = World.Cells.Where(cell => cell.IsAlive).ToList();
            foreach (var cell in liveCells)
            {
                UpdateNeighboursNumberOfLiveNeighbours(cell);
            }
        }

        private void ResetCurrentLiveNeighboursOfAllCellsToZero()
        {
            foreach (var cell in World.Cells)
            {
                cell.ResetNumberOfLiveNeighboursToZero();
            }
        }

        private void UpdateNeighboursNumberOfLiveNeighbours(Cell liveCell)
        {
            var neighbours = ReturnNeighbours(liveCell);
            foreach (var cell in neighbours)
            {
                cell.IncrementNumberOfLiveNeighbours();
            }
        }

        public List<Cell> ReturnNeighbours(Cell liveCell)
        {
            var potentialRowAbove = liveCell.Position.Row - 1;
            var potentialRowBelow = liveCell.Position.Row + 1;
            var potentialColumnLeft = liveCell.Position.Column - 1;
            var potentialColumnRight = liveCell.Position.Column + 1;

            var positionsOfNeighbours = new List<Position>()
            {
                new(potentialColumnLeft, potentialRowAbove),
                new(liveCell.Position.Column, potentialRowAbove),
                new(potentialColumnRight, potentialRowAbove),

                new(potentialColumnLeft, liveCell.Position.Row),
                new(potentialColumnRight, liveCell.Position.Row),

                new(potentialColumnLeft, potentialRowBelow),
                new(liveCell.Position.Column, potentialRowBelow),
                new(potentialColumnRight, potentialRowBelow)
            };

            var adjustedPositions = positionsOfNeighbours.Select(AdjustForWorldWrapping).ToList();
            return adjustedPositions.Select(position => World.CellAtThisWorldPosition(position)).ToList();
        }

        private Position AdjustForWorldWrapping(Position potentialNewPosition)
        {
            if (potentialNewPosition.Row < 0) potentialNewPosition.Row = World.Height - 1;

            if (potentialNewPosition.Column < 0) potentialNewPosition.Column = World.Length - 1;

            if (potentialNewPosition.Row > World.Height - 1) potentialNewPosition.Row = 0;

            if (potentialNewPosition.Column > World.Length - 1) potentialNewPosition.Column = 0;
            return potentialNewPosition;
        }
    }
}
