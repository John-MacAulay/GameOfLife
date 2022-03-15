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
    }
}
