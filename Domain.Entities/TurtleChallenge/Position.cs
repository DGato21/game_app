using System.Diagnostics.CodeAnalysis;

namespace Domain.Entities.TurtleChallenge
{
    public class Position : IEquatable<Position>
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// Compare Position to Board Maximum Boards
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool ValidatePosition(Position maxBoardSize)
        {
            return this.X > 0 && this.Y > 0 && this.X < maxBoardSize.X && this.Y < maxBoardSize.Y;
        }

        /// <summary>
        /// Compare Position to Board Maximum Boards
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool ValidatePosition(int x, int y)
        {
            return ValidatePosition(new Position(x, y));
        }

        public bool Equals(Position? other)
        {
            return this.X.Equals(other.X) && this.Y.Equals(other.Y);
        }
    }
}
