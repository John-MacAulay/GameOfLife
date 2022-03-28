using System;
using GameOfLife;
using GameOfLifeTests.TestUserInteractions;
using Xunit;

namespace GameOfLifeTests
{
    public class CoreLogicTest
    {
        private readonly string _testFolder = $@"..//..//..//..//./TestSavedWorlds/";
        private const int DisplayBeatTime = 1;

        [Fact]
        public void CoreLogicRun_WhenGivenAppropriateInputs_LoadAndRunGame()
        {
            // Arrange
            var testOutput = new TestOutput();
            var testInput = new TestInput(new[] {"l", "1","n"});
            var core = new CoreLogic(testOutput, testInput, DisplayBeatTime, _testFolder);
            core.LogicRun();

            // Act 
            var actual = testOutput.FakeOutput[12];

            // Expected 
            var expected = $"{Environment.NewLine} Generation " +
                           $"0 {Environment.NewLine}{Environment.NewLine} · ·{Environment.NewLine}" +
                           $" ◉ ◉{Environment.NewLine}{Environment.NewLine} Press 'q' to quit.";
            Assert.Equal(expected, actual);
        }
    }
}
