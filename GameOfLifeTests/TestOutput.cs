using System.Collections.Generic;
using GameOfLife;

namespace GameOfLifeTests
{
    public class TestOutput : IOutput
    {
        public readonly List<string> FakeOutput = new();

        public void PrintText(string textToPrint)
        {
            FakeOutput.Add(textToPrint);
        }

        public void ClearDisplay()
        {
            FakeOutput.Add("The output was cleared at this point.");
        }
    }
}
