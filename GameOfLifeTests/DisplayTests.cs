using System;
using GameOfLife;
using Xunit;

namespace GameOfLifeTests
{
    public class DisplayTests
    {
        [Fact]
        public void GivenAWorld_WithSomeLiveCells_ShowWorld_WillOutputRepresentativeGrid()
        {
            // Arrange
            var testOutput = new TestOutput();
            var display = new Display(testOutput);
            var world = new World(3, 3);
            const int displaySleep = 0;
            var firstCellToMakeLive = world.CellAtThisWorldPosition(new Position(0, 0));
            firstCellToMakeLive.MakeCellAlive();
            var secondCellToMakeLive = world.CellAtThisWorldPosition(new Position(1, 2));
            secondCellToMakeLive.MakeCellAlive();
            display.ShowWorld(world, displaySleep);

            // Act 
            var actual = testOutput.FakeOutput[1];

            // Assert
            var expected = $"{Environment.NewLine} Generation 0 {Environment.NewLine}{Environment.NewLine}" +
                           $" * · ·{Environment.NewLine} · · ·{Environment.NewLine} · * ·{Environment.NewLine}";
            Assert.Equal(expected, actual);
        }
    }
}
