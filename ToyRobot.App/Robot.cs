using System;
using ToyRobot.App;

namespace ToyRobot.App
{
    public class Robot
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
