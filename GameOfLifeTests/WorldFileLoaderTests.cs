using System.Collections.Generic;
using GameOfLife;
using Xunit;

namespace GameOfLifeTests
{
    public class WorldFileLoaderTests
    {
        //  need to have files that aren't Json and make sure this is handled correct;y
        // need to have some test files that aren't valid as co-ordinates are out of world range and make these fail gracefully
        private readonly string _testFolder = $@"..//..//..//..//./TestSavedWorlds/";

        [Fact]
        public void GivenAValidFile_LoadJsonLocal_WillReturnAValidWorldAccordingToJsonFileParameters()
        {
            // Arrange 
            var world = new World(10, 6);
            var cellsToBeAlive = new List<Cell>()
            {
                world.CellAtThisWorldPosition(new Position(0, 0)),
                world.CellAtThisWorldPosition(new Position(1, 0)),
                world.CellAtThisWorldPosition(new Position(2, 0)),
                world.CellAtThisWorldPosition(new Position(0, 1)),
                world.CellAtThisWorldPosition(new Position(4, 0))
            };
            foreach (var cell in cellsToBeAlive)
            {
                cell.IsAlive = true;
            }

            var worldFileSave = new WorldFileSaver(world, _testFolder);
            worldFileSave.SaveJsonLocal("OfficialWorldSave");
            var reader = new WorldFileLoader(_testFolder);

            // Act
            var actualReturnedWorld = reader.LoadJsonLocal("OfficialWorldSave");
            var actualTypeOfObject = actualReturnedWorld.GetType();
            var actualCellCount = actualReturnedWorld.Cells.Count; 
            
            // Arrange
            const int expectedNumberOfCells = 60;
            var expectedType =world.GetType();
            Assert.NotNull(actualReturnedWorld);
            Assert.Equal(expectedType,actualTypeOfObject);
            Assert.Equal(expectedNumberOfCells, actualCellCount);
            
           
      
        }
    }
}
