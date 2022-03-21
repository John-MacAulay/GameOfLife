using System.Collections.Generic;
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

        public void WorldSourceOptions()
        {
            _display.OfferChoiceForGeneratingWorld();
            var load = _input.GetText().ToLower();
            if (load != "l")
            {
                var worldGenerator = new WorldGenerator(_output, _input);
                _world = worldGenerator.GetWorldFromManualInputs();
                
            }
            else
            {
                var fileNamesToDisplay = new List<string>();
                string [] savedGameFiles  = System.IO.Directory.GetFiles($@"..//..//..//..//./SavedWorlds", "*.json");
                foreach (var fileName in savedGameFiles)
                {
                   var fileNameShortened = fileName.Substring(30);
                   var stringLength = fileNameShortened.Length;
                   var finalName = fileNameShortened.Remove(stringLength - 5);
                   fileNamesToDisplay.Add(finalName); 
                }
                
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
