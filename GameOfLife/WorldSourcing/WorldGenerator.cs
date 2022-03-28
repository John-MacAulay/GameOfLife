using System.Collections.Generic;
using System.Linq;
using GameOfLife.UserInteractions;
using GameOfLife.WorldComponents;

namespace GameOfLife.WorldSourcing
{
    public class WorldGenerator : IWorldSource
    {
        private readonly IInput _input;
        private readonly Display _display;
        private World World { get; set; }

        public WorldGenerator(Display display, IInput input)
        {
            _input = input;
            _display = display;
        }

        public World RetrieveWorld()
        {
            var lengthAsInt = GetWorldParameter("length");
            var heightAsInt = GetWorldParameter("height");

            World = new World(lengthAsInt, heightAsInt);
            ManuallyAddLiveCellPositions();
            return World;
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
                if (requiredInt < 1)
                {
                    success = false;
                }
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
                cellToMakeLive.MakeCellAlive();
            }
        }

        private string AddNewLiveCellPositionFromUserInput(World world, ICollection<Position> positionsForLiveCells)
        {
            _display.PromptForLiveCellSeedPosition();

            var userInputForPosition = _input.GetText();
            var worldPositionValidator = new TwoDimensionalWorldPositionValidator(world);
            if (worldPositionValidator.TryParseStringToPosition(userInputForPosition))
            {
                positionsForLiveCells.Add(worldPositionValidator.ValidatedPosition);
            }

            return userInputForPosition;
        }
    }
}
