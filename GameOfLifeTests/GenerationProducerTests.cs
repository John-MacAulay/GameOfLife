using System.Collections.Generic;
using System.Linq;
using GameOfLife;
using GameOfLife.WorldComponents;
using Xunit;

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
            var cellToMakeLive = world.CellAtThisWorldPosition(initialCellPosition);

            // Act
            var actualNeighbourCellsList = nextGen.ReturnNeighbours(cellToMakeLive);

            // Assert
            var expected =
                manualNeighbourPositions.Select(position => world.CellAtThisWorldPosition(position))
                    .ToList();

            Assert.Equal(expected, actualNeighbourCellsList);
        }

        public static IEnumerable<object[]> GetInputs2()
        {
            yield return new object[]
            {
                new Position(2, 2),
                new List<Position>()
                {
                    new Position(1, 1),
                    new Position(1, 2),
                    new Position(2, 1)
                }
            };
            yield return new object[]
            {
                new Position(2, 2),
                new List<Position>()
                {
                    new Position(2, 2),
                    new Position(1, 1),
                    new Position(1, 2),
                    new Position(2, 1)
                }
            };
            yield return new object[]
            {
                new Position(5, 0),
                new List<Position>()
                {
                    new Position(0, 0),
                    new Position(0, 4),
                    new Position(5, 4)
                }
            };
        }

        [Theory]
        [MemberData(nameof(GetInputs2))]
        public void
            MakeNextGeneration_WillMakeACellAliveForNewGeneration_WhenItHasExactlyThreeNeighbours_RegardlessOfPreviousStatus
            (Position cellPositionUnderTest, List<Position> positionsAliveAtStartGeneration)
        {
            // Arrange 
            var world = new World(6, 5);

            var cellToMakeLive =
                positionsAliveAtStartGeneration.Select(position => world.CellAtThisWorldPosition(position))
                    .ToList();
            foreach (var cell in cellToMakeLive)
            {
                cell.MakeCellAlive();
            }

            var nextGen = new GenerationProducer(world);
            nextGen.MakeNextGeneration();

            // Act 
            var cellToCheck = world.CellAtThisWorldPosition(cellPositionUnderTest);

            // Assert
            Assert.True(cellToCheck.IsAlive);
            
            // there might be too many dependencies for this test
        }

        public static IEnumerable<object[]> GetInputs3()
        {
            yield return new object[]
            {
                new Position(2, 2),
                new List<Position>
                {
                    new(2, 2),
                    new(1, 2),
                    new(2, 1),
                },
                true
            };
            yield return new object[]
            {
                new Position(5, 2),
                new List<Position>
                {
                    new(5, 2),
                    new(4, 2),
                    new(0, 2),
                },
                true
            };
            yield return new object[]
            {
                new Position(5, 2),
                new List<Position>
                {
                    new(5, 2),
                    new(4, 2),
                },
                false
            };
            yield return new object[]
            {
                new Position(5, 2),
                new List<Position>
                {
                    new(5, 2),
                    new(4, 2),
                    new(0, 2),
                    new(5, 1),
                    new(4, 1)
                },
                false
            };
            yield return new object[]
            {
                new Position(5, 2),
                new List<Position>
                {
                    new(4, 2),
                    new(0, 2),
                },
                false
            };
        }

        [Theory]
        [MemberData(nameof(GetInputs3))]
        public void
            MakeNextGeneration_WillKeepALiveCellAlive_ForNewGeneration_WhenItHasExactlyTwoOrThreeNeighbours
            (Position cellPositionUnderTest, List<Position> positionsAliveAtStartGeneration, bool isAliveAtNextGen)
        {
            // Arrange 
            var world = new World(6, 5);

            var cellToMakeLive =
                positionsAliveAtStartGeneration.Select(position => world.CellAtThisWorldPosition(position))
                    .ToList();
            foreach (var cell in cellToMakeLive)
            {
                cell.MakeCellAlive();
            }

            var nextGen = new GenerationProducer(world);
            nextGen.MakeNextGeneration();

            // Act 
            // var cellToCheck = world.Cells.First(cell => cell.Position == cellPositionUnderTest);
            var cellToCheck = world.CellAtThisWorldPosition(cellPositionUnderTest);

            // Assert
            Assert.Equal(isAliveAtNextGen, cellToCheck.IsAlive);
        }

        public static IEnumerable<object[]> GetInputs4()
        {
            yield return new object[]
            {
                new Position(1, 1),
                new List<Position>()
                {
                    new(1, 1)
                },
                false
            };
            yield return new object[]
            {
                new Position(1, 1),
                new List<Position>()
                {
                    new(1, 1),
                    new(1, 3),
                    new(4, 3),
                    new(4, 2)
                },
                false
            };
        }

        [Theory]
        [MemberData(nameof(GetInputs4))]
        public void
            NextGeneration_MakesAnyCellThatHasLessThanTwoNeighbours_DeadInNextGeneration
            (Position cellPositionUnderTest, List<Position> positionsAliveAtStartGeneration, bool isAliveAtNextGen)
        {
            // Arrange 
            var world = new World(6, 5);

            var cellToMakeLive =
                positionsAliveAtStartGeneration.Select(position => world.CellAtThisWorldPosition(position))
                    .ToList();
            foreach (var cell in cellToMakeLive)
            {
                cell.MakeCellAlive();
            }

            var nextGen = new GenerationProducer(world);
            nextGen.MakeNextGeneration();

            // Act 
            var cellToCheck = world.CellAtThisWorldPosition(cellPositionUnderTest);

            // Assert
            Assert.Equal(isAliveAtNextGen, cellToCheck.IsAlive);
        }

        public static IEnumerable<object[]> GetInputs5()
        {
            yield return new object[]
            {
                new Position(1, 1),
                new List<Position>()
                {
                    new(1, 1),
                    new(0, 1),
                    new(column: 2, 1),
                    new(1, 2),
                    new(1, 0)
                },
                false
            };
        }

        [Theory]
        [MemberData(nameof(GetInputs5))]
        public void
            NextGeneration_MakesAnyCellHasMoreThanThreeNeighbours_DeadInNextGeneration
            (Position cellPositionUnderTest, List<Position> positionsAliveAtStartGeneration, bool isAliveAtNextGen)
        {
            // Arrange 
            var world = new World(6, 5);

            var cellToMakeLive =
                positionsAliveAtStartGeneration.Select(position => world.CellAtThisWorldPosition(position))
                    .ToList();
            foreach (var cell in cellToMakeLive)
            {
                cell.MakeCellAlive();
            }

            var nextGen = new GenerationProducer(world);
            nextGen.MakeNextGeneration();

            // Act 
            var cellToCheck = world.CellAtThisWorldPosition(cellPositionUnderTest);

            // Assert
            Assert.Equal(isAliveAtNextGen, cellToCheck.IsAlive);
        }

        [Fact]
        public void
            MakeNextGeneration_WhenWorldCoalescesToRepeatingPattern_CorrectlyUpdatesWorldGenerationStartOfPeriodicityAndPeriodicity()
        {
            // Arrange 
            var world = new World(9, 9);
            var positionsAliveAtStartGeneration = new List<Position>()
            {
                new(1, 1),
                new(1, 2),
                new(2, 1),
                new(2, 2),
                new(3, 3),
                new(3, 4),
                new(4, 3),
                new(4, 4),
            };

            var cellToMakeLive =
                positionsAliveAtStartGeneration.Select(position => world.CellAtThisWorldPosition(position))
                    .ToList();
            foreach (var cell in cellToMakeLive)
            {
                cell.MakeCellAlive();
            }

            var nextGen = new GenerationProducer(world);
            nextGen.MakeNextGeneration();
            nextGen.MakeNextGeneration();
            nextGen.MakeNextGeneration();
            nextGen.MakeNextGeneration();
            nextGen.MakeNextGeneration();
            nextGen.MakeNextGeneration();

            // Act
            var periodicity = world.Periodicity;
            var startOfPeriodicity = world.GenerationStartOfPeriodicity;

            // Expected
            const int expectedPeriodicity = 2;
            const int expectedStart = 1;

            Assert.Equal(expectedPeriodicity, periodicity);
            Assert.Equal(expectedStart, startOfPeriodicity);
        }
    }
}
