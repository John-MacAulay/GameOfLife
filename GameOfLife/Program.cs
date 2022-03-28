using System;
using System.IO;
using GameOfLife.UserInteractions;

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
            var displayBeatTime = 1000;
            if (args.Length > 0)
            {
                if (int.TryParse(args[0], out displayBeatTime))
                {
                    if (displayBeatTime < 1)
                    {
                        displayBeatTime = 1000;
                    }
                }
            }

            var core = new CoreLogic(output, input, displayBeatTime, saveFolder);
            core.LogicRun();
        }
    }
}
