namespace GameOfLife
{
    public class Display
    {
        private readonly IOutput _output;

        public Display(IOutput output)
        {
            _output = output;
        }
    }
}
