using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
                world.CellAtThisWorldPosition(new Position(1, 0)),
                world.CellAtThisWorldPosition(new Position(0, 0)),
                world.CellAtThisWorldPosition(new Position(2, 0)),
                world.CellAtThisWorldPosition(new Position(0, 1)),
                world.CellAtThisWorldPosition(new Position(4, 0))
                
            };
            foreach (var cell in cellsToBeAlive)
            {
                cell.MakeCellAlive();
            }

            var worldFileSave = new WorldFileSaver(world, _testFolder);
            worldFileSave.SaveJsonLocal("OfficialWorldSave");
            var reader = new WorldFileLoader(_testFolder);

            // Act
            var actualReturnedWorld = reader.LoadJsonLocal("OfficialWorldSave");
            var actualTypeOfObject = actualReturnedWorld.GetType();
            var actualCellCount = actualReturnedWorld.Cells.Count;
            var actualLivePositionsInOrder = actualReturnedWorld.Cells.Where(cell => cell.IsAlive).ToList()
                .Select(cell => cell.Position).OrderBy(cell => cell.Row).ThenBy(cell => cell.Column).ToList();

            // Arrange
            const int expectedNumberOfCells = 60;
            var expectedLivePositionsInOrder = new List<Position>()
            {
                new(0, 0),
                new(1, 0),
                new(2, 0),
                new(4, 0),
                new(0, 1)
            };
            foreach (var expectedLivePosition in expectedLivePositionsInOrder)
            {
                Assert.Contains(actualLivePositionsInOrder, actualPosition => actualPosition == expectedLivePosition);
            }

           
            Assert.NotNull(actualReturnedWorld);
            Assert.True(actualTypeOfObject  == typeof(World) );
            Assert.Equal(expectedNumberOfCells, actualCellCount);
            for (var i = 0; i < expectedLivePositionsInOrder.Count; i++)
            {
                Assert.Equal(expectedLivePositionsInOrder[i],actualLivePositionsInOrder[i]);
            }
        }

    }
}
