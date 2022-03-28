
using GameOfLife.UserInteractions;
using GameOfLife.WorldComponents;
using GameOfLife.WorldSourcing;

namespace GameOfLife
{
    public class CoreLogic
    {
        private readonly IInput _input;
        private readonly Display _display;
        private readonly int _displayBeatTime;
        private IWorldSource _worldSource;
        private World World { get; set; }
        private readonly string _pathToSaveFolder;


        public CoreLogic(IOutput output, IInput input, int displayBeatTime,
            string pathToSaveFolder)
        {
            _input = input;
            _display = new Display(output);
            _displayBeatTime = displayBeatTime;
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
             _worldSource =  load == "l"
                ? new SavedWorldSource(_display, _input, _pathToSaveFolder)
                : new ManualWorldSource(_display, _input, _pathToSaveFolder);
        }
        
        public void PlayGame()
        {
            World = _worldSource.RetrieveWorld();
            _display.ShowWorld(World, _displayBeatTime);
            var producer = new GenerationProducer(World);
            while (!World.IsEmpty())
            {
                producer.MakeNextGeneration();
                _display.ShowWorld(World, _displayBeatTime);
                if (_input.CheckForBreak()) break;
            }
        } 
        
    }
}
