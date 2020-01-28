using System.Collections.Generic;
using Moq;
using ToyRobot.App;
using ToyRobot.App.Constants;
using ToyRobot.App.Validators;
using Xunit;

namespace ToyRobot.Test
{
    public class InputValidatorTests
    {
        [Theory]
        [InlineData("Foo")]
        [InlineData("Bar")]
        [InlineData("Foo Bar")]
        [InlineData("Place")]
        [InlineData("PLACE 0")]
        [InlineData("PLACE 0,")]
        [InlineData("PLACE 0,0")]
        [InlineData("PLACE 0,0,")]
        [InlineData("PLACE 0,0,LEFT")]
        [InlineData("PLACE ,0,0,LEFT")]
        [InlineData("Place0,0,North")]
        [InlineData("Place 0,x,North")]
        [InlineData("Place x,0,North")]
        [InlineData("Place 0,0,Down")]
        public void ValidateInput_GivenUnknownCommand_ReturnsUnknownCommandMessage(string unknownCommand)
        {
            //Arrange
            var inputValidator = GetInputValidator();

            //Act
            var result = inputValidator.ValidateInput(unknownCommand);

            //Assert
            Assert.Equal(result, UserMessageConstants.UnknownCommand);
        }

        [Theory]
        [MemberData(nameof(GetValidPlaceCommands))]
        public void ValidateInput_GivenValidPlaceCommand_ReturnsRobotPlacedMessage(string placeCommand, string xCoord, string yCoord, string heading)
        {
            //Arrange
            var tableMock = GetTableMock();
            var robotMock = GetRobotMock();
            tableMock.Setup(x => x.Place(It.IsAny<int[]>(), It.IsAny<IRobot>())).Returns(true);
            var inputValidator = GetInputValidator(robotMock.Object, tableMock.Object);
            var expected = $"Robot placed at {xCoord}, {yCoord} facing {heading.ToUpper()}";

            //Act
            var result = inputValidator.ValidateInput($"{placeCommand} {xCoord},{yCoord},{heading}");

            //Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [MemberData(nameof(GetValidPlaceCommands))]
        public void ValidateInput_GivenValidPlaceCommand_SetsTheRobotHeading(string placeCommand, string xCoord, string yCoord, string heading)
        {
            //Arrange
            var tableMock = GetTableMock();
            var robotMock = GetRobotMock();
            var inputValidator = GetInputValidator(robotMock.Object, tableMock.Object);

            //Act
            inputValidator.ValidateInput($"{placeCommand} {xCoord},{yCoord},{heading}");

            //Assert
            robotMock.VerifySet(x => x.Heading = heading.ToUpper());
        }

        [Theory]
        [MemberData(nameof(GetValidPlaceCommands))]
        public void ValidateInput_GivenValidPlaceCommand_PlacesTheRobot(string placeCommand, string xCoord, string yCoord, string heading)
        {
            //Arrange
            var tableMock = GetTableMock();
            var robotMock = GetRobotMock();
            var inputValidator = GetInputValidator(robotMock.Object, tableMock.Object);
            int.TryParse(xCoord, out int x);
            int.TryParse(yCoord, out int y);

            var position = new int[] {x, y};
            //Act
            inputValidator.ValidateInput($"{placeCommand} {xCoord},{yCoord},{heading}");


            //Assert
            tableMock.Verify(x => x.Place(position, robotMock.Object), Times.Once());
        }

        [Theory]
        [InlineData("move")]
        [InlineData("MOVE")]
        public void ValidateInput_GivenMoveCommand_MovesTheRobot(string moveCommand)
        {
            //Arrange
            var tableMock = GetTableMock();
            var robotMock = GetRobotMock();
            var inputValidator = GetInputValidator(robotMock.Object, tableMock.Object);

            //Act
            inputValidator.ValidateInput(moveCommand);

            //Assert
            tableMock.Verify(x => x.Move(It.IsAny<IRobot>()), Times.Once());
        }

        [Theory]
        [InlineData("move")]
        [InlineData("MOVE")]
        public void ValidateInput_GivenMoveCommand_ReturnsRobotMovedMessage(string moveCommand)
        {
            //Arrange
            var inputValidator = GetInputValidator();
            var expected = UserMessageConstants.RobotMoved;

            //Act
            var result = inputValidator.ValidateInput(moveCommand);

            //Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("left")]
        [InlineData("LEFT")]
        public void ValidateInput_GivenLeftCommand_TurnsRobotLeft(string leftCommand)
        {
            //Arrange
            var tableMock = GetTableMock();
            var robotMock = GetRobotMock();
            var inputValidator = GetInputValidator(robotMock.Object, tableMock.Object);

            //Act
            inputValidator.ValidateInput(leftCommand);

            //Assert
            robotMock.Verify(x => x.TurnLeft(), Times.Once());
        }

        [Theory]
        [InlineData("left")]
        [InlineData("LEFT")]
        public void ValidateInput_GivenLeftCommand_ReturnsTurnedLeftMessage(string leftCommand)
        {
            //Arrange
            var inputValidator = GetInputValidator();
            var expected = UserMessageConstants.TurnedLeft;

            //Act
            var result = inputValidator.ValidateInput(leftCommand);

            //Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("right")]
        [InlineData("RIGHT")]
        public void ValidateInput_GivenRightCommand_TurnsRobotRight(string rightCommand)
        {
            //Arrange
            var tableMock = GetTableMock();
            var robotMock = GetRobotMock();
            var inputValidator = GetInputValidator(robotMock.Object, tableMock.Object);

            //Act
            inputValidator.ValidateInput(rightCommand);

            //Assert
            robotMock.Verify(x => x.TurnRight(), Times.Once());
        }

        [Theory]
        [InlineData("right")]
        [InlineData("RIGHT")]
        public void ValidateInput_GivenRightCommand_ReturnsTurnedRightMessage(string rightCommand)
        {
            //Arrange
            var inputValidator = GetInputValidator();
            var expected = UserMessageConstants.TurnedRight;

            //Act
            var result = inputValidator.ValidateInput(rightCommand);

            //Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("report")]
        [InlineData("REPORT")]
        public void ValidateInput_GivenReportCommand_ReportsRobotPosition(string reportCommand)
        {
            //Arrange
            var tableMock = GetTableMock();
            var robotMock = GetRobotMock();
            var inputValidator = GetInputValidator(robotMock.Object, tableMock.Object);

            //Act
            inputValidator.ValidateInput(reportCommand);

            //Assert
            robotMock.Verify(x => x.Report(), Times.Once());
        }

        [Theory]
        [InlineData("exit")]
        [InlineData("EXIT")]
        public void ValidateInput_GivenExitCommand_ReturnsExitMessage(string exitCommand)
        {
            //Arrange
            var inputValidator = GetInputValidator();
            var expected = UserMessageConstants.Exit;

            //Act
            var result = inputValidator.ValidateInput(exitCommand);

            //Assert
            Assert.Equal(expected, result);
        }



        public static IEnumerable<object[]> GetValidPlaceCommands =>
            new List<object[]>
            {
                new object[] {"PLACE", "0", "0", "NORTH"},
                new object[] {"place", "0", "0", "SOUTH"},
                new object[] {"PLACE", "0", "0", "east"},
                new object[] {"PLACE", "5", "5", "West"},
            };

        private Mock<IRobot> GetRobotMock()
        {
            return new Mock<IRobot>();
        }

        private Mock<ITable> GetTableMock()
        {
            return new Mock<ITable>();
        }

        private InputValidator GetInputValidator(IRobot robot, ITable table)
        {
            return new InputValidator(robot, table);
        }

        private InputValidator GetInputValidator()
        {
            var robotMock = new Mock<IRobot>();
            var tableMock = new Mock<ITable>();
            return GetInputValidator(robotMock.Object, tableMock.Object);
        }
    }
}
