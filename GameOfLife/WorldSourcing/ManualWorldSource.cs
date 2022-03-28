using GameOfLife.UserInteractions;
using GameOfLife.UserInterfaces;
using GameOfLife.WorldComponents;

namespace GameOfLife.WorldSourcing
{
    public class ManualWorldSource: IWorldSource
    {
        private readonly IInput _input;
        private readonly Display _display;
        private World _world;
        private readonly string _pathToSavedGamesFolder;
        
        public ManualWorldSource(Display display, IInput input, string pathToSavedGamesFolder)
        {
            _input = input;
            _display = display;
            _pathToSavedGamesFolder = pathToSavedGamesFolder;
        }
        public World RetrieveWorld()
        {
            MakeWorldFromManualInputs();
            SaveWorldIfRequired();
            return _world;
        }
        private void MakeWorldFromManualInputs()
        {
            var worldGenerator = new WorldGenerator(_display, _input);
            _world = worldGenerator.GetWorldFromManualInputs();
        }

        private void SaveWorldIfRequired()
        {
            _display.PromptForCheckIfSaveWorld();
            var response = _input.GetText().ToLower();
            if (response is not ("y" or "yes")) return;
            _display.PromptForSaveName();
            var nameToSaveUnder = _input.GetText();
            var saver = new WorldFileSaver(_world, _pathToSavedGamesFolder);
            saver.SaveJsonLocal(nameToSaveUnder);
        }
    }
}
