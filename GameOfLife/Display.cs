using System;

namespace GameOfLife
{
    public class Display
    {
        private readonly IOutput _output;

        public Display(IOutput output)
        {
            _output = output;
        }

        public void PromptForWorldParameter(string gridParameter)
        {
            _output.PrintText($"Please enter the grid {gridParameter} for this Game of Life.");
        }

        public void PromptForLiveCellSeedPosition()
        {
            _output.PrintText(
                $"Please enter the next position of a live cell within the bounds of your Game of Life grid. " +
                $"This should be entered as the row starting from 0, and the column starting from 0, seperated" +
                $"by a comma.  ie.   2,3  means there is a live cell seed at row 2, column 3. {Environment.NewLine}" +
                $"If you have finished entering live cells enter 'q' to quit");
        }
    }
}
