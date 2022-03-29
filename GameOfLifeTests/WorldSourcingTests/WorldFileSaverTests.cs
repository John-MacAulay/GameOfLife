using System;
using System.Collections.Generic;
using System.IO;
using GameOfLife.WorldComponents;
using GameOfLife.WorldSourcing;
using Xunit;

namespace GameOfLifeTests.WorldSourcingTests
{
    public class WorldFileSaverTests
    {
        private static string UseThisTestFolder()
        {
            var folderPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var testFolder = $"{folderPath}/Documents/GameOfLifeTestSaves";
            Directory.CreateDirectory($@"{testFolder}");
            return testFolder;
        }

        [Fact]
        public void GivenAValidWorld_SaveJsonLocal_WillSaveThisWoldFileInJsonFormatToLocalFolder()
        {
            // Arrange 
            var testFolder = UseThisTestFolder();
            var filePathToCheck = testFolder + "/TinyWorld.json";
            if (File.Exists(filePathToCheck))
            {
                File.Delete(filePathToCheck);
            }

            Assert.False(File.Exists(filePathToCheck));

            var world = new World(2, 2);
            var cellsToBeAlive = new List<Cell>()
            {
                world.CellAtThisWorldPosition(new Position(1, 1)),
                world.CellAtThisWorldPosition(new Position(0, 1)),
            };
            foreach (var cell in cellsToBeAlive)
            {
                cell.MakeCellAlive();
            }

            var worldFileSave = new WorldFileSaver(world, testFolder);
            worldFileSave.SaveJsonLocal("TinyWorld");

            // Act
            string actual;
            using (var sr = new StreamReader(filePathToCheck))
            {
                actual = sr.ReadToEnd();
            }

            const string expected = "{\"CurrentGenerationNumber\":0,\"Cells\":[" +
                                    "{\"Position\":{\"Column\":0,\"Row\":0},\"IsAlive\":false," +
                                    "\"NumberOfLiveNeighbours\":0}," +
                                    "{\"Position\":{\"Column\":1,\"Row\":0},\"IsAlive\":false," +
                                    "\"NumberOfLiveNeighbours\":0}," +
                                    "{\"Position\":{\"Column\":0,\"Row\":1},\"IsAlive\":true," +
                                    "\"NumberOfLiveNeighbours\":0},"+
                                    "{\"Position\":{\"Column\":1,\"Row\":1},\"IsAlive\":true," +
                                    "\"NumberOfLiveNeighbours\":0}]," +
                                    "\"Periodicity\":null,\"GenerationStartOfPeriodicity\":null," +
                                    "\"Length\":2,\"Height\":2}\n";
            Assert.True(File.Exists(filePathToCheck));
            Assert.Equal(expected, actual);
        }
    }
}
