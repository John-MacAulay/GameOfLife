using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using GameOfLife;
using Xunit;

namespace GameOfLifeTests
{
    public class WorldPositionValidatorTests
    {
        [Theory]
        [InlineData(20, 15, "2,5")]
        [InlineData(8, 10, "0,9")]
        [InlineData(15, 10, "14,0")]
        public void
            WorldPositionValidatorTryParseStringToPosition_WhenGivenIntParsableString_WithValuesInWorldParameterRange_WillReturnTrue(
                int worldLength, int worldHeight, string stringToCheck)
        {
            // Assert
            var world = new World(worldLength, worldHeight);
            var validator = new WorldPositionValidator(world);

            // Act
            var actualBool = validator.TryParseStringToPosition(stringToCheck);

            // Assert
            Assert.True(actualBool);
        }

        [Theory]
        [InlineData(20, 15, "-1,5")]
        [InlineData(8, 10, "0,-1")]
        [InlineData(15, 10, "15,0")]
        [InlineData(14, 10, "15,10")]
        public void
            TryParseStringToPosition_WhenGivenIntParsableString_WithValuesOutsideWorldParameterRange_WillReturnFalse(
                int worldLength, int worldHeight, string stringToCheck)
        {
            // Assert
            var world = new World(worldLength, worldHeight);
            var validator = new WorldPositionValidator(world);

            // Act
            var actualBool = validator.TryParseStringToPosition(stringToCheck);

            // Assert
            Assert.False(actualBool);
        }

        [Fact]
        public void
            ValidatedPosition_WillReturnNull_WhenNewlyInstantiatedWorldPositionValidator()
        {
            // Assert
            var world = new World(20, 15);
            var validator = new WorldPositionValidator(world);

            // Act
            var actual = validator.ValidatedPosition;

            // Assert
            Assert.Null(actual);
        }

        [Theory]
        [InlineData(20, 15, "-1,5")]
        [InlineData(20, 15, "7,25")]
        public void
            ValidatedPosition_WillReturnNull_WhenPreviousTryParseStringToPositionWasFalse
            (int worldLength, int worldHeight, string stringToCheck)
        {
            // Assert
            var world = new World(worldLength, worldHeight);
            var validator = new WorldPositionValidator(world);
            var validationIsTrue = validator.TryParseStringToPosition(stringToCheck);

            // Act
            var actual = validator.ValidatedPosition;

            // Assert
            Assert.Null(actual);
        }
        public static IEnumerable<object[]> GetInputs()
        {
            yield return new object[]
            {
                10,
                15,
                "3,4",
                new Position(3,4)
            };
        }
        
        [Theory]
        [MemberData(nameof(GetInputs))]
        public void
            ValidatedPosition_WillReturnLastValidatedPosition_WhenPreviousTryParseStringToPositionWasTrue
            (int worldLength, int worldHeight, string stringToCheck, Position expectedValidatedPosition)
        {
            // Arrange
            var world = new World(worldLength, worldHeight);
            var validator = new WorldPositionValidator(world);
            var validationIsTrue = validator.TryParseStringToPosition(stringToCheck);
            
            // Act 
            var actual = validator.ValidatedPosition;
            
            // Assert 
            Assert.Equal(expectedValidatedPosition,actual);

        }
    }
}
