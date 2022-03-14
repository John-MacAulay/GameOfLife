using GameOfLife;
using Microsoft.VisualBasic;
using Xunit;

namespace GameOfLifeTests
{
    public class CoreLogicTests
    {
        [Fact]
        public void GivenCoreLogicPlay_ItWillGetANewWorldAccordingToInputParameters()
        {
            // Arrange 
            var testOutput = new TestOutput();
            var testInput = new TestInput(new []{"This","is a string array"});
            var coreLogic = new CoreLogic(testOutput, testInput);
        }
        
    }
}
