using System;
using System.Collections.Generic;
using System.Linq;
using GameOfLife.UserInterfaces;
using GameOfLife.WorldComponents;

namespace GameOfLife.WorldSourcing
{
    public class SavedWorldSource : IWorldSource
    {
        private readonly IInput _input;
        private readonly Display _display;
        private World _world;
        private readonly string _pathToSavedGamesFolder;

        public SavedWorldSource(Display display, IInput input, string pathToSavedGamesFolder)
        {
            _input = input;
            _display = display;
            _pathToSavedGamesFolder = pathToSavedGamesFolder;
        }
        public World RetrieveWorld()
        {
            var fileNamesToDisplay = RetrieveFileNamesToDisplay();
            var saveGameNumber = 0;
            saveGameNumber = LetUserChooseSavedFileToLoad(saveGameNumber, fileNamesToDisplay);
            var reader = new WorldFileLoader(_pathToSavedGamesFolder);
            _world = reader.LoadJsonLocal(fileNamesToDisplay[saveGameNumber - 1]);
            return _world;
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
