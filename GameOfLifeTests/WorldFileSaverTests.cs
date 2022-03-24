using System.Collections.Generic;
using System.IO;
using GameOfLife;
using Xunit;

namespace GameOfLifeTests
{
    public class WorldFileSaverTests
    {
      //  private readonly string _testFolder = $@"..//..//..//..//./TestSavedWorlds/";
        private readonly string _testFolder = $@"/Users/John.MacAulay/Documents/GameOfLifeTestSaves";

        [Fact]
        public void GivenAValidWorld_SaveJsonLocal_WillSaveThisWoldFileInJsonFormatToLocalFolder()
        {
            // Arrange 
            var filePathToCheck = _testFolder + "/TinyWorld.json";
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
            
            var worldFileSave = new WorldFileSaver(world, _testFolder);
            worldFileSave.SaveJsonLocal("TinyWorld");

            // Act
            string actual;
            using (var sr = new StreamReader(filePathToCheck))
            {
                actual = sr.ReadToEnd();
            }

            const string expected = "{\"CurrentGenerationNumber\":0,\"Cells\":[{\"NumberOfLiveNeighbours\":0," +
                                    "\"IsAlive\":false,\"Position\":{\"Row\":0,\"Column\":0}}," +
                                    "{\"NumberOfLiveNeighbours\":0,\"IsAlive\":false,\"Position\":" +
                                    "{\"Row\":0,\"Column\":1}},{\"NumberOfLiveNeighbours\":0,\"IsAlive\":true," +
                                    "\"Position\":{\"Row\":1,\"Column\":0}},{\"NumberOfLiveNeighbours\":0," +
                                    "\"IsAlive\":true,\"Position\":{\"Row\":1,\"Column\":1}}]," +
                                    "\"Length\":2,\"Height\":2}\n";
            Assert.True(File.Exists(filePathToCheck));
            Assert.Equal(expected, actual);
        }
    }
}
