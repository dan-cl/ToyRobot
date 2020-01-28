using System;
using System.Collections.Generic;
using System.Linq;
using ToyRobot.App.Constants;

namespace ToyRobot.App.Validators
{
    public class InputValidator
    {
        private IRobot _robot;
        private ITable _table;

        private delegate string PlaceRobotCommandMethod(string input);

        private delegate string RobotCommandMethod();

        private delegate string MenuCommandMethod();

        private Dictionary<string, Delegate> CommandMethods => BindDelegates;

        public InputValidator(IRobot robot, ITable table)
        {
            _robot = robot;
            _table = table;
        }
        public string ValidateInput(string input)
        {
            Delegate commandDelegate = null;
            var inputCommand = input.Split(" ")[0].ToUpper();

            foreach (var command in CommandConstants.CommandStrings)
            {
                if (inputCommand == command)
                {
                    commandDelegate = CommandMethods[command];
                    break;
                }
            }

            switch (commandDelegate)
            {
                case null:
                    return UnknownCommand();
                case PlaceRobotCommandMethod placeRobotMethod:
                    return placeRobotMethod(input);
                case RobotCommandMethod robotMethod:
                    return _robot.Placed ? robotMethod() : UserMessageConstants.RobotNotPlaced;
                case MenuCommandMethod menuMethod:
                    return menuMethod();
                default:
                    return UnknownCommand();
            }
        }

        private Dictionary<string, Delegate> BindDelegates =>
            new Dictionary<string, Delegate>()
            {
                {CommandConstants.Place, new PlaceRobotCommandMethod(Place)},
                {CommandConstants.Move, new RobotCommandMethod(Move)},
                {CommandConstants.Left, new RobotCommandMethod(Left)},
                {CommandConstants.Right, new RobotCommandMethod(Right)},
                {CommandConstants.Report, new RobotCommandMethod(Report)},
                {CommandConstants.Exit, new MenuCommandMethod(Exit) },
                {CommandConstants.Help, new MenuCommandMethod(Help) }
            };

        private string Place(string input)
        {
            var commandAndArguments = input.Split(" ");
            if (commandAndArguments.Length < 2)
                return UnknownCommand();

            var positionAndHeading = commandAndArguments[1];
            var positionAndHeadingArray = positionAndHeading?.Split(",");

            if (positionAndHeadingArray?.Length < 3)
                return UnknownCommand();

            if (!int.TryParse(positionAndHeadingArray?[0], out int xCoordinate))
                return UnknownCommand();

            if (!int.TryParse(positionAndHeadingArray?[1], out int yCoordinate))
                return UnknownCommand();

            var heading = positionAndHeadingArray?[2].ToUpper();

            if (!HeadingConstants.HeadingStrings.Contains(heading))
                return UnknownCommand();

            _robot.Heading = heading;
            var position = new int[] {xCoordinate, yCoordinate};

            if (_table.Place(position, _robot))
                return $"Robot placed at {xCoordinate}, {yCoordinate} facing {heading}";

            return UserMessageConstants.CannotPlaceRobot;
        }

        private string Move()
        {
            _table.Move(_robot);
            return UserMessageConstants.RobotMoved;
        }

        private string Left()
        {
            _robot.TurnLeft();
            return UserMessageConstants.TurnedLeft;
        }

        private string Right()
        {
            _robot.TurnRight();
            return UserMessageConstants.TurnedRight;
        }

        private string Report()
        {
            return _robot.Report();
        }

        private string Exit()
        {
            return UserMessageConstants.Exit;
        }

        private string Help()
        {
            return UserMessageConstants.HelpMessage;
        }


        private string UnknownCommand()
        {
            return UserMessageConstants.UnknownCommand;
        }
    }
}
