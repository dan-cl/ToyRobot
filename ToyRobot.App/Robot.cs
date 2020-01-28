using System.Collections.Generic;
using System.Linq;
using ToyRobot.App.Constants;

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

        private List<string> _compass;

        public Robot()
        {
            Placed = false;
            SetupCompass();
        }

        public string Report()
        {
            return Placed ? $"{Position[0]},{Position[1]},{Heading}" : UserMessageConstants.RobotNotPlaced;
        }

        public void TurnLeft()
        {
            var currentCompassIndex = GetCurrentCompassIndex(Heading);
            Heading = currentCompassIndex == 0 ? _compass.Last() : _compass[currentCompassIndex - 1];
        }

        public void TurnRight()
        {
            var currentCompassIndex = GetCurrentCompassIndex(Heading);
            Heading = currentCompassIndex == (_compass.Count - 1) ? _compass.First() : _compass[currentCompassIndex + 1];
        }

        private void SetupCompass()
        {
            _compass = new List<string>
            {
                HeadingConstants.North,
                HeadingConstants.East,
                HeadingConstants.South,
                HeadingConstants.West
            };
        }

        private int GetCurrentCompassIndex(string heading)
        {
            return _compass.FindIndex(x => x.StartsWith(heading));
        }
    }
}
