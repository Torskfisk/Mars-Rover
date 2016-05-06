using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRover
{
    public class VehicleController
    {
        // set the starting positions of the vehicles. We could do this from the outside, via a public
        // constructor or method, but keep it simple for now.
        public static Vehicle[] _vehicles =  {  new Vehicle {X = 1, Y=2,Orientation=Direction.N},
                                                new Vehicle {X = 3, Y=3,Orientation=Direction.E},
                                               // new Vehicle {X = 5, Y=5,Orientation=Direction.N}
                                              };

        public int maxXPos { get; set; }
        public int maxYPos { get; set; }


        // return true if we can find one vehicle in the master list, that matches the input vehicle.
        // otherwise false.
        public bool FindVehicle(Vehicle vIn)
        {
            bool result = false;

            var matching = from v in _vehicles
                           where v.X == vIn.X && v.Y == vIn.Y && v.Orientation == vIn.Orientation
                           select v;

            if (matching != null && matching.ToList().Count() == 1)
            {
                result = true;
            }

            return result;
        }

        private bool FindVehicleAtPosition(int X, int Y)
        {
            bool result = false;

            var matching = from v in _vehicles
                           where v.X == X && v.Y == Y
                           select v;

            if (matching != null && matching.ToList().Count() > 0)
            {
                result = true;
            }

            return result;
        }

        private Vehicle GetVehicleAtPosition(int X, int Y)
        {
            Vehicle result = null;

            var matching = from v in _vehicles
                           where v.X == X && v.Y == Y
                           select v;

            if (matching != null)
            {
                result = matching.ToList().FirstOrDefault();
            }

            return result;
        }

        public bool UpdateVehicle(int Xold, int Yold, Direction Dold, Vehicle Vnew)
        {
            bool result = false;

            var matching = from v in _vehicles
                           where v.X == Xold && v.Y == Yold && v.Orientation == Dold
                           select v;

            if (matching != null && matching.ToList().Count() == 1)
            {
                var vUpdate = matching.FirstOrDefault();
                vUpdate.X = Vnew.X;
                vUpdate.Y = Vnew.Y;
                vUpdate.Orientation = Vnew.Orientation;
                result = true;
            }

            return result;
        }

        private bool IsPositionOnMap(int X, int Y)
        {
            bool result = false;

            if (X >= 0 && Y >= 0)
            {
                if (X <= maxXPos && Y <= maxYPos)
                {
                    result = true;
                }
            }

            return result;
        }

        public string MoveVehicle(Vehicle v, string cmd)
        {
            string result = "";
            bool confirmMove = true;
            int startingX = v.X;
            int startingY = v.Y;

            if (v != null)
            {
                // make a copy of the vehicle to be moved, and use this to test
                // for collisions, going off the edge of the plateau, before updating the position of
                // the real vehicle.
                Vehicle dummy = new Vehicle();
                dummy.X = v.X;
                dummy.Y = v.Y;
                dummy.Orientation = v.Orientation;

                foreach (char c in cmd)
                {
                    if (c == 'R' || c == 'L')
                    {
                        dummy.Rotate(c);
                    }
                    else if (c == 'M')
                    {
                        dummy.Move();

                        // check that ther vehicle is not going to collide with another one, or go off the map
                        // before updating the position of the working vehicle.
                        if (FindVehicleAtPosition(dummy.X, dummy.Y))
                        {
                            // the vehicle is allowed to cross its own starting position
                            if (dummy.X != startingX && dummy.Y != startingY)
                            {
                                confirmMove = false;
                                break;
                            }
                        }
                        if (false == IsPositionOnMap(dummy.X, dummy.Y))
                        {
                            confirmMove = false;
                            break;
                        }

                    }

                }

                if (true == confirmMove)
                {
                    if (true == UpdateVehicle(v.X, v.Y, v.Orientation, dummy))
                    {
                        result = dummy.DisplayPosition();
                    }
                }


            }


            return result;
        }

    }
}
