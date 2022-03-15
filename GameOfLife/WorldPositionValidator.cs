namespace GameOfLife
{
    public class WorldPositionValidator
    {
        private readonly World _world;
        private const int WorldDimensionalParameters = 2;

        public WorldPositionValidator(World world)
        {
            _world = world;
        }

        public Position ReturnValidatedPosition { get; private set; }

        public bool TryParseStringResponseToPosition(string toCheck)
        {
            var splitString = toCheck.Split(',');
            if (splitString.Length != WorldDimensionalParameters)
            {
                return false;
            }

            var potentialColumnOK = int.TryParse(splitString[0], out var potentialColumn);
            var potentialRowOK = int.TryParse(splitString[1], out var potentialRow);
            if (!potentialColumnOK || !potentialRowOK)
            {
                return false;
            }

            if (potentialRow < 0 || potentialRow > _world.Height-1)
            {
                return false;
            }

            if (potentialColumn < 0 || potentialColumn > _world.Length-1)
            {
                return false;
            }

            ReturnValidatedPosition = new Position(potentialRow, potentialColumn);

            return true;
        }
    }
}
