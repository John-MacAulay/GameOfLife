using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;

namespace GameOfLife
{
    public class WorldGenerator
    {
        private readonly IInput _input;
        private readonly Display _display;

        public WorldGenerator(IOutput output, IInput input)
        {
            _input = input;
            _display = new Display(output);
        }

        public World GetWorldFromManualInputs()
        {
            var lengthAsInt = 0;
            var heightAsInt = 0;

            lengthAsInt = GetWorldParameter("length");
            heightAsInt = GetWorldParameter("height");

            var newlyCreatedWorld = new World(lengthAsInt, heightAsInt);
            var worldWithLiveCells = ManuallyAddLiveCellPositions(newlyCreatedWorld);
            return worldWithLiveCells;
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

        private World ManuallyAddLiveCellPositions(World world)
        {
            var positionsForLiveCells = new List<Position>();
            var NewPositionString = "";
            while (NewPositionString != "q")
            {
                NewPositionString = AddNewLiveCellPositionFromUserInput(world, positionsForLiveCells);
            }

            return world;
        }

        private string AddNewLiveCellPositionFromUserInput(World world, List<Position> positionsForLiveCells)
        {
            string response;
            _display.PromptForLiveCellSeedPosition();
            response = _input.GetText();
            var worldPositionValidator = new WorldPositionValidator(world);
            if (worldPositionValidator.TryParseStringResponseToPosition(response))
            {
                positionsForLiveCells.Add(worldPositionValidator.ReturnValidatedPosition);
            }

            return response;
        }
    }
}
