namespace ToyRobot.App.Map
{
    public class Map
    {
        private int _xAxisLimit;
        private int _yAxisLimit;

        public Map()
        {
            _xAxisLimit = 5;
            _yAxisLimit = 5;
        }

        public bool Move(int[] currentPosition, string direction, out int[] newPosition)
        {
            newPosition = new int[2];
            return true;
        }

    }
}
