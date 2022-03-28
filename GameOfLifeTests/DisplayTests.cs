using System;
using GameOfLife;
using GameOfLife.UserInteractions;
using GameOfLife.WorldComponents;
using GameOfLifeTests.TestUserInteractions;
using Xunit;

namespace GameOfLifeTests
{
    public class DisplayTests
    {
        private readonly string _littleCircle = '\u25e6'.ToString();
        private readonly string _bigCircle = '\u25c9'.ToString();
        private readonly string _openCircle = '\u25cb'.ToString();

        [Fact]
        public void GivenAWorld_WithSomeLiveCells_ShowWorld_WillOutputRepresentativeGrid_WithThreePartPulse()
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
            var actualClearScreen1 = testOutput.FakeOutput[0];
            var actual = testOutput.FakeOutput[1];
            var actualClearScreen2 = testOutput.FakeOutput[2];
            var actual2 = testOutput.FakeOutput[3];
            var actualClearScreen3 = testOutput.FakeOutput[4];
            var actual3 = testOutput.FakeOutput[5];

            // Assert
            const string expectedClear = "The output was cleared at this point.";
            Assert.Equal(expectedClear, actualClearScreen1);

            var expected = $"{Environment.NewLine} Generation 0 {Environment.NewLine}{Environment.NewLine}" +
                           $" {_littleCircle} · ·{Environment.NewLine} · · ·{Environment.NewLine} · {_littleCircle} ·" +
                           $"{Environment.NewLine}{Environment.NewLine} Press 'q' to quit.";
            Assert.Equal(expected, actual);

            Assert.Equal(expectedClear, actualClearScreen2);

            var expectedPhase2 = $"{Environment.NewLine} Generation 0 {Environment.NewLine}{Environment.NewLine}" +
                                 $" {_bigCircle} · ·{Environment.NewLine} · · ·{Environment.NewLine} · {_bigCircle} ·" +
                                 $"{Environment.NewLine}{Environment.NewLine} Press 'q' to quit.";
            Assert.Equal(expectedPhase2, actual2);

            Assert.Equal(expectedClear, actualClearScreen3);

            var expectedPhase3 = $"{Environment.NewLine} Generation 0 {Environment.NewLine}{Environment.NewLine}" +
                                 $" {_openCircle} · ·{Environment.NewLine} · · ·{Environment.NewLine} · {_openCircle} ·" +
                                 $"{Environment.NewLine}{Environment.NewLine} Press 'q' to quit.";
            Assert.Equal(expectedPhase3, actual3);
        }
    }
}
