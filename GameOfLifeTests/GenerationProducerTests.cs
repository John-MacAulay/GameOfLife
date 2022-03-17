using System.Collections.Generic;
using System.Linq;
using GameOfLife;
using Xunit;
using Xunit.Sdk;

namespace GameOfLifeTests
{
    public class GenerationProducerTests
    {
        public static IEnumerable<object[]> GetInputs()
        {
            yield return new object[]
            {
                new Position(1, 2),
                new List<Position>
                {
                    new(0, 1),
                    new(1, 1),
                    new(2, 1),
                    new(0, 2),
                    new(2, 2),
                    new(0, 3),
                    new(1, 3),
                    new(2, 3)
                }
            };
            yield return new object[]
            {
                new Position(0, 0),
                new List<Position>
                {
                    new(4, 7),
                    new(0, 7),
                    new(1, 7),
                    new(4, 0),
                    new(1, 0),
                    new(4, 1),
                    new(0, 1),
                    new(1, 1)
                }
            };
            yield return new object[]
            {
                new Position(4, 7),
                new List<Position>
                {
                    new(3, 6),
                    new(4, 6),
                    new(0, 6),
                    new(3, 7),
                    new(0, 7),
                    new(3, 0),
                    new(4, 0),
                    new(0, 0)
                }
            };
        }

        [Theory]
        [MemberData(nameof(GetInputs))]
        public void GivenACell_ReturnNeighbours_GetsAListOfEightClosestNeighboursToThatCellIn2DWrappedWorld
            (Position initialCellPosition, List<Position> manualNeighbourPositions)
        {
            // Arrange
            var world = new World(5, 8);
            var nextGen = new GenerationProducer(world);
            var cellToMakeLive = world.Cells.First(cell => cell.Position == initialCellPosition);
         
            // Act
            var actualNeighbourCellsList = nextGen.ReturnNeighbours(cellToMakeLive);

            // Assert
            var expected =
                manualNeighbourPositions.Select(position => world.Cells.First(cell => cell.Position == position))
                    .ToList();

            Assert.Equal(expected, actualNeighbourCellsList);
        }

        [Fact]
        public void
            MakeNextGeneration_WillMakeACellAliveForNewGeneration_WhenItHasExactlyThreeNeighbours_RegardlessOfPreviousStatus()
        {
            // Arrange 
            var world = new World(5, 5);
            var positionsOfCellsThatAreAlive = new List<Position>()
            {
                new Position(1,1), 
                new Position(1,2),
                new Position(2,1)
            };
            var cellToMakeLive =
                positionsOfCellsThatAreAlive.Select(position => world.Cells.First(cell => cell.Position == position)).ToList();
            foreach (var cell in cellToMakeLive)
            {
                cell.IsAlive = true;
            }
            var nextGen = new GenerationProducer(world);
            nextGen.MakeNextGeneration();
            
            // Act 
            var cellToCheck = world.Cells.First(cell => cell.Position == new Position(2, 2));
            
            // Assert
            Assert.True(cellToCheck.IsAlive);

        }
    }
}
