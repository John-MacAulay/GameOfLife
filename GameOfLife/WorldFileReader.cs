using System.IO;
using System.Text;
using System.Text.Json;

namespace GameOfLife
{
    public class WorldFileReader
    {
        private World _world;
        
        public WorldFileReader()
        {
            
        }

        public World LoadJsonLocal(string fileName)
        {
            var sb = new StringBuilder();
            string path = $"..//..//..//..//{fileName}.json";
            using (var reader = new StreamReader($@"{path}")) 
            {
                while (!reader.EndOfStream)
                {
                    var lineToAdd = reader.ReadLine();
                    sb.Append(lineToAdd);
                }
            }

            string jsonString = sb.ToString();
            _world  = JsonSerializer.Deserialize<World>(jsonString);
            return _world;
        }

    }
}
