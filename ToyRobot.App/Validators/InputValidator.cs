using System;
using System.Collections.Generic;
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
            return "";
        }

        private string Move()
        {
            return "";
        }

        private string Left()
        {
            return "";
        }

        private string Right()
        {
            return "";
        }

        private string Report()
        {
            return "";
        }

        private string Exit()
        {
            return "";
        }

        private string UnknownCommand()
        {
            return "";
        }
    }
}
