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
    }

    public class Robot : IRobot
    {
        public int[] Position { get; set; }
        public string Heading { get; set; }
        public bool Placed { get; set; }

        public string Report()
        {
            return $"{Position[0]},{Position[1]},{Heading}";
        }
    }
}
