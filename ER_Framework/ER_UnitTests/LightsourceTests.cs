using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace de.sounour.uni.er.Tests
{
    [TestClass]
    public class LightsourceTests
    {
        private readonly double tolerance = 0.1;

        [TestMethod]
        public void CalculateBrightnessTest()
        {
            var w = new World(200, 200);
            var l = new Lightsource(w, 10, 10, 20, 20);
            Assert.AreEqual(20, l.CalculateBrightness(10, 10), tolerance);
            Assert.AreEqual(0, l.CalculateBrightness(100, 100), tolerance);
            Assert.AreEqual(10, l.CalculateBrightness(0, 10), tolerance);
            Assert.AreEqual(10, l.CalculateBrightness(10, 0), tolerance);
            Assert.AreEqual(20 - 10*Math.Sqrt(2),
                l.CalculateBrightness(0, 0), tolerance
            );
        }
    }
}