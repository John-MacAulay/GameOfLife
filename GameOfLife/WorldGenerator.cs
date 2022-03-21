using System.Collections.Generic;
using System.Linq;


namespace GameOfLife
{
    public class WorldGenerator
    {
        private readonly IInput _input;
        private readonly Display _display;
        private World World { get; set; }

        public WorldGenerator(IOutput output, IInput input)
        {
            _input = input;
            _display = new Display(output);
        }

        public World GetWorldFromManualInputs()
        {
            var lengthAsInt = GetWorldParameter("length");
            var heightAsInt = GetWorldParameter("height");

            World = new World(lengthAsInt, heightAsInt);
            ManuallyAddLiveCellPositions();
            // CheckAndSaveWorldIfRequired();
            return World;
        }

        private void CheckAndSaveWorldIfRequired()
        {
            _display.PromptForCheckIfSaveWorld();
            var response = _input.GetText().ToLower();
            if (response is not ("y" or "yes")) return;
            _display.PromptForSaveName();
            var nameToSaveUnder = _input.GetText();
            var saver = new WorldFileSaver(World, $@"..//..//..//..//./SavedWorlds/");
            saver.SaveJsonLocal(nameToSaveUnder);
        }

        private int GetWorldParameter(string prompt)
        {
            var requiredInt = 0;
            var success = false;
            while (!success)
            {
                _display.PromptForWorldParameter(prompt);
                var response = _input.GetText();
                success = int.TryParse(response, out requiredInt);
            }

            return requiredInt;
        }

        private void ManuallyAddLiveCellPositions()
        {
            var positionsForLiveCells = new List<Position>();
            var userInputForPosition = "";
            while (userInputForPosition != "q")
            {
                userInputForPosition = AddNewLiveCellPositionFromUserInput(World, positionsForLiveCells);
            }

            foreach (var cellToMakeLive in positionsForLiveCells.Select(position =>
                         World.CellAtThisWorldPosition(position)))

            {
                cellToMakeLive.IsAlive = true;
            }
        }

        private string AddNewLiveCellPositionFromUserInput(World world, List<Position> positionsForLiveCells)
        {
            _display.PromptForLiveCellSeedPosition();
            var userInputForPosition = _input.GetText();
            var worldPositionValidator = new WorldPositionValidator(world);
            if (worldPositionValidator.TryParseStringToPosition(userInputForPosition))
            {
                positionsForLiveCells.Add(worldPositionValidator.ValidatedPosition);
            }

            return userInputForPosition;
        }
    }
}
