using System.IO;
using GameOfLife;
using Xunit;

namespace GameOfLifeTests
{
    public class WorldProviderTests
    {

        private readonly string _testFolder = $@"..//..//..//..//./TestSavedWorlds/";
        [Fact]
        public void GivenValidInputs_WorldProvider_RetrieveWorld_Using_ManualInputs_WillReturnAValidWorldAsPerInstructions()
        {
            // Arrange
            var testOutput = new TestOutput();
            var testInput = new TestInput(new[]{"anything but l","6","10","2,3", "3,7","q", "n"});
            var provider = new WorldProvider(testOutput, testInput, _testFolder);
            
            // Act
            var world = provider.RetrieveWorld();
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
        public void GivenValidInputs_WorldProvider_RetrieveWorld_Using_ManualInputs_WillSaveAndReturnAValidWorldAsPerInstructions()
        {
            // Arrange
            var testOutput = new TestOutput();
            var testInput = new TestInput(new[]{"anything but l","10","12","4,5", "invalid" , "2,2","q", "y","Save this world"});
            var provider = new WorldProvider(testOutput, testInput, _testFolder);
            
            // Act
            var world = provider.RetrieveWorld();
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
        
        [Fact]
        public void GivenValidInputs_WorldProvider_RetrieveWorld_ChooseWorldFromSavedWorlds_WillReturnWorldCorrectly()
        {
            // Arrange
            var testOutput = new TestOutput();
            var testInput = new TestInput(new[]{"l", "1"});
            var provider = new WorldProvider(testOutput, testInput, _testFolder);
            
            // Act
            var world = provider.RetrieveWorld();
            var returnedObjectType = world.GetType();
            
            // Assert
            Assert.True(returnedObjectType  == typeof(World) );
        }
        
    }
}
