using System.IO;
using System.Text;
using System.Text.Json;

namespace GameOfLife
{
    public class WorldFileReader
    {
      
        
        public WorldFileReader()
        {
            
        }

        public World LoadJsonLocal(string fileName)
        {
            var sb = new StringBuilder();
            var path = $"..//..//..//..//./SavedWorlds/{fileName}.json";
            using (var reader = new StreamReader($@"{path}")) 
            {
                while (!reader.EndOfStream)
                {
                    var lineToAdd = reader.ReadLine();
                    sb.Append(lineToAdd);
                }
            }

            var jsonString = sb.ToString();
            var  world  = JsonSerializer.Deserialize<World>(jsonString);
            // var world = JsonSerializer.Deserialize(jsonString);
            return world;
        }

    }
}
