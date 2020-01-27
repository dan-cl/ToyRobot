using System;
using ToyRobot.App;

namespace ToyRobot.App
{
    public class Table
    {
        private int _xAxisLimit;
        private int _yAxisLimit;

        public Table()
        {
            _xAxisLimit = 5;
            _yAxisLimit = 5;
        }

        public bool Move(Robot robot)
        {
            return true;
        }

        public bool Place(int[] position, string heading, Robot robot)
        {
            return true;
        }
    }
}
