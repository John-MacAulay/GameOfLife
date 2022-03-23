using System.IO;
using System.Text.Json;

namespace GameOfLife
{
    public class WorldFileSaver
    {
        private readonly World _world;
        private readonly string _relativePathToSaveFolderToSaves;

        public WorldFileSaver(World world, string relativePathToSaveFolder)
        {
            _world = world;
            _relativePathToSaveFolderToSaves = relativePathToSaveFolder;
        }

        public void SaveJsonLocal(string fileName)
        {
            var jsonString = JsonSerializer.Serialize(_world);
            var fileToWrite = new StreamWriter($@"{_relativePathToSaveFolderToSaves}/{fileName}.json");
            fileToWrite.WriteLine(jsonString);
            fileToWrite.Close();
        }
    }
}
