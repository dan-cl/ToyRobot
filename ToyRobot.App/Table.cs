using System;
using System.Collections.Generic;
using System.Linq;


namespace ToyRobot.App
{
    public class Table
    {
        private readonly int _tableSize;

        public delegate bool MoveMethodDelegate(IRobot robot);

        public Dictionary<string, Delegate> MoveMethods;

        public Table()
        {
            _tableSize = TableSizeConstants.TableSize;

            MoveMethods = new Dictionary<string, Delegate>()
            {
                {Constants.North, new MoveMethodDelegate(MoveNorth) },
                {Constants.East, new MoveMethodDelegate(MoveEast) },
                {Constants.South, new MoveMethodDelegate(MoveSouth) },
                {Constants.West, new MoveMethodDelegate(MoveWest) },
            };
        }

        public bool Move(IRobot robot)
        {
            if(!ValidPosition(robot.Position)) return false;

            if (!ValidHeading(robot.Heading, out var moveMethodDelegate)) return false;

            return moveMethodDelegate(robot);
        }

        public bool Place(int[] position, IRobot robot)
        {
            if (!ValidPosition(position)) return false;

            robot.Position = position;
            robot.Placed = true;
            return true;
        }
         


        private bool ValidPosition(int[] position)
        {
            return position.All(value => value >= 0 && _tableSize >= value);
        }

        private bool ValidHeading(string robotHeading, out MoveMethodDelegate moveMethodDelegate)
        {
            foreach (var headingString in Constants.HeadingStrings)
            {
                if (robotHeading != headingString) continue;

                moveMethodDelegate = MoveMethods[headingString] as MoveMethodDelegate;
                return true;
            }

            moveMethodDelegate = null;
            return false;
        }

        private bool MoveNorth(IRobot robot)
        {
            var axis = 1;
            var velocity = 1;
            return ChangeCoordinate(robot, axis, velocity);
        }

        private bool MoveEast(IRobot robot)
        {
            var axis = 0;
            var velocity = 1;
            return ChangeCoordinate(robot, axis, velocity);
        }

        private bool MoveSouth(IRobot robot)
        {
            var axis = 1;
            var velocity = -1;
            return ChangeCoordinate(robot, axis, velocity);
        }

        private bool MoveWest(IRobot robot)
        {
            var axis = 0;
            var velocity = -1;
            return ChangeCoordinate(robot, axis, velocity);
        }

        private bool ChangeCoordinate(IRobot robot, int axis, int velocity)
        {
            var newPosition = (int[])robot.Position.Clone();
            newPosition[axis] += velocity;

            if (!ValidPosition(newPosition)) return false;

            robot.Position[axis] += velocity;
            return true;
        }
    }
}
