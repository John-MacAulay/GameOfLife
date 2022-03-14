namespace GameOfLife
{
    public class CoreLogic
    {
        private readonly IInput _input;
        private readonly Display _display;
            
        public CoreLogic(IOutput output, IInput input)
        {
            _input = input;
            _display = new Display(output);
        }
        
        
    }
}
