using System;
using GameOfLife;
using Xunit;

namespace GameOfLifeTests
{
    public class ProgramTests
    {
        [Fact]
        public void PotatoReferenceCheck()
        {
            // Arrange 
            var check = new Check();
            
            // Act 
            var actual = check.ReferenceCheck();
            
            // Assert
            var expected = "Yep, setup works as expected.";
            Assert.Equal(expected,actual);
        }
    }
}
