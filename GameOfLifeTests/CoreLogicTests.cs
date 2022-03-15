using GameOfLife;
using Microsoft.VisualBasic;
using Xunit;

namespace GameOfLifeTests
{
    public class CoreLogicTests
    {
        [Fact]
        public void GivenGetWorldManually_ItWillReturnAnEmptyNewWorldAccordingToInputParameters()
        {
            // Arrange 
            var testOutput = new TestOutput();
            var testInput = new TestInput(new []{"10","15"});
            var coreLogic = new CoreLogic(testOutput, testInput);
       
        }
        
    }
}
