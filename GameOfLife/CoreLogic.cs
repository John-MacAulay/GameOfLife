
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
            World = _worldSource.RetrieveWorld();
            SaveWorldIfRequired();
            PlayGame();
        }

        public void ChooseWhereToSourceWorld()
        {
            _display.OfferChoiceForGeneratingWorld();
            var load = _input.GetText().ToLower();
             _worldSource =  load == "l"
                ? new SavedWorldSource(_display, _input, _pathToSaveFolder)
                : new WorldGenerator(_display, _input);
        }
        
        public void PlayGame()
        {
            _display.ShowWorld(World, _displayBeatTime);
            var producer = new GenerationProducer(World);
            while (!World.IsEmpty())
            {
                producer.MakeNextGeneration();
                _display.ShowWorld(World, _displayBeatTime);
                if (_input.CheckForBreak()) break;
            }
        } 
        
        
        private void SaveWorldIfRequired()
        {
            _display.PromptForCheckIfSaveWorld();
            var response = _input.GetText().ToLower();
            if (response is not ("y" or "yes")) return;
            _display.PromptForSaveName();
            var nameToSaveUnder = _input.GetText();
            var saver = new WorldFileSaver(World, _pathToSaveFolder);
            saver.SaveJsonLocal(nameToSaveUnder);
        }
        
    }
}
