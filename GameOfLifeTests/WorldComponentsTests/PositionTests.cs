using GameOfLife.WorldComponents;
using Xunit;

namespace GameOfLifeTests.WorldComponentsTests
{
    public class PositionTests
    {
        [Fact]
        public void
            GivenTwoInstancesOfPositions_WhenTheValuesOfEachPositionsRowAndColumnAreEqual_ThesePositionsAreDefinedAsEqual()
        {
            // Arrange 
            var position1 = new Position(10, 15);
            var position2 = new Position(10, 15);

            // Assert 
            Assert.True(position1 == position2);
        }

        [Theory]
        [InlineData(10, 15, 11, 15)]
        public void GivenTwoPositions_WhenTheyHaveUnequalValuesForColumnOrRow_ThesePositionsAreDefinedAsNotEqual(
            int position1Row, int position1Column, int position2Row, int position2Column)
        {
            // Arrange 
            var position1 = new Position(position1Row, position1Column);
            var position2 = new Position(position2Row, position2Column);

            Assert.True(position1 != position2);
        }
    }
}
