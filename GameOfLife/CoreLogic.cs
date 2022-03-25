using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace GameOfLife
{
    public class CoreLogic
    {
        private readonly IInput _input;
        private readonly Display _display;
        private readonly int _displayMillisecondSleep;
        private readonly IWorldProvider _worldProvider;
        private World _world;


        public CoreLogic(IOutput output, IInput input, int displayMillisecondSleep,
            IWorldProvider worldProvider)
        {
            _input = input;
            _display = new Display(output);
            _displayMillisecondSleep = displayMillisecondSleep;
            _worldProvider = worldProvider;
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
                if (Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Q) break;
            }
        } 
        
    }
}
