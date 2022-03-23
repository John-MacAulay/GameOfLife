namespace GameOfLife
{
    public class TwoDimensionalWorldPositionValidator
    {
        private readonly World _world;
        private const int WorldDimensionalParameters = 2;

        public TwoDimensionalWorldPositionValidator(World world)
        {
            _world = world;
        }

        public Position ValidatedPosition { get; private set; }

        public bool TryParseStringToPosition(string toCheck)
        {
            ValidatedPosition = null;
            var splitString = toCheck.Split(',');
            if (splitString.Length != WorldDimensionalParameters) return false;
            
            var potentialColumnOk = int.TryParse(splitString[0], out var potentialColumn);
            var potentialRowOk = int.TryParse(splitString[1], out var potentialRow);
            
            if (!potentialColumnOk || !potentialRowOk) return false;
            if (potentialRow < 0 || potentialRow > _world.Height - 1) return false;
            if (potentialColumn < 0 || potentialColumn > _world.Length - 1) return false;
            
            ValidatedPosition = new Position(potentialColumn, potentialRow);
            return true;
        }
    }
}
