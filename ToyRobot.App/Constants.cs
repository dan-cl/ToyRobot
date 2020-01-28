namespace ToyRobot.App
{
    public static class Constants
    {
         public const string North = "NORTH";
         public const string East = "EAST";
         public const string South = "SOUTH";
         public const string West = "WEST";

         public static string[] HeadingStrings = {North, East, South, West};
    }

    public static class TableSizeConstants
    {
        public const int TableSize = 5;
    }

    public static class CommandConstants
    {
        public const string Place = "PLACE";
        public const string Move = "MOVE";
        public const string Left = "LEFT";
        public const string Right = "RIGHT";
        public const string Report = "REPORT";
        public const string Exit = "EXIT";

        public static string[] CommandStrings = {Place, Move, Left, Right, Report, Exit};
    }
}
