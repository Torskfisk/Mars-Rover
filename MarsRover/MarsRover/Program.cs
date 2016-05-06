using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRover
{
    public enum Direction { N = 0, E = 1, S = 2, W = 3 }

    class Program
    {
        static VehicleController _trafficController = new VehicleController();
        
        static void Main(string[] args)
        {

            GetPlateauDimensionInputFromConsole();

            // just allow the user to enter as many instructions as they want.
            // We ought to define a command to stop entering instructions really, 
            while( true )
            {
                Vehicle v = GetVehicleFromConsole();
                string cmd = ProcessCommandFromConsole();
                if(v != null)
                {
                    string output;
                    output = _trafficController.MoveVehicle(v, cmd);
                    Console.WriteLine( "Vehicle moved to position: {0}", output);
                }

            }

            Console.ReadLine();
        }

        static void GetPlateauDimensionInputFromConsole()
        {
            bool result = false;
            int X = 0;
            int Y = 0;
            string errorMsg = "Incorrect Input: we require positive integer values for X and Y coordinates, separated by a space, for example '5 5'";

            while (false == result)
            {
                Console.WriteLine("Enter the coordinates for the Upper right corner of your plateau");
                string input = Console.ReadLine();
               
                try
                {
                    string[] coords = input.Split(' ');
                    if (coords.Count() == 2)
                    {
                        X = int.Parse(coords[0]);
                        Y = int.Parse(coords[1]);

                        if (X > 0 && Y > 0)
                        {
                            _trafficController.maxXPos = X;
                            _trafficController.maxYPos = Y;

                            result = true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    result = false;
                    //Console.WriteLine(ex.Message);
                }

                if (false == result)
                {
                    Console.WriteLine(errorMsg);
                }
            }
           
        }

        static Vehicle GetVehicleFromConsole()
        {
            Vehicle v = null;
            string errorMsg = "Could not locate vehicle using the information entered, try again";
            bool result = false;
            int X =0;
            int Y = 0;
            Direction d = Direction.N;

            while(false == result)
            {
                Console.WriteLine("Identify the vehicle that you want to move, by providing its X Y and Orientation as two integers and a character, separated by spaces");
                string input = Console.ReadLine();

                try
                {
                    string[] locn = input.Split(' ');
                    if (locn.Count() == 3)
                    {
                        X = int.Parse(locn[0]);
                        Y = int.Parse(locn[1]);
                        
                        // this is ugly, should do something clever with parsing to enum instead
                        switch (locn[2])
                        {
                            case "N":
                                {
                                    d = Direction.N;
                                    result = true;
                                    break;
                                }
                            case "E":
                                {
                                    d = Direction.E;
                                    result = true;
                                    break;
                                }
                            case "S":
                                {
                                    d = Direction.S;
                                    result = true;
                                    break;
                                }
                            case "W":
                                {
                                    d = Direction.W;
                                    result = true;
                                    break;
                                }  
                        }

                        if (true == result)
                        {
                            v = new Vehicle();
                            v.X = X;
                            v.Y = Y;
                            v.Orientation = d;

                            result = _trafficController.FindVehicle(v);
                        }

                        if(false == result)
                        {
                            Console.WriteLine(errorMsg);
                        }
                    }
                }
                catch (Exception ex)
                {
                    result = false;
                    //Console.WriteLine(ex.Message);
                }
            }

            return v;
        }

         static string ProcessCommandFromConsole()
        {
            string input = "";
            bool result = false;
            string errorMsg = "Incorrect Input: we reqire instructions as a series of 'L', 'R' and 'M' characters";

            // this is a bit crude, but serves our purpose. This funtion won't retrn until we're happy with the input
            while (result == false)
            {
                Console.WriteLine("Enter your instructions as a series of 'L', 'R' and 'M' characters, to turn the vehicle Left, Right, or to move foward one space, respectively");
                input = Console.ReadLine();

                if (false == IsCommandOK(input))
                {
                    Console.WriteLine(errorMsg);
                }
                else
                {
                    result = true;
                }
            }

            return input;
        }    

        static bool IsCommandOK(string input)
        {
            bool result = true;
            char[] allowable = { 'L', 'R', 'M' };

            foreach (char c in input)
            {
                bool bFound = false;
                foreach (char a in allowable)
                {
                    if (c == a)
                    {
                        bFound = true;
                        break;
                    }
                }

                if (false == bFound)
                {
                    result = false;
                    break;
                }
            }

            return result;
        }
    }


}
