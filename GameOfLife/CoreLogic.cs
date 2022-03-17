using System.Diagnostics.Tracing;

namespace GameOfLife
{
    public class CoreLogic
    {
        private readonly IInput _input;
        private readonly Display _display;
        private readonly IOutput _output;
            
        public CoreLogic(IOutput output, IInput input)
        {
            _input = input;
            _output = output;
            _display = new Display(_output);
        }

        public void PlayGame()
        {
            var worldGenerator = new WorldGenerator(_output, _input);
            var world = worldGenerator.GetWorldFromManualInputs();
            _display.ShowWorld(world);
            var generations = new GenerationProducer(world);
            while (!world.IsEmpty())
            {
                generations.MakeNextGeneration();
            }

        }
        
        
    }
}
