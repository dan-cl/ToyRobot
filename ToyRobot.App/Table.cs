using System;
using System.Collections.Generic;
using System.Linq;
using ToyRobot.App.Constants;


namespace ToyRobot.App
{
    public interface ITable
    {
        bool Move(IRobot robot);
        bool Place(int[] position, IRobot robot);
    }

    public class Table : ITable
    {
        private readonly int _tableSize;

        public delegate bool MoveMethodDelegate(IRobot robot);

        public Dictionary<string, Delegate> MoveMethodDelegateBindings;

        public Table()
        {
            _tableSize = TableSizeConstants.TableSize;

            MoveMethodDelegateBindings = bindHeadingMethodDelegates();
        }

        public bool Move(IRobot robot)
        {
            if (!robot.Placed) return false;

            if(!ValidPosition(robot.Position)) return false;

            if (!ValidHeading(robot.Heading, out var moveMethodDelegate)) return false;

            return moveMethodDelegate?.Invoke(robot) ?? false;
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
            //iterate through valid headings to check robot heading is valid
            foreach (var headingString in HeadingConstants.HeadingStrings)
            {
                if (robotHeading != headingString) continue;

                //if robot heading is valid select corresponding move method
                moveMethodDelegate = MoveMethodDelegateBindings[headingString] as MoveMethodDelegate;
                return true;
            }

            moveMethodDelegate = null;

            //if no match found return false
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
            //set axis and direction of move
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

            //check if new position will be valid
            if (!ValidPosition(newPosition)) return false;

            robot.Position[axis] += velocity;
            return true;
        }

        private Dictionary<string, Delegate> bindHeadingMethodDelegates() =>
            new Dictionary<string, Delegate>
            {
                {HeadingConstants.North, new MoveMethodDelegate(MoveNorth) },
                {HeadingConstants.East, new MoveMethodDelegate(MoveEast) },
                {HeadingConstants.South, new MoveMethodDelegate(MoveSouth) },
                {HeadingConstants.West, new MoveMethodDelegate(MoveWest) },
            };
    }
}
