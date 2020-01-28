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
            //var inputValidator = new InputValidator();
            var exit = false;

            while (!exit)
            {
                var input = Console.ReadLine();
                InputValidator.ValidateInput(input);
                

            }

        }
    }
}
