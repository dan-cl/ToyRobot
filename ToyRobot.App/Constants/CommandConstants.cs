namespace ToyRobot.App.Constants
{
    public static class CommandConstants
    {
        public const string Place = "PLACE";
        public const string Move = "MOVE";
        public const string Left = "LEFT";
        public const string Right = "RIGHT";
        public const string Report = "REPORT";
        public const string Exit = "EXIT";
        public const string Help = "HELP";

        public static string[] CommandStrings = { Place, Move, Left, Right, Report, Exit, Help };
    }
}