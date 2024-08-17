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

        public float CalculateRotatedArea()
        {
            // Step 1: Compute the vertices of the equilateral triangle
            var halfHeight = (MathF.Sqrt(3) / 2) * SideLength;
            var vertex1 = (X: Center.X, Y: Center.Y + 2 * halfHeight / 3, Z: 0f);
            var vertex2 = (X: Center.X - SideLength / 2, Y: Center.Y - halfHeight / 3, Z: 0f);
            var vertex3 = (X: Center.X + SideLength / 2, Y: Center.Y - halfHeight / 3, Z: 0f);

            // Step 2: Apply rotation around the y-axis
            vertex1 = RotatePointAroundYAxis(vertex1, Orientation);
            vertex2 = RotatePointAroundYAxis(vertex2, Orientation);
            vertex3 = RotatePointAroundYAxis(vertex3, Orientation);

            // Step 3: Project the points to the xy-plane (ignore Z)
            var projectedVertex1 = (X: vertex1.X, Y: vertex1.Y);
            var projectedVertex2 = (X: vertex2.X, Y: vertex2.Y);
            var projectedVertex3 = (X: vertex3.X, Y: vertex3.Y);

            // Step 4: Calculate the area of the triangle
            return MathF.Abs(
                0.5f * (projectedVertex1.X * (projectedVertex2.Y - projectedVertex3.Y) +
                        projectedVertex2.X * (projectedVertex3.Y - projectedVertex1.Y) +
                        projectedVertex3.X * (projectedVertex1.Y - projectedVertex2.Y))
            );
        }

        // public float CalculateRotatedPerimeter()
        // {

        // }

        private (float X, float Y, float Z) RotatePointAroundYAxis((float X, float Y, float Z) point, float angle)
        {
            float cosTheta = MathF.Cos(angle);
            float sinTheta = MathF.Sin(angle);
            float xNew = cosTheta * point.X + sinTheta * point.Z;
            float zNew = -sinTheta * point.X + cosTheta * point.Z;
            return (xNew, point.Y, zNew);
        }
    }
}