using System;
using System.Windows;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using de.sounour.uni.er;

namespace de.sounour.uni.er.Tests
{
    [TestClass()]
    public class RobotTest
    {
        private double tolerance = 0.1;

        [TestMethod()]
        public void TurnTest()
        {
            World w = new World(200, 200);
            Robot r = new Robot(w);
            Assert.AreEqual(r.Heading , new Vector(1,0));
            r.Turn(90);
            Assert.IsTrue(Math.Abs(r.Heading.X) < tolerance);
            Assert.IsTrue(Math.Abs(r.Heading.Y - 1 ) < tolerance);

            r.Turn(90);
            Assert.IsTrue(Math.Abs(r.Heading.X +1 ) < tolerance);
            Assert.IsTrue(Math.Abs(r.Heading.Y) < tolerance);
        }

        [TestMethod]
        public void TestMove()
        {
            World w = new World(200, 200);
            Robot r = new Robot(w);
            Vector exp = new Vector(2,0);
            Assert.AreEqual(exp, r.GetStep());
            Assert.AreEqual(exp, r.GetStep());
            Assert.AreEqual(exp, r.GetStep());


        }


    }
}
