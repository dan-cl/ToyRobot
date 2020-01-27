using System;
using System.Linq;


namespace ToyRobot.App
{
    public class Table
    {
        private readonly int _tableSize;

        public Table()
        {
            _tableSize = 5;
        }

        public bool Move(Robot robot)
        {
            return true;
        }

        public bool Place(int[] position, IRobot robot)
        {
            if (!ValidPosition(position)) return false;

            robot.Position = position;
            robot.Placed = true;
            return true;
        }
         
        private bool ValidPosition(int[] position)
        {
            return position.All(value => value >= 0 && _tableSize >= value);
        }
    }
}
