using System;
using System.Collections.Generic;
using System.Text;
using ToyRobot.App.Validators;

namespace ToyRobot.App
{
    public class UserInterface
    {
        public void Start()
        {
            Console.WriteLine("Welcome");

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
                    Console.WriteLine(outPut);
                }

            }

        }
    }
}
