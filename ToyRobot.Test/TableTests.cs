
using Moq;
using ToyRobot.App;
using Xunit;

namespace ToyRobot.Test
{
    public class TableTests
    {
        [Fact]
        public void Place_GivenValidPosition_ReturnsTrue()
        {
            //Arrange
            var table = new Table();
            var position = new int[] { 0, 0 };
            var robotMock = SetupRobotMock();

            //Act
            var result = table.Place(position, robotMock.Object);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void Place_GivenValidPosition_SetsRobotPosition()
        {
            //Arrange
            var table = new Table();
            var position = new int[] { 0, 0 };
            var robotMock = SetupRobotMock();

            //Act
            table.Place(position, robotMock.Object);
            var result = robotMock.Object.Position;

            //Assert
            Assert.Equal(position, result);
        }

        [Fact]
        public void Place_GivenValidPosition_FlagsRobotAsPlaced()
        {
            //Arrange
            var table = new Table();
            var position = new int[] { 0, 0 };
            var robotMock = SetupRobotMock();

            //Act
            table.Place(position, robotMock.Object);

            //Assert
            Assert.True(robotMock.Object.Placed);
        }



        [Fact]
        public void Place_GivenInvalidPosition_ReturnsFalse()
        {
            //Arrange
            var table = new Table();
            var position = new[] { 10, 10 };
            var robotMock = new Mock<IRobot>();

            //Act
            var result = table.Place(position, robotMock.Object);

            //Assert
            Assert.False(result);
        }

        [Fact]
        public void Place_GivenInvalidPosition_DoesNotSetRobotPosition()
        {
            //Arrange
            var table = new Table();
            var position = new[] { 10, 10 };
            var robotMock = new Mock<IRobot>();

            //Act
            table.Place(position, robotMock.Object);
            var result = robotMock.Object.Position;

            //Assert
            Assert.NotEqual(position, result);
        }

        [Fact]
        public void Place_GivenInvalidPosition_DoesNotFlagRobotAsPlaced()
        {
            //Arrange
            var table = new Table();
            var position = new[] { 10, 10 };
            var robotMock = new Mock<IRobot>();

            //Act
            table.Place(position, robotMock.Object);

            //Assert;
            robotMock.Verify(x => x.Placed, Times.Never);
        }

        private Mock<IRobot> SetupRobotMock()
        {
            var robotMock = new Mock<IRobot>();
            robotMock.SetupAllProperties();
            return robotMock;
        }
    }
}
