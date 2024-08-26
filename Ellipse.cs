namespace ShapeCalculator
{
    public class Ellipse : Shape
    {
        public float MajorAxis { get; set; }
        public float MinorAxis { get; set; }
        public float Orientation { get; set; }

        public Ellipse(float x, float y, float majorAxis, float minorAxis, float orientation)
            : base(x, y)
        {
            MajorAxis = majorAxis;
            MinorAxis = minorAxis;
            Orientation = orientation;
        }

        public override float CalculateArea() => MathF.PI * MajorAxis * MinorAxis;

        public override float CalculatePerimeter()
        {
            // Approximation formula for the perimeter of an ellipse
            return MathF.PI * (3 * (MajorAxis + MinorAxis) - MathF.Sqrt((3 * MajorAxis + MinorAxis) * (MajorAxis + 3 * MinorAxis)));
        }

        public override (float X, float Y) CalculateCentroid() => Center;

        public (float X, float Y)[] GetVertices()
        {
            float majorAxisX = MajorAxis * MathF.Cos(Orientation);
            float majorAxisY = MajorAxis * MathF.Sin(Orientation);

            var V1 = (
                Center.X + majorAxisX,
                Center.Y + majorAxisY
            );

            var V2 = (
                Center.X - majorAxisX,
                Center.Y - majorAxisY
            );

            float minorAxisX = MinorAxis * MathF.Cos(Orientation + MathF.PI / 2);
            float minorAxisY = MinorAxis * MathF.Sin(Orientation + MathF.PI / 2);

            var V3 = (
                Center.X + minorAxisX,
                Center.Y + minorAxisY
            );

            var V4 = (
                Center.X - minorAxisX,
                Center.Y - minorAxisY
            );

            return [V1, V2, V3, V4];
        }
    }
}