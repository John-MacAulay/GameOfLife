using System;

namespace GameOfLife
{
    class Program
    {
        private const string PathToSaveGameFolder = @"..//..//..//..//./SavedWorlds";
        static void Main(string[] args)
        {
            var input = new ConsoleInput();
            var output = new ConsoleOutput();
            var core = new CoreLogic(output, input, 1000,  PathToSaveGameFolder );
            core.PlayGame();
        }
        
    }
}
