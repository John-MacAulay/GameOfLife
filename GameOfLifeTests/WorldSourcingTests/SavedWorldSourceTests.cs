using GameOfLife;
using GameOfLife.UserInteractions;
using GameOfLife.WorldComponents;
using GameOfLife.WorldSourcing;
using GameOfLifeTests.TestUserInteractions;
using Xunit;

namespace GameOfLifeTests.WorldSourcingTests
{
    public class SavedWorldSourceTests
    {
        private readonly string _testFolder = $@"..//..//..//..//./TestSavedWorlds/";

        [Fact]
        public void GivenValidInputs_SavedWorldSource_RetrieveWorld_WillReturnWorldCorrectly()
        {
            // Arrange
            var testOutput = new TestOutput();
            var display = new Display(testOutput);
            var testInput = new TestInput(new[]{ "1"});
            var source = new SavedWorldSource(display, testInput, _testFolder);
            
            // Act
            var world = source.RetrieveWorld();
            var returnedObjectType = world.GetType();
            
            // Assert
            Assert.True(returnedObjectType  == typeof(World) );
        }
    }
}
