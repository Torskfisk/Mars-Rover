using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRover
{
    public class Vehicle
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Direction Orientation { get; set; }

        public string DisplayPosition()
        {
            return string.Format("{0} {1} {2}", X, Y, Orientation.ToString());
        }

        public void Move()
        {
            switch (Orientation)
            {
                case Direction.N:
                    {
                        Y++;
                        break;
                    }
                case Direction.E:
                    {
                        X++;
                        break;
                    }
                case Direction.S:
                    {
                        Y--;
                        break;
                    }
                case Direction.W:
                    {
                        X--;
                        break;
                    }
            }
        }

        public void Rotate(char dir)
        {
            if (dir == 'R')
            {
                switch (Orientation)
                {
                    case Direction.N:
                        {
                            Orientation = Direction.E;
                            break;
                        }
                    case Direction.E:
                        {
                            Orientation = Direction.S;
                            break;
                        }
                    case Direction.S:
                        {
                            Orientation = Direction.W;
                            break;
                        }
                    case Direction.W:
                        {
                            Orientation = Direction.N;
                            break;
                        }
                }
            }
            else if (dir == 'L')
            {
                switch (Orientation)
                {
                    case Direction.N:
                        {
                            Orientation = Direction.W;
                            break;
                        }
                    case Direction.E:
                        {
                            Orientation = Direction.N;
                            break;
                        }
                    case Direction.S:
                        {
                            Orientation = Direction.E;
                            break;
                        }
                    case Direction.W:
                        {
                            Orientation = Direction.S;
                            break;
                        }
                }
            }
        }
    }
}
