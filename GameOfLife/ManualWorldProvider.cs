namespace GameOfLife
{
    public class ManualWorldProvider: IWorldProvider
    {
        private readonly IInput _input;
        private readonly Display _display;
        private World _world;
        private readonly string _pathToSavedGamesFolder;
        
        public ManualWorldProvider(Display display, IInput input, string pathToSavedGamesFolder)
        {
            _input = input;
            _display = display;
            _pathToSavedGamesFolder = pathToSavedGamesFolder;
        }
        public World RetrieveWorld()
        {
            MakeWorldFromManualInputs();
            CheckAndSaveWorldIfRequired();
            return _world;
        }
        private void MakeWorldFromManualInputs()
        {
            var worldGenerator = new WorldGenerator(_display, _input);
            _world = worldGenerator.GetWorldFromManualInputs();
        }

        private void CheckAndSaveWorldIfRequired()
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
