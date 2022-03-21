using System.IO;
using System.Text;
using System.Text.Json;

namespace GameOfLife
{
    public class WorldFileReader
    {
        private readonly string _relativePathToSaveFolder;

        public WorldFileReader(string relativePathToSaveFolder)
        {
            _relativePathToSaveFolder = relativePathToSaveFolder;
        }

        public World LoadJsonLocal(string fileName)
        {
            var sb = new StringBuilder();
            var path = $"{_relativePathToSaveFolder}/{fileName}.json";

            using (var reader = new StreamReader($@"{path}"))
            {
                while (!reader.EndOfStream)
                {
                    var lineToAdd = reader.ReadLine();
                    sb.Append(lineToAdd);
                }
            }

            var jsonString = sb.ToString();
            var world = JsonSerializer.Deserialize<World>(jsonString);
            return world;
        }
    }
}
