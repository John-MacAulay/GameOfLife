using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Linq;

namespace GameOfLife
{
    public class GenerationProducer
    {
       
        private World World { get; set; }

        public GenerationProducer(World world)
        {
            World = world;
        }

        public void MakeNextGeneration()
        {
            var liveCells = World.Cells.Where(cell => cell.IsAlive).ToList();
            foreach (var cell in liveCells)
            {
                UpdateCloseCellsNumberOfLiveNeighbours(cell);
                // list of neighbour cells should be eight
                // defined by position row -1 ( column -1, colum = , column +1) 
                // defined by position row =  ( column -1 , column +1)
                // defined by position row + 1 ( column -1, colum = , column +1)
                // then check for wrapping 
            }
        }

        private void UpdateCloseCellsNumberOfLiveNeighbours(Cell liveCell)
        {

            var potentialRowAbove = liveCell.Position.Row - 1;
            var potentialRowBelow = liveCell.Position.Row + 1;
            var potentialColumnLeft = liveCell.Position.Column - 1;
            var potentialColumRight = liveCell.Position.Column + 1;

            var positionsOfNeighbours = new List<Position>()
            {
                new Position(potentialColumnLeft,potentialRowAbove),
                new Position(liveCell.Position.Column, potentialRowAbove),
                new Position(potentialColumRight,potentialRowAbove),
                
                new Position(potentialColumnLeft, liveCell.Position.Row),
                new Position(potentialColumRight,liveCell.Position.Row),
                
                new Position(potentialColumnLeft, potentialRowBelow),
                new Position(liveCell.Position.Column, potentialRowBelow),
                new Position(potentialColumRight,potentialRowBelow)
                
            };

            var adjustedPositions = positionsOfNeighbours.Select(AdjustWorldWrapping).ToList();
            
            //then foreach cell where the Cell position matches one of these position increase live neighbours by one.
        }

        public List<Cell> ReturnNeighbours(Cell liveCell)
        {    var potentialRowAbove = liveCell.Position.Row - 1;
            var potentialRowBelow = liveCell.Position.Row + 1;
            var potentialColumnLeft = liveCell.Position.Column - 1;
            var potentialColumRight = liveCell.Position.Column + 1;

            var positionsOfNeighbours = new List<Position>()
            {
                new Position(potentialColumnLeft,potentialRowAbove),
                new Position(liveCell.Position.Column, potentialRowAbove),
                new Position(potentialColumRight,potentialRowAbove),
                
                new Position(potentialColumnLeft, liveCell.Position.Row),
                new Position(potentialColumRight,liveCell.Position.Row),
                
                new Position(potentialColumnLeft, potentialRowBelow),
                new Position(liveCell.Position.Column, potentialRowBelow),
                new Position(potentialColumRight,potentialRowBelow)
                
            };

            var adjustedPositions = positionsOfNeighbours.Select(AdjustWorldWrapping).ToList();

            var neighbours = new List<Cell>();
            foreach (var position in adjustedPositions)
            {
                var neighbourToAdd = World.Cells.First(cell => cell.Position == position);
                neighbours.Add(neighbourToAdd);
            }

            return neighbours;
        }
        private Position AdjustWorldWrapping( Position potentialNewPosition)
        {
            if (potentialNewPosition.Row < 0) potentialNewPosition.Row = World.Height - 1;

            if (potentialNewPosition.Column < 0) potentialNewPosition.Column = World.Length - 1;

            if (potentialNewPosition.Row > World.Height - 1) potentialNewPosition.Row = 0;

            if (potentialNewPosition.Column > World.Length - 1) potentialNewPosition.Column = 0;
            return potentialNewPosition;
        }
    }
}
