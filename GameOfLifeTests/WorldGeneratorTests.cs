using System.Collections.Generic;
using System.Linq;
using GameOfLife;
using Xunit;

namespace GameOfLifeTests
{
    public class WorldGeneratorTests
    {
        [Fact]
        public void GivenValidInputsInSequence_GetWorldFromManualInputsWillReturnAValidWorldOfAppropriateSize()
        {
            // Arrange 
            var output = new TestOutput();
            var input = new TestInput(new[] {"10","15","q"});
            var generator = new WorldGenerator(output, input);
            
            // Act
            var actualWorld = generator.GetWorldFromManualInputs();
            var actual = actualWorld.GetType();
            var comparisonWorld = new World(1, 1);
            
            // Expected 
            var expectedTypeOfObject = comparisonWorld.GetType();
            const int expectedLengthOfCreatedWorld = 10;
            const int expectedHeightOfCreatedWorld = 15;
            Assert.Equal(expectedTypeOfObject, actual);
            Assert.Equal(expectedLengthOfCreatedWorld, actualWorld.Length);
            Assert.Equal(expectedHeightOfCreatedWorld, actualWorld.Height);
        }

        [Fact]
        public void GivenInValidInputsInSequence_GetWorldFromManualInputsWillLoopUntilValidInputsThenReturnValidWorld()
        {
            // Arrange 
            var output = new TestOutput();
            var input = new TestInput(new[] {"Invalid Input for Length","invalid again", "12", "Invalid Input for Height","8","q"});
            var generator = new WorldGenerator(output, input);

            // Act 
            var actualWorld = generator.GetWorldFromManualInputs();
            var prompt1 = output.FakeOutput[0];
            var prompt2 = output.FakeOutput[1];
            var prompt3 = output.FakeOutput[2];
            var prompt4 = output.FakeOutput[3];
            var prompt5 = output.FakeOutput[4];

            // Expected
            const string expectedFirstPrompt = "Please enter the grid length for this Game of Life.";
            const string expectedSecondPrompt = "Please enter the grid length for this Game of Life.";
            const string expectedThirdPrompt = "Please enter the grid length for this Game of Life.";
            const string expectedFourthPrompt = "Please enter the grid height for this Game of Life.";
            const string expectedFifthPrompt = "Please enter the grid height for this Game of Life.";
            const int expectedLengthOfCreatedWorld = 12;
            const int expectedHeightOfCreatedWorld = 8;
            
            Assert.Equal(expectedFirstPrompt,prompt1);
            Assert.Equal(expectedSecondPrompt,prompt2);
            Assert.Equal(expectedThirdPrompt, prompt3);
            Assert.Equal(expectedFourthPrompt, prompt4); 
            Assert.Equal(expectedFifthPrompt, prompt5); 
            Assert.Equal(expectedHeightOfCreatedWorld, actualWorld.Height);
            Assert.Equal(expectedLengthOfCreatedWorld, actualWorld.Length);
        }

        public static IEnumerable<object[]> GetInputs()
        {
            yield return new object[]
            {
                new[]{"9","8", "3,2", "2,2","4,4","q"},
                72,
                3
            };
            
            yield return new object[]
            {
                new[]{"15","20", "3,2", "12,10" ,"invalid", "2,2","4,4", "13,10", "q"},
                300,
                5
            };
            
        }


        [Theory]
        [MemberData(nameof(GetInputs))]
        public void GivenValidInputsInSequence_GetWorldFromManualInputs_CanReturnAWorldWithDesignatedCellsAsAlive
            (string[] userInput, int expectedCells, int expectedLiveSeedCells)
        {
            // Arrange 
            var output = new TestOutput();
            var input = new TestInput(userInput);
            var generator = new WorldGenerator(output, input);
            var world = generator.GetWorldFromManualInputs();

            // Act 
            var numberOfWorldCells = world.Cells.Count;
            var numberOfLiveSeedCells = world.Cells.Where(cell => cell.IsAlive).ToList().Count; 
            
            // Assert
            Assert.Equal(expectedCells,numberOfWorldCells);
            Assert.Equal(expectedLiveSeedCells, numberOfLiveSeedCells);
            
            
        }
    }
}
