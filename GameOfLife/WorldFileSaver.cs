using System.IO;
using System.Text.Json;

namespace GameOfLife
{
    public class WorldFileSaver
    {
        private readonly World _world;

        public WorldFileSaver(World world)
        {
            _world = world;
        }

        public void SaveJsonLocal(string fileName)
        {
            var jsonString = JsonSerializer.Serialize(_world);
            var fileToWrite = new StreamWriter($@"..//..//..//..//{fileName}.json");
            fileToWrite.WriteLine(jsonString);
            fileToWrite.Close();
        }
    }
}
