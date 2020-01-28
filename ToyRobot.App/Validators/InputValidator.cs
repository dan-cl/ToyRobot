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

        private Dictionary<string, Delegate> CommandMethodsDelegates => BindDelegates;

        public InputValidator(IRobot robot, ITable table)
        {
            _robot = robot;
            _table = table;
        }

        public string ValidateInput(string input)
        {
            Delegate commandDelegate = null;
            var inputCommand = input.Split(" ")[0].ToUpper();

            //iterate though list of commands to match user input to command
            foreach (var command in CommandConstants.CommandStrings)
            {
                //if a match is found use the command as key to select the method associated to that command in the dictionary of method delegates
                if (inputCommand == command)
                {
                    commandDelegate = CommandMethodsDelegates[command];
                    break;
                }
            }

            switch (commandDelegate)
            {
                case null:
                    return UnknownCommand();
                //if command was a 'PLACE' command, call the place method passing the user input
                case PlaceRobotCommandMethod placeRobotMethod:
                    return placeRobotMethod(input);
                //if command was a 'LEFT', 'RIGHT', 'MOVE' etc. command, call appropriate method
                case RobotCommandMethod robotMethod:
                    return _robot.Placed ? robotMethod() : UserMessageConstants.RobotNotPlaced;
                //if command was a command, call appropriate method
                case MenuCommandMethod menuMethod:
                    return menuMethod();
                //if command was unknown
                default:
                    return UnknownCommand();
            }
        }

        //assign methods to command inputs
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
            if (!validatePlaceCommand(input, out var heading, out var position))
                return UserMessageConstants.UnknownCommand;

            _robot.Heading = heading;

            if (_table.Place(position, _robot))
            {
                var xCoordinate = position[0];
                var yCoordinate = position[1];
                return $"Robot placed at {xCoordinate}, {yCoordinate} facing {heading}";
            }

            return UserMessageConstants.CannotPlaceRobot;
        }

        private string Move()
        {
            if(_table.Move(_robot))
                return UserMessageConstants.RobotMoved;

            return UserMessageConstants.CannotMoveRobot;
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

        private bool validatePlaceCommand(string placeCommand, out string heading, out int[] position)
        {
            heading = null;
            position = null;

            var commandAndArguments = placeCommand.Split(" ");
            if (commandAndArguments.Length < 2)
                return false;

            var positionAndHeading = commandAndArguments[1];
            var positionAndHeadingArray = positionAndHeading?.Split(",");

            if (positionAndHeadingArray?.Length < 3)
                return false;

            if (!int.TryParse(positionAndHeadingArray?[0], out int xCoordinate))
                return false;

            if (!int.TryParse(positionAndHeadingArray?[1], out int yCoordinate))
                return false;

            heading = positionAndHeadingArray?[2].ToUpper();

            if (!HeadingConstants.HeadingStrings.Contains(heading))
                return false;

            position = new int[] { xCoordinate, yCoordinate };

            return true;
        }
    }
}
