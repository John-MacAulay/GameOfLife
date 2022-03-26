using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using GameOfLife.UserInterfaces;
using GameOfLife.WorldComponents;

namespace GameOfLife
{
    public class Display
    {
        private readonly IOutput _output;
        private readonly string _middleDot = char.ConvertFromUtf32(0x000000B7);
        private readonly string _littleCircle = '\u25e6'.ToString();
        private readonly string _bigCircle = '\u25c9'.ToString();
        private readonly string _openCircle = '\u25cb'.ToString();

        public Display(IOutput output)
        {
            _output = output;
        }

        public void PromptForWorldParameter(string gridParameter)
        {
            _output.PrintText($"Please enter the grid {gridParameter} for this Game of Life.");
        }

        public void PromptForLiveCellSeedPosition()
        {
            _output.PrintText(
                $"Please enter the next position of a live cell within the bounds of your Game of Life grid. " +
                $"This should be entered as the row starting from 0, and the column starting from 0, seperated {Environment.NewLine}" +
                $"by a comma.  ie.   2,3  means there is a live cell seed at column 2, row 3. {Environment.NewLine}" +
                $"If you have finished entering live cells enter 'q' to quit");
        }

        public void ShowWorld(World world, int millisecondsSleep)
        {
            var displayPhases = new List<string>()
            {
                _littleCircle,
                _bigCircle,
                _openCircle
            };
            foreach (var phase in displayPhases)
            {
                _output.ClearDisplay();
                var worldAsGrid = new StringBuilder();
                worldAsGrid.Append($"{Environment.NewLine}");
                worldAsGrid.Append(
                    $" Generation {world.CurrentGenerationNumber} {Environment.NewLine}{Environment.NewLine}");

                for (var rowPosition = 0; rowPosition < world.Height; rowPosition++)
                {
                    for (var columnPosition = 0; columnPosition < world.Length; columnPosition++)
                    {
                        var position = new Position(columnPosition, rowPosition);
                        var cellToDefineDisplay = world.Cells.First(cell => cell.Position == position);
                        worldAsGrid.Append(cellToDefineDisplay.IsAlive ? $" {phase}" : $" {_middleDot}");
                    }

                    worldAsGrid.Append($"{Environment.NewLine}");
                }

                if (world.Periodicity != null)
                {
                    worldAsGrid.Append($"{Environment.NewLine} Periodicity {world.Periodicity} {Environment.NewLine}");
                }
                worldAsGrid.Append($"{Environment.NewLine} Press 'q' to quit.");
                _output.PrintText(worldAsGrid.ToString());
                Thread.Sleep(millisecondsSleep / displayPhases.Count );
            }
        }

        public void OfferChoiceForGeneratingWorld()
        {
            _output.ClearDisplay();
            _output.PrintText("Please enter 'l' to load a file from disc," +
                              " all other answers will default to manual generation of world");
        }

        public void PromptForCheckIfSaveWorld()
        {
            _output.PrintText($"{Environment.NewLine} Would you like to save this world set up? {Environment.NewLine}" +
                              $" (y or yes to save, any other key for no.) ");
        }

        public void PromptForSaveName()
        {
            _output.PrintText(
                $"{Environment.NewLine} Please enter the file name you would like this world saved under.");
        }

        public void ShowSavedGameFiles(List<string> fileNamesToDisplay)
        {
            var numberSavedFiles = 0;
            _output.PrintText($" Game Saved Files available to load: {Environment.NewLine}");
            foreach (var file in fileNamesToDisplay)
            {
                numberSavedFiles++;
                _output.PrintText($" {numberSavedFiles}. {file}");
            }
        }

        public void PromptForSaveToLoad()
        {
            _output.PrintText($" Please enter the number of the saved game file you wish to load.");
        }
    }
}
