using System;

namespace GameOfLife.UserInteractions
{
    public class ConsoleOutput : IOutput
    {
        public void PrintText(string textToPrint)
        {
            Console.WriteLine(textToPrint);
        }

        public void ClearDisplay()
        {
            Console.Clear();
        }
    }
}
