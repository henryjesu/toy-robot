using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;

namespace RobotAPI.Model
{
    /// <summary>
    /// Robot Location on the table
    /// </summary>
    public class Location
    {
        /// <summary>
        /// X Position of the Robot.
        /// </summary>
        public int X { get; set; }
        /// <summary>
        /// Y Position of the Robot.
        /// </summary>
        public int Y { get; set; }
    }
    /// <summary>
    /// Robot Facing Directions 
    /// </summary>
    public enum Direction
    {
        SOUTH,
        WEST,
        NORTH,
        EAST
    }

    /// <summary>
    /// Set of Robot Commands
    /// </summary>
    public enum Command
    {
        PLACE,
        MOVE,
        LEFT,
        RIGHT,
        REPORT
    }
 
    public static class Robot
    {
        /// <summary>
        /// Robot is placed or not in the table.
        /// </summary>
        public static Boolean IsRobotPlaced { get { return CurrentLocation != null; } }
        
        /// <summary>
        /// Current Location Coordinates
        /// </summary>
        public static Location CurrentLocation { get; set; }

        /// <summary>
        /// Current Facing Direction
        /// </summary>
        public static Direction CurrentDirection { get; set; }
        /// <summary>
        /// Processing the Robot Command and it arguments
        /// </summary>
        /// <param name="command">Name of the Command</param>
        /// <param name="location">Location Coordinates for the Robot </param>
        /// <param name="direction">Robot facing direction</param>
        /// <returns></returns>
        public static string ProcessCommand(Command command,Location location=null,Direction? direction=null )
        {
            var returnMsg = "";
            switch(command)
            {
                case Command.PLACE:
                    PlaceRobot(location, direction.Value);
                    break;
                case Command.REPORT:
                    returnMsg = ReportRobotLocation();
                    break;
                case Command.MOVE:
                    MoveRobot();
                    break;
                case Command.LEFT:
                    TurnRobotLeft();
                    break;
                case Command.RIGHT:
                    TurnRobotRight();
                    break;
            }

            return returnMsg;
        }

        /// <summary>
        /// Placing the Robot in ordered place.
        /// </summary>
        /// <param name="location">X Y coordinated of the Robot.</param>
        /// <param name="direction">Facing Direction of the Robot.</param>
        /// <returns></returns>
        private static bool PlaceRobot(Location location, Direction direction)
        {
            var isPlaced = false;

            if(CheckNewLocationIsSafe(location))
            {
                CurrentLocation = new Location { X = location.X, Y = location.Y };
                CurrentDirection = direction;
            }

            return isPlaced;
        }

        /// <summary>
        /// Report the current Location and Facing Direction of the Robot.
        /// </summary>
        /// <returns> Robot X Y Coordinates with Facing Direction.</returns>
        private static string ReportRobotLocation()
        {
            var returnMsg = "";
            if (IsRobotPlaced)
                returnMsg = string.Format("{0},{1},{2}", CurrentLocation.X, CurrentLocation.Y, CurrentDirection);
            return returnMsg;
        }

        /// <summary>
        /// Checking the next step location is not outside to the actual boundary.
        /// </summary>
        /// <param name="newLocation">Location which robot ordered to move.</param>
        /// <returns>Robot whether Location is safe to proceed or not</returns>
        private static bool CheckNewLocationIsSafe(Location newLocation)
        {
            var tableRows = 5;
            var tableColumns = 5;
            bool isSafe = newLocation.X < tableColumns && newLocation.X >= 0 &&
                          newLocation.Y < tableRows && newLocation.Y >= 0;
            return isSafe;
        }
        /// <summary>
        /// Move the Robot based on current location and direction.
        /// </summary>
        /// <returns>Robot whether Moved or Ignored the command</returns>
        private static bool MoveRobot()
        {
            var isMoved = false;
            if (IsRobotPlaced)
            {
                var nextLocation = new Location { X = CurrentLocation.X, Y = CurrentLocation.Y };
                switch (CurrentDirection)
                {
                    case Direction.NORTH:
                        nextLocation.Y += 1;
                        break;
                    case Direction.SOUTH:
                        nextLocation.Y -= 1;
                        break;
                    case Direction.EAST:
                        nextLocation.X += 1;
                        break;
                    case Direction.WEST:
                        nextLocation.X -= 1;
                        break;
                }

                if (CheckNewLocationIsSafe(nextLocation))
                {
                    CurrentLocation = new Location { X = nextLocation.X, Y = nextLocation.Y };
                    isMoved = true;
                }
            }
            return isMoved;
        }
        /// <summary>
        /// Robot Turing towards left side.
        /// </summary>
        /// <returns>Robot whether Turned or Ignored the command</returns>
        private static bool TurnRobotLeft()
        {
            return TurnRobot(-1); //Turning Counter Clockwise Decrements
        }
        /// <summary>
        /// Robot Turing towards right side.
        /// </summary>
        /// <returns>Robot is Turned or Ignored the command</returns>
        private static bool TurnRobotRight()
        {
            return TurnRobot(1); ; //Turning Clockwise Increments

        }
        /// <summary>
        /// Turn the Robot based on Left or Right
        /// </summary>
        /// <param name="i">Increment or Decrement step based on right or left</param>
        /// <returns>Robot whether Turned or Ignored the command</returns>
        private static bool TurnRobot(int i)
        {
            var isTurned = false;
            if (IsRobotPlaced)
            {
                var directionArray = Enum.GetValues(typeof(Direction));
                var directionArrayLength = directionArray.Length;
                
                var nextDirectionIndex = (int)CurrentDirection + i;

                if (nextDirectionIndex < 0)
                    CurrentDirection = (Direction)directionArrayLength - 1;
                else
                    CurrentDirection = (Direction)(((int)CurrentDirection + i) % directionArrayLength);
            }
            return isTurned;
        }
    }
}
