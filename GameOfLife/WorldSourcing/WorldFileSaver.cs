using System.IO;
using System.Text.Json;
using GameOfLife.WorldComponents;

namespace GameOfLife.WorldSourcing
{
    public class WorldFileSaver
    {
        private readonly World _world;
        private readonly string _pathToSaveFolder;

        public WorldFileSaver(World world, string pathToSaveFolder)
        {
            _world = world;
            _pathToSaveFolder = pathToSaveFolder;
        }

        public void SaveJsonLocal(string fileName)
        {
            var jsonString = JsonSerializer.Serialize(_world);
            var fileToWrite = new StreamWriter($@"{_pathToSaveFolder}/{fileName}.json");
            fileToWrite.WriteLine(jsonString);
            fileToWrite.Close();
        }
    }
}
