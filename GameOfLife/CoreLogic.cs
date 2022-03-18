using System.Threading;

namespace GameOfLife
{
    public class CoreLogic
    {
        private readonly IInput _input;
        private readonly Display _display;
        private readonly IOutput _output;
        private  World _world;

        public CoreLogic(IOutput output, IInput input)
        {
            _input = input;
            _output = output;
            _display = new Display(_output);
        }

        public void StartLogic()
        {
            var worldGenerator = new WorldGenerator(_output, _input);
            _display.OfferChoiceForGeneratingWorld();
            var load = _input.GetText().ToLower();
            if (load != "l")
            {
                _world = worldGenerator.GetWorldFromManualInputs();
                
            }
            else
            {
                var reader = new WorldFileReader();
                _world =  reader.LoadJsonLocal("OfficialWorldSave");
                
            }

            PlayGame();
        }
        public void PlayGame()
        {
            _display.ShowWorld(_world);
            var generations = new GenerationProducer(_world);
            while (!_world.IsEmpty())
            {
                generations.MakeNextGeneration();
                Thread.Sleep(1000);
                _display.ShowWorld(_world);
            }
        }
    }
}
