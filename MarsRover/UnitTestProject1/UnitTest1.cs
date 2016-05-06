using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MarsRover;

namespace UnitTestProject1
{

    public class testCase
    {
        public Vehicle Input { get; set; }
        public string command { get; set; }
        public string ExpectedOutput { get; set; }
    }

    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestVehicleControllerMove()
        {
            VehicleController vc = new VehicleController();
            vc.maxXPos = 5;
            vc.maxYPos = 5;
            testCase[] tests = { new testCase{ Input= new Vehicle{X=1, Y=2, Orientation = Direction.N}, command ="LMLMLMLMM",  ExpectedOutput = "1 3 N" }, // one of the test cases provided with the instructions
                                 new testCase{ Input= new Vehicle{X=3, Y=3, Orientation = Direction.E}, command ="MMRMMRMRRM", ExpectedOutput = "5 1 E" }, // one of the test cases provided with the instructions
                                 new testCase{ Input= new Vehicle{X=5, Y=5, Orientation = Direction.N}, command ="MMMMMRMRRM", ExpectedOutput = "5 5 N" }, // Try and drive a vehicle off the edge of the plateau
                                 new testCase{ Input= new Vehicle{X=5, Y=5, Orientation = Direction.N}, command ="RRMMMMMLL",  ExpectedOutput = "5 5 N" } // Try and cause a collision
                               };

            foreach (var test in tests)
            {
                string output = vc.MoveVehicle(test.Input, test.command);
                Assert.IsTrue(output == test.ExpectedOutput);
            }
        }
    }
}
