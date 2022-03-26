
namespace GameOfLife
{
    public class CoreLogic
    {
        private readonly IInput _input;
        private readonly Display _display;
        private readonly int _displayMillisecondSleep;
        private IWorldProvider _worldProvider;
        private World _world;
        private readonly string _pathToSaveFolder;


        public CoreLogic(IOutput output, IInput input, int displayMillisecondSleep,
            string pathToSaveFolder)
        {
            _input = input;
            _display = new Display(output);
            _displayMillisecondSleep = displayMillisecondSleep;
            _pathToSaveFolder = pathToSaveFolder;
        }

        public void LogicRun()
        {
            ChooseWhereToSourceWorld();
            PlayGame();
        }

        public void ChooseWhereToSourceWorld()
        {
            _display.OfferChoiceForGeneratingWorld();
            var load = _input.GetText().ToLower();
             _worldProvider =  load == "l"
                ? new SavedWorldProvider(_display, _input, _pathToSaveFolder)
                : new ManualWorldProvider(_display, _input, _pathToSaveFolder);
        }
        
        public void PlayGame()
        {
            _world = _worldProvider.RetrieveWorld();
            _display.ShowWorld(_world, _displayMillisecondSleep);
            var producer = new GenerationProducer(_world);
            while (!_world.IsEmpty())
            {
                producer.MakeNextGeneration();
                _display.ShowWorld(_world, _displayMillisecondSleep);
                if (_input.CheckForBreak()) break;
            }
        } 
        
    }
}
