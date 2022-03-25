using System;
using System.IO;

namespace GameOfLife
{
    class Program
    {
 
        static void Main(string[] args)
        {
            var folderPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var saveFolder = $"{folderPath}/Documents/GameOfLifeSaves";
            Directory.CreateDirectory($@"{saveFolder}");
            
            var input = new ConsoleInput();
            var output = new ConsoleOutput();
            var worldProvider = new WorldProvider(output, input, saveFolder);
            var displayTime = 1000;
            if (args.Length > 0 )
            {
                if (int.TryParse(args[0], out  displayTime))
                {
                    if (displayTime < 1)
                    {
                        displayTime = 1000;
                    }
                }
            }

            var core = new CoreLogic(output, input, displayTime, worldProvider);
            core.PlayGame();
        }
    }
}
