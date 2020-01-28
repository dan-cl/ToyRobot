using System;
using ToyRobot.App;

namespace ToyRobot.App
{
    public interface IRobot
    {
        int[] Position { get; set; }
        string Heading { get; set; }
        bool Placed { get; set; }
        string Report();
        void TurnLeft();
        void TurnRight();
    }

    public class Robot : IRobot
    {
        public int[] Position { get; set; }
        public string Heading { get; set; }
        public bool Placed { get; set; }

        public Robot()
        {
            Placed = false;
        }

        public string Report()
        {
            return Placed ? $"{Position[0]},{Position[1]},{Heading}" : "Robot has not been placed";
        }

        public void TurnLeft()
        {

        }

        public void TurnRight()
        {

        }

    }
}
