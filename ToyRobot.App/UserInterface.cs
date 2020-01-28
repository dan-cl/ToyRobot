using System;
using ToyRobot.App.Constants;
using ToyRobot.App.Validators;

namespace ToyRobot.App
{
    public class UserInterface
    {
        public void Start()
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine(UserMessageConstants.WelcomeMessage);
            Console.WriteLine(UserMessageConstants.HelpMessage);

            var robot = new Robot();
            var table = new Table();
            var inputValidator = new InputValidator(robot, table);
            var exit = false;

            while (!exit)
            {
                var input = Console.ReadLine();
                var outPut = inputValidator.ValidateInput(input);

                if (outPut == "EXIT")
                    exit = true;
                else
                {
                    Console.Clear();
                    Console.WriteLine(outPut);
                }

            }

        }
    }
}
