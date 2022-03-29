using System.Collections.Generic;
using GameOfLife.WorldComponents;
using GameOfLife.WorldSourcing;
using Xunit;

namespace GameOfLifeTests.WorldSourcingTests
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
        [InlineData(20, 15, "invalid,5")]
        [InlineData(20, 15, "4,invalid")]
        public void    TryParseStringToPosition_WhenGivenIntParsableString_WithInvalidValues_WillReturnFalse(
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
        
        [Theory]
        [InlineData(20, 15, "a,5")]
        [InlineData(20, 15, "-1,5")]
        [InlineData(8, 10, "0,-1")]
        [InlineData(15, 10, "15,0")]
        [InlineData(14, 10, "15,10")]
        [InlineData(14, 10, "7")]
        [InlineData(14, 10, "3,3,3")]
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
        [InlineData(20, 15, "7,8,14")]
        [InlineData(20, 15, "7")]
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
            Assert.False(validationIsTrue);
        }

        public static IEnumerable<object[]> GetInputs()
        {
            yield return new object[]
            {
                10,
                15,
                "3,4",
                new Position(3, 4)
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
            Assert.Equal(expectedValidatedPosition, actual);
            Assert.True(validationIsTrue);
        }

        public static IEnumerable<object[]> GetInputs2()
        {
            yield return new object[]
            {
                10,
                15,
                "3,4",
                "4,5",
                new Position(4, 5)
            };
            yield return new object[]
            {
                10,
                15,
                "3,4",
                "-1,5",
                null
            };
            yield return new object[]
            {
                10,
                15,
                "30,12,13",
                "9,10",
                new Position(9, 10)
            };
        }

        [Theory]
        [MemberData(nameof(GetInputs2))]
        public void
            GivenMultipleStringsToCheck_ValidatedPosition_WillReturnLastValidatedPosition_OrNull_DependantOnLastTryParseStringToPosition
            (int worldLength, int worldHeight, string stringToCheck, string secondCheckedString,
                Position expectedValidatedPosition)
        {
            // Arrange
            var world = new World(worldLength, worldHeight);
            var validator = new WorldPositionValidator(world);
            var validationIsTrue = validator.TryParseStringToPosition(stringToCheck);
            var secondValidationIsTrue = validator.TryParseStringToPosition(secondCheckedString);
            // Act 
            var actual = validator.ValidatedPosition;

            // Assert 
            Assert.Equal(expectedValidatedPosition, actual);
        }
    }
}
