using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace GameOfLife
{
    public class CoreLogic
    {
        private readonly IInput _input;
        private readonly Display _display;
        private readonly IOutput _output;
        private readonly int _displayMillisecondSleep;
        private readonly string _pathToSavedGamesFolder;
        private World _world;


        public CoreLogic(IOutput output, IInput input, int displayMillisecondSleep, string pathToSavedGamesFolder)
        {
            
            _input = input;
            _output = output;
            _display = new Display(_output);
            _displayMillisecondSleep = displayMillisecondSleep;
            _pathToSavedGamesFolder = pathToSavedGamesFolder;
        }

        public void PlayGame()
        {
            var worldProvider = new WorldProvider(_output, _input, _pathToSavedGamesFolder);
            _world = worldProvider.RetrieveWorld();
            
            _display.ShowWorld(_world, _displayMillisecondSleep);
            var generations = new GenerationProducer(_world);
            while (!_world.IsEmpty())
            {
                generations.MakeNextGeneration();
                _display.ShowWorld(_world, _displayMillisecondSleep);
            }
        }
        
    }
}
