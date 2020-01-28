using System;
using System.Collections.Generic;
using System.Text;

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
}
