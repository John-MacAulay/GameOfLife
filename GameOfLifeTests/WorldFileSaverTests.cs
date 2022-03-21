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
            var world = new World(7,8);
            var testAliveLocation = world.CellAtThisWorldPosition(new Position(0, 0));
            var testAliveLocation2 = world.CellAtThisWorldPosition(new Position(1, 0));
            var testAliveLocation3 = world.CellAtThisWorldPosition(new Position(2, 0));
            var testAliveLocation4 = world.CellAtThisWorldPosition(new Position(0, 1));
            var testAliveLocation5 = world.CellAtThisWorldPosition(new Position(4, 0));
            var testAliveLocation6 = world.CellAtThisWorldPosition(new Position(2, 3));
    
            testAliveLocation.IsAlive = true;
            testAliveLocation2.IsAlive = true;
            testAliveLocation2.IsAlive = true; 
            testAliveLocation3.IsAlive = true;
            testAliveLocation4.IsAlive = true;
            testAliveLocation5.IsAlive = true;
            testAliveLocation5.IsAlive = true;
            
            
            
            var worldFileSave = new WorldFileSaver(world,$@"..//..//..//..//./TestSavedWorlds/");
            
            worldFileSave.SaveJsonLocal("OfficialWorldSave");

            var reader = new WorldFileReader(@"..//..//..//..//./TestSavedWorlds");
            var returnedWorld = reader.LoadJsonLocal("OfficialWorldSave");
            
            Assert.NotNull(returnedWorld);
        }
    }
}
