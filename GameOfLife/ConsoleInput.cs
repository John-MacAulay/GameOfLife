using System;

namespace GameOfLife
{
    public class ConsoleInput : IInput
    {
        public string GetText()
        {
            var needThis = Console.ReadLine();
            return needThis;
        }

        public bool CheckForBreak()
        { 
           return  Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Q;
        }
    }
}
