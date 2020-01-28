using System.Collections.Generic;
using System.Linq;
using Moq;
using ToyRobot.App;
using ToyRobot.App.Constants;
using Xunit;

namespace ToyRobot.Test
{
    public class TableTests
    {
        public class PlaceTests : TableTests
        {
            [Fact]
            public void Place_GivenValidPosition_ReturnsTrue()
            {
                //Arrange
                var table = new Table();
                var position = new int[] { 0, 0 };
                var robotMock = new Mock<IRobot>();

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
                var robotMock = new Mock<IRobot>();
                robotMock.SetupAllProperties();

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
                var robotMock = new Mock<IRobot>();
                robotMock.SetupAllProperties();

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
                robotMock.SetupAllProperties();

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
                robotMock.SetupAllProperties();

                //Act
                table.Place(position, robotMock.Object);

                //Assert;
                robotMock.Verify(x => x.Placed, Times.Never);
            }
        }

        public class MoveTests : TableTests
        {
            [Theory]
            [MemberData(nameof(GetValidMoveTestDataWithOutFinishPosition))]
            public void Move_WithinTheBoundsOfTheTable_ReturnsTrue(int[] startPosition, string heading)
            {
                //Arrange
                var table = new Table();
                var robotMock = SetupRobotMock(startPosition, true, heading);

                //Act
                var result = table.Move(robotMock.Object);

                //Assert
                Assert.True(result); ;
            }

            [Theory]
            [MemberData(nameof(GetValidMoveTestDataWithFinishPosition))]
            public void Move_WithinTheBoundsOfTheTable_MovesRobot(int[] startPosition, string heading, int[]endPosition)
            {
                //Arrange
                var table = new Table();
                var robotMock = SetupRobotMock(startPosition, true, heading);
                var expected = endPosition;

                //Act
                table.Move(robotMock.Object);

                //Assert
                Assert.True(expected.SequenceEqual(robotMock.Object.Position));;
            }

            [Theory]
            [MemberData(nameof(GetInvalidMoveTestData))]
            public void Move_OutsideTheBoundsOfTheTable_ReturnsFalse(int[] startPosition, string heading)
            {
                //Arrange
                var table = new Table();
                var robotMock = SetupRobotMock(startPosition, true, heading);

                //Act
                var result = table.Move(robotMock.Object);

                //Assert
                Assert.False(result);
            }

            [Theory]
            [MemberData(nameof(GetInvalidMoveTestData))]
            public void Move_OutsideTheBoundsOfTheTable_DoesNotMovesRobot(int[] startPosition, string heading)
            {
                //Arrange
                var table = new Table();
                var robotMock = SetupRobotMock(startPosition, true, heading);
                var expected = startPosition;

                //Act
                table.Move(robotMock.Object);

                //Assert
                Assert.True(expected.SequenceEqual(robotMock.Object.Position)); ;
            }




            public static IEnumerable<object[]> GetValidMoveTestDataWithFinishPosition =>
                new List<object[]>
                {
                    new object[] { new[]{0, 0}, HeadingConstants.North, new[]{0, 1} },
                    new object[] { new[]{0, 0}, HeadingConstants.East,  new[]{1, 0} },
                    new object[] { new[]{0, 1}, HeadingConstants.South, new[]{0, 0} },
                    new object[] { new[]{1, 0}, HeadingConstants.West,  new[]{0, 0} }
                };

            public static IEnumerable<object[]> GetValidMoveTestDataWithOutFinishPosition =>
                new List<object[]>
                {
                    new object[] { new[]{0, 0}, HeadingConstants.North},
                    new object[] { new[]{0, 0}, HeadingConstants.East},
                    new object[] { new[]{0, 1}, HeadingConstants.South},
                    new object[] { new[]{1, 0}, HeadingConstants.West}
                };


            public static IEnumerable<object[]> GetInvalidMoveTestData()
            {
                var xCoordinateMaxValue = TableSizeConstants.TableSize;
                var yCoordinateMaxValue = TableSizeConstants.TableSize;

                return new List<object[]>
                {
                    new object[] { new[] {0, yCoordinateMaxValue}, HeadingConstants.North},
                    new object[] { new[] {xCoordinateMaxValue, 0}, HeadingConstants.East},
                    new object[] { new[] {0, 0}, HeadingConstants.South},
                    new object[] { new[] {0, 0}, HeadingConstants.West}
                };

                
            }

            private static Mock<IRobot> SetupRobotMock(int[] position, bool placed, string heading)
            {
                var robotMock = new Mock<IRobot>();
                robotMock.SetupAllProperties();
                robotMock.Object.Position = position;
                robotMock.Object.Heading = heading;
                robotMock.Object.Placed = placed;
                return robotMock;
            }
        }
    }
}
