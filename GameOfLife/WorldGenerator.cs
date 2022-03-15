using System;

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

            var worldToReturn = new World(lengthAsInt, heightAsInt);
            return worldToReturn;
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
    }
}
