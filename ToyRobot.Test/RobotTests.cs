using ToyRobot.App;
using ToyRobot.App.Constants;
using Xunit;

namespace ToyRobot.Test
{
    public class RobotTests
    {
        [Fact]
        public void Report_BeforeRobotPlaced_ReturnsRobotNotPlacedMessage()
        {
            //Arrange
            var robot = new Robot();
            var expected = UserMessageConstants.RobotNotPlaced;

            //Act
            var result = robot.Report();

            //Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Report_AfterRobotPlaced_ReturnsRobotPositionAndHeading()
        {
            //Arrange
            var heading = HeadingConstants.North;
            var position = new[] {0, 0};

            var robot = new Robot
                {
                    Heading = heading,
                    Position = position,
                    Placed = true
                };

            var expected = $"{position[0]},{position[1]},{heading}";

            //Act
            var result = robot.Report();

            //Assert
            Assert.Equal(expected, result);
        }


        [Theory]
        [InlineData(HeadingConstants.North, HeadingConstants.West)]
        [InlineData(HeadingConstants.West, HeadingConstants.South)]
        [InlineData(HeadingConstants.South, HeadingConstants.East)]
        [InlineData(HeadingConstants.East, HeadingConstants.North)]

        public void TurnLeft_Turns90DegAnticlockwise(string currentHeading, string newHeading)
        {
            //Arrange
            var robot = new Robot()
            {
                Heading = currentHeading
            };

            var expected = newHeading;

            //Act
            robot.TurnLeft();
            var result = robot.Heading;

            //Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(HeadingConstants.North, HeadingConstants.East)]
        [InlineData(HeadingConstants.East, HeadingConstants.South)]
        [InlineData(HeadingConstants.South, HeadingConstants.West)]
        [InlineData(HeadingConstants.West, HeadingConstants.North)]

        public void TurnRight_Turns90DegClockwise(string currentHeading, string newHeading)
        {
            //Arrange
            var robot = new Robot()
            {
                Heading = currentHeading
            };

            var expected = newHeading;

            //Act
            robot.TurnRight();
            var result = robot.Heading;

            //Assert
            Assert.Equal(expected, result);
        }
    }
}
