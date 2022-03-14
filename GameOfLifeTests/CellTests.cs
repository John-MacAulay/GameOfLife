using GameOfLife;
using Xunit;

namespace GameOfLifeTests
{
    public class CellTests
    {
        [Fact]
        public void GivenANewLocation_TheLocationIsNotAlive()
        {
            // Arrange 
            var position = new Position(0, 0);
            var cell = new Cell(position);
            
            // Act 
            var actual = cell.IsAlive;
            
            // Assert
            Assert.False(actual);
        }
    }
}
