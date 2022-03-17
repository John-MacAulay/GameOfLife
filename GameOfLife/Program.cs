using System;

namespace GameOfLife
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = new ConsoleInput();
            var output = new ConsoleOutput();
            var core = new CoreLogic(output, input);
            core.PlayGame();

        }

       
    }
}
