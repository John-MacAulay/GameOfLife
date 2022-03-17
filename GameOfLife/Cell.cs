
namespace GameOfLife
{
    public class Cell
    {
        public int NumberOfLiveNeighbours { get; set; }
        public Position Position { get; set; }
        
        public bool IsAlive { get; set; }

        public Cell( Position position)
        {
            Position = position;
        }

        private void DefineNeighbourCells(World world)
        {
            // list of neighbour cells should be eight
            // defined by position row -1 ( column -1, colum = , column +1) 
            // defined by position row =  ( column -1 , column +1)
            // defined by position row + 1 ( column -1, colum = , column +1)
            // then check for wrapping 
            
        }
    }
}
