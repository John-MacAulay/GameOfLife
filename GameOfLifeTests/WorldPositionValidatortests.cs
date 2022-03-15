using System.Runtime.InteropServices;
using GameOfLife;
using Xunit;

namespace GameOfLifeTests
{
    public class WorldPositionValidatorTests
    {
        [Theory]
        [InlineData(20, 15, "2,5")] 
        [InlineData(8,10,"0,9")]
        [InlineData(15,10,"14,0")]
        public void
            WorldPositionValidatorTryParseStringResponseToPosition_WhenGivenIntParsableString_WithValuesOutInParameterRange_WillReturnTrue(
                int worldLength, int worldHeight, string stringToCheck)
        {
            // Assert
            var world = new World(worldLength, worldHeight);
            var validator = new WorldPositionValidator(world);
            

            // Act
            var actualBool = validator.TryParseStringResponseToPosition(stringToCheck);
            
            // Assert
            Assert.True(actualBool);
            
        }
    }
}
