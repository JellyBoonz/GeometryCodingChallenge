namespace ShapeCalculator
{
    public class Square : Shape, IGraphingShape
    {
        public float SideLength { get; set; }
        public float Orientation { get; set; }

        public Square(float x, float y, float sideLength, float orientation)
            : base(x, y)
        {
            SideLength = sideLength;
            Orientation = orientation;
        }

        public override float CalculateArea() => SideLength * SideLength;

        public override float CalculatePerimeter() => 4 * SideLength;

        public override (float X, float Y) CalculateCentroid() => Center;

        public (float X, float Y)[] GetVertices()
        {
            // Calculate the distance from the centroid of square to a vertex
            float r = MathF.Sqrt(2) / 2 * SideLength;

            // Calculate the first vertex V1 on the x-axis with the given orientation in radians
            var V1 = (
                r * MathF.Cos(Orientation),
                r * MathF.Sin(Orientation)
            );

            var V2 = RotatePoint(V1.Item1, V1.Item2, MathF.PI / 2);
            var V3 = RotatePoint(V1.Item1, V1.Item2, MathF.PI);
            var V4 = RotatePoint(V1.Item1, V1.Item2, 3 * MathF.PI / 2);



            // Translate the vertices to the given center
            V1 = (Center.X + V1.Item1, Center.Y + V1.Item2);
            V2 = (Center.X + V2.Item1, Center.Y + V2.Item2);
            V3 = (Center.X + V3.Item1, Center.Y + V3.Item2);
            V4 = (Center.X + V4.Item1, Center.Y + V4.Item2);

            return [V1, V2, V3, V4];
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