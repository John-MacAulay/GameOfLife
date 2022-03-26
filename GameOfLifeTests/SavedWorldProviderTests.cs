using GameOfLife;
using Xunit;

namespace GameOfLifeTests
{
    public class SavedWorldProviderTests
    {
        private readonly string _testFolder = $@"..//..//..//..//./TestSavedWorlds/";
       
      
        [Fact]
        public void GivenValidInputs_SavedWorldProvider_RetrieveWorld_WillReturnWorldCorrectly()
        {
            // Arrange
            var testOutput = new TestOutput();
            var display = new Display(testOutput);
            var testInput = new TestInput(new[]{ "1"});
            var provider = new SavedWorldProvider(display, testInput, _testFolder);
            
            // Act
            var world = provider.RetrieveWorld();
            var returnedObjectType = world.GetType();
            
            // Assert
            Assert.True(returnedObjectType  == typeof(World) );
        }
    }
}
