using System;
using System.IO;
using GameOfLife.UserInteractions;
using GameOfLife.WorldComponents;
using GameOfLife.WorldSourcing;
using GameOfLifeTests.TestUserInteractions;
using Xunit;

namespace GameOfLifeTests.WorldSourcingTests
{
    public class SavedWorldSourceTests
    {

        private static string UseThisTestFolder()
        {
            var folderPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var testFolder = $"{folderPath}/Documents/GameOfLifeTestSaves";
            Directory.CreateDirectory($@"{testFolder}");
            return testFolder;
        }
        
        [Fact]
        public void GivenValidInputs_SavedWorldSource_RetrieveWorld_WillReturnWorldCorrectly()
        {
            // Arrange
            var testFolder = UseThisTestFolder();
            var testOutput = new TestOutput();
            var display = new Display(testOutput);
            var testInput = new TestInput(new[] {"1"});
            var source = new SavedWorldSource(display, testInput, testFolder);

            // Act
            var world = source.RetrieveWorld();
            var returnedObjectType = world.GetType();

            // Assert
            Assert.True(returnedObjectType == typeof(World));
        }
    }
}
