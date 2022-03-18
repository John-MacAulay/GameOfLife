using GameOfLife;
using Xunit;

namespace GameOfLifeTests
{
    public class WorldFileSaverTests
    {
        [Fact]
        public void GivenAValidWorld_SaveJsonLocal_WillSaveJsonFileToLocalFolder()
        {
            // Arrange 
            var world = new World(20,25);
            var testAliveLocation = world.CellAtThisWorldPosition(new Position(12, 17));
            testAliveLocation.IsAlive = true;
            var worldFileSave = new WorldFileSaver(world);
            
            worldFileSave.SaveJsonLocal("OfficialWorldSave");

            var reader = new WorldFileReader();
            var returnedWorld = reader.LoadJsonLocal("OfficialWorldSave");
            
            Assert.NotNull(returnedWorld);
        }
    }
}
