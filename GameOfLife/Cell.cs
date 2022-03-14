namespace GameOfLife
{
    public class Cell
    {
        public Position Position { get; set; }
        
        public bool IsAlive { get; set; }

        public Cell( Position position)
        {
            Position = position;
        }
        
    }
}
