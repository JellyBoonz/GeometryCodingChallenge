using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ShapeCalculator.Tests
{
    [TestClass]
    public class ShapeTests
    {
        private const float Epsilon = 1e-5f; // For floating-point comparisons

        [TestMethod]
        public void CircleCalculations()
        {
            var circle = new Circle(10, 20, 5);

            Assert.AreEqual(Math.PI * 25, circle.CalculateArea(), Epsilon);
            Assert.AreEqual(2 * Math.PI * 5, circle.CalculatePerimeter(), Epsilon);
            Assert.AreEqual((10, 20), circle.CalculateCentroid());
        }

        [TestMethod]
        public void EquilateralTriangleCalculations()
        {
            var triangle = new EquilateralTriangle(0, 0, 0, 10);

            Assert.AreEqual(25 * Math.Sqrt(3), triangle.CalculateArea(), Epsilon);
            Assert.AreEqual(30, triangle.CalculatePerimeter(), Epsilon);
            Assert.AreEqual((0, 0), triangle.CalculateCentroid());
        }

        [TestMethod]
        public void EllipseCalculations()
        {
            var ellipse = new Ellipse(5, 5, 10, 5);

            Assert.AreEqual(Math.PI * 50, ellipse.CalculateArea(), Epsilon);
            // Approximation of ellipse perimeter
            Assert.AreEqual(Math.PI * (3 * (10 + 5) - Math.Sqrt((3 * 10 + 5) * (10 + 3 * 5))), ellipse.CalculatePerimeter(), Epsilon);
            Assert.AreEqual((5, 5), ellipse.CalculateCentroid());
        }

        [TestMethod]
        public void SquareCalculations()
        {
            var square = new Square(2, 2, 4);

            Assert.AreEqual(16, square.CalculateArea(), Epsilon);
            Assert.AreEqual(16, square.CalculatePerimeter(), Epsilon);
            Assert.AreEqual((2, 2), square.CalculateCentroid());
        }

        [TestMethod]
        public void PolygonCalculations()
        {
            var vertices = new (float X, float Y)[]
            {
                (346.746f, 51.165f),
                (413.75f, 208.36f),
                (309.926f, 280.286f),
                (34.4096f, 297.813f),
                (346.746f, 51.165f)
            };

            var polygon = new Polygon(0, 0, vertices);

            Assert.AreEqual(41810.6f, polygon.CalculateArea(), Epsilon);
            Assert.AreEqual(971.2385f, polygon.CalculatePerimeter(), Epsilon);
            Assert.AreEqual((262.32706f, 202.21654f), polygon.CalculateCentroid());
        }

        [TestMethod]
        public void EquilateralTriangle_ValidSideLengthLessThanOne()
        {
            var triangle = new EquilateralTriangle(0, 0, 0, 0.5f);
            Assert.AreEqual(0.0625 * MathF.Sqrt(3), triangle.CalculateArea(), Epsilon);
            Assert.AreEqual(1.5, triangle.CalculatePerimeter(), Epsilon);
            Assert.AreEqual((0, 0), triangle.CalculateCentroid());
        }
    }
}