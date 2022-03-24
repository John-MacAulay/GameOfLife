using System;
using System.IO;

namespace GameOfLife
{
    class Program
    {
        private static readonly string SaveFolder = $@"/Users/John.MacAulay/Documents/GameOfLifeSaves";
        static void Main(string[] args)
        {
            Directory.CreateDirectory($@"/Users/John.MacAulay/Documents/GameOfLifeSaves");
            var input = new ConsoleInput();
            var output = new ConsoleOutput();
            var core = new CoreLogic(output, input, 1000,  SaveFolder );
            core.PlayGame();
        }
        
    }
}
