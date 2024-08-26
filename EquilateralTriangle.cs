using System.Net.NetworkInformation;
using Microsoft.Testing.Platform.Extensions.Messages;

namespace ShapeCalculator
{
    public class EquilateralTriangle : Shape
    {
        public float SideLength { get; set; }
        public float Orientation { get; set; }

        public EquilateralTriangle(float x, float y, float orientation, float sideLength)
            : base(x, y)
        {
            SideLength = sideLength;
            Orientation = orientation;
        }

        public override float CalculateArea() => (MathF.Sqrt(3) / 4) * MathF.Pow(SideLength, 2);

        public override float CalculatePerimeter() => 3 * SideLength;

        public override (float X, float Y) CalculateCentroid() => Center;

        public (float X, float Y)[] GetVertices()
        {
            // Calculate the distance from the centroid to a vertex
            float r = MathF.Sqrt(3) / 3 * SideLength;

            // Calculate the first vertex V1 on the x-axis with the given orientation in radians
            var V1 = (
                r * MathF.Cos(Orientation),
                r * MathF.Sin(Orientation)
            );

            // Calculate the second vertex V2 by rotating V1 by 120 degrees (2π/3 radians)
            var V2 = RotatePoint(V1.Item1, V1.Item2, 2 * MathF.PI / 3);

            // Calculate the third vertex V3 by rotating V1 by 240 degrees (4π/3 radians)
            var V3 = RotatePoint(V1.Item1, V1.Item2, 4 * MathF.PI / 3);

            // Translate the vertices to the given center
            V1 = (Center.X + V1.Item1, Center.Y + V1.Item2);
            V2 = (Center.X + V2.Item1, Center.Y + V2.Item2);
            V3 = (Center.X + V3.Item1, Center.Y + V3.Item2);

            return [V1, V2, V3];
        }

        private static (float, float) RotatePoint(float x, float y, float angleRad)
        {
            float cosAngle = MathF.Cos(angleRad);
            float sinAngle = MathF.Sin(angleRad);

            float xNew = x * cosAngle - y * sinAngle;
            float yNew = x * sinAngle + y * cosAngle;

            return (xNew, yNew);
        }

    }
}