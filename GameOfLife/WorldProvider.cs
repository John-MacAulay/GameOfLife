using System;
using System.Collections.Generic;
using System.Linq;

namespace GameOfLife
{
    public class WorldProvider
    {
        private readonly IInput _input;
        private readonly Display _display;
        private readonly IOutput _output;
        private World _world;
        private readonly string _pathToSavedGamesFolder;


        public WorldProvider(IOutput output, IInput input, string pathToSavedGamesFolder)
        {
            _input = input;
            _output = output;
            _display = new Display(_output);
            _pathToSavedGamesFolder = pathToSavedGamesFolder;
        }

        public World RetrieveWorld()
        {
            _display.OfferChoiceForGeneratingWorld();
            var load = _input.GetText().ToLower();
            if (load != "l")
            {
                ChooseWorldFromManualInputs();
                CheckAndSaveWorldIfRequired();
            }
            else
            {
                ChooseWorldFromSavedWorlds();
            }

            return _world;
        }

        private void ChooseWorldFromManualInputs()
        {
            var worldGenerator = new WorldGenerator(_output, _input);
            _world = worldGenerator.GetWorldFromManualInputs();
        }

        private void ChooseWorldFromSavedWorlds()
        {
            var fileNamesToDisplay = RetrieveFileNamesToDisplay();
            var saveGameNumber = 0;
            saveGameNumber = LetUserChooseSavedFileToLoad(saveGameNumber, fileNamesToDisplay);
            var reader = new WorldFileLoader(_pathToSavedGamesFolder);
            _world = reader.LoadJsonLocal(fileNamesToDisplay[saveGameNumber - 1]);
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

        private List<string> RetrieveFileNamesToDisplay()
        {
            var savedGameFiles = System.IO.Directory.GetFiles(_pathToSavedGamesFolder, "*.json");

            return (from filename in savedGameFiles let indexToCutAt =
                filename.LastIndexOf("/", StringComparison.Ordinal) select filename[(indexToCutAt + 1)..]
                into trimmedFile select trimmedFile.Remove(trimmedFile.Length - 5)).ToList();
        }

        private int LetUserChooseSavedFileToLoad(int saveGameNumber, List<string> fileNamesToDisplay)
        {
            var choiceIsInt = false;
            while (!choiceIsInt || !(saveGameNumber > 0 && saveGameNumber <= fileNamesToDisplay.Count))
            {
                _display.ShowSavedGameFiles(fileNamesToDisplay);
                _display.PromptForSaveToLoad();
                var choice = _input.GetText();
                choiceIsInt = int.TryParse(choice, out saveGameNumber);
            }

            return saveGameNumber;
        }
    }
}
