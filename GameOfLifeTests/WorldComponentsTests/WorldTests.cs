using System.Linq;
using GameOfLife.WorldComponents;
using Xunit;

namespace GameOfLifeTests.WorldComponentsTests
{
    public class WorldTests
    {
        [Fact]
        public void GivenANewWorld_WhichIsInitiatedByLengthAndHeight_TheNumberOfCellsEqualsLengthMultipliedByHeight()
        {
            // Arrange 
            var world = new World(10, 15);

            // Actual
            var actual = world.Cells.Count;

            // Assert
            const int expected = 150;
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GivenANewWorld_EachLocationShouldHaveAUniquePosition()
        {
            // Arrange 
            var world = new World(10, 15);
            var cellPositions = world.Cells.Select(cell => cell.Position).ToList();

            // Act
            var actualUniquePositions = cellPositions.Distinct().Count();

            // Expected 
            var expectedUniquePositions = world.Cells.Count;
            Assert.True(expectedUniquePositions == actualUniquePositions);
        }

        [Fact]
        public void GivenAWorldWithNoCellsThatAreAlive_IsEmpty_ReturnsTrue()
        {
            // Arrange 
            var world = new World(10, 15);

            // Act
            var actual = world.IsEmpty();

            //Assert
            Assert.True(actual);
        }
    }
}
