using System.IO;
using GameOfLife;
using GameOfLife.UserInteractions;
using GameOfLife.WorldComponents;
using GameOfLife.WorldSourcing;
using GameOfLifeTests.TestUserInterfaces;
using Xunit;

namespace GameOfLifeTests.WorldSourcingTests
{
    public class ManualWorldSourceTests
    {
          private readonly string _testFolder = $@"..//..//..//..//./TestSavedWorlds/";
        [Fact]
        public void GivenValidInputs_ManualWorldSource_RetrieveWorld_WillReturnAValidWorldAsPerInstructions()
        {
            // Arrange
            var testOutput = new TestOutput();
            var display = new Display(testOutput);
            var testInput = new TestInput(new[]{"6","10","2,3", "3,7","q", "n"});
            var source = new ManualWorldSource(display, testInput, _testFolder);
            
            // Act
            var world = source.RetrieveWorld();
            var returnedObjectType = world.GetType();
            var sampleLiveCell = world.CellAtThisWorldPosition(new Position(3,7));
            var sampleDeadCell = world.CellAtThisWorldPosition(new Position(4,7));
            
            //Assert
            const int expectedLengthOfWorld = 6;
            Assert.True(returnedObjectType  == typeof(World) );
            Assert.Equal(expectedLengthOfWorld,  world.Length);
            Assert.True(sampleLiveCell.IsAlive);
            Assert.False(sampleDeadCell.IsAlive);
        }
        [Fact]
        public void GivenValidInputs_ManualWorldSource_RetrieveWorld_WillSaveAndReturnAValidWorldAsPerInstructions()
        {
            // Arrange
            var testOutput = new TestOutput();
            var display = new Display(testOutput);
            var testInput = new TestInput(new[]{"10","12","4,5", "invalid" , "2,2","q", "y","Save this world"});
            var source = new ManualWorldSource(display, testInput, _testFolder);
            
            // Act
            var world = source.RetrieveWorld();
            var returnedObjectType = world.GetType();
            var sampleLiveCell = world.CellAtThisWorldPosition(new Position(2,2));
            var sampleDeadCell = world.CellAtThisWorldPosition(new Position(4,7));
            var savedGameFiles = Directory.GetFiles(_testFolder, "*.json");
            
            //Assert
            const int expectedLengthOfWorld = 10;
            var expectedSavedGamePath = $"{_testFolder}Save this world.json";
            Assert.Contains(expectedSavedGamePath, savedGameFiles);
            Assert.True(returnedObjectType  == typeof(World) );
            Assert.Equal(expectedLengthOfWorld,  world.Length);
            Assert.True(sampleLiveCell.IsAlive);
            Assert.False(sampleDeadCell.IsAlive);
            
        }
    }
}
