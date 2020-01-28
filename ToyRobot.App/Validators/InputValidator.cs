using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ToyRobot.App.Validators
{
    public class InputValidator
    {
        private IRobot _robot;
        private ITable _table;

        private delegate string PlaceCommandMethodDelegate(string input);

        private delegate string CommandMethodDelegate();

        private Dictionary<string, Delegate> CommandMethods => BindDelegates;

        public InputValidator(IRobot robot, ITable table)
        {
            _robot = robot;
            _table = table;
        }
        public string ValidateInput(string input)
        {
            Delegate commandDelegate = null;
            var inputCommand = input.Split(" ")[0];

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
                case PlaceCommandMethodDelegate placeMethod:
                    return placeMethod(input);
                case CommandMethodDelegate method:
                    return method();
                default:
                    return UnknownCommand();
            }
        }

        private Dictionary<string, Delegate> BindDelegates =>
            new Dictionary<string, Delegate>()
            {
                {CommandConstants.Place, new PlaceCommandMethodDelegate(Place)},
                {CommandConstants.Move, new CommandMethodDelegate(Move)},
                {CommandConstants.Left, new CommandMethodDelegate(Left)},
                {CommandConstants.Right, new CommandMethodDelegate(Right)},
                {CommandConstants.Report, new CommandMethodDelegate(Report)},
                {CommandConstants.Exit, new CommandMethodDelegate(Exit) }
            };

        private string Place(string input)
        {
            var positionAndHeading = input.Split(" ")[1];

            if (!int.TryParse(positionAndHeading?.Split(",")[0], out int xCoordinate))
                return UnknownCommand();

            if (!int.TryParse(positionAndHeading?.Split(",")[1], out int yCoordinate))
                return UnknownCommand();

            var heading = positionAndHeading?.Split(",")[2].ToUpper();

            if (!HeadingConstants.HeadingStrings.Contains(heading))
                return UnknownCommand();

            _robot.Heading = heading;
            var position = new int[] {xCoordinate, yCoordinate};

            if (_table.Place(position, _robot))
                return $"Robot placed at {xCoordinate}, {yCoordinate} facing {heading}";


            return "Can't place robot outside of table";
        }

        private string Move()
        {
            _table.Move(_robot);
            return "robot moved";
        }

        private string Left()
        {
            _robot.TurnLeft();
            return "robot turned left";
        }

        private string Right()
        {
            _robot.TurnRight();
            return "robot turned right";
        }

        private string Report()
        {
            return _robot.Report();
        }

        private string Exit()
        {
            return "EXIT";
        }

        private string UnknownCommand()
        {
            return "Unknown command";
        }
    }
}
