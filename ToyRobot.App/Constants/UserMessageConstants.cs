namespace ToyRobot.App.Constants
{
    public class UserMessageConstants
    {
        public const string WelcomeMessage = "Welcome to Toy Robot Simulator\n\n";

        public const string HelpMessage =
            "PLACE <x>,<y>,<heading> - Will place the robot at coordinates x & y facing 'heading'.\n\n" +
            "LEFT & RIGHT - Will turn the robot 90deg in either direction.\n\n" +
            "MOVE - Will move the robot one unit forward in current direction.\n\n" + 
            "REPORT - Will report the current position and heading of the robot.\n\n" + 
            "HELP - Will display this message again.\n\n" + 
            "EXIT - Will quit this application.\n\n\n";
        public const string RobotNotPlaced = "Robot has not been placed\n\n\n";
        public const string CannotPlaceRobot = "Can't place robot outside of table\n\n\n";
        public const string RobotMoved = "Robot moved\n\n\n";
        public const string TurnedLeft = "Robot turned left\n\n\n";
        public const string TurnedRight = "Robot turned right\n\n\n";
        public const string Exit = "EXIT";
        public const string UnknownCommand = "Unknown command\n\n\n";
    }
}
