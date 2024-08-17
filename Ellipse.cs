namespace ShapeCalculator
{
    public class Ellipse : Shape
    {
        public float MajorAxis { get; set; }
        public float MinorAxis { get; set; }

        public Ellipse(float x, float y, float majorAxis, float minorAxis)
            : base(x, y)
        {
            MajorAxis = majorAxis;
            MinorAxis = minorAxis;
        }

        public override float CalculateArea() => MathF.PI * MajorAxis * MinorAxis;

        public override float CalculatePerimeter()
        {
            // Approximation formula for the perimeter of an ellipse
            return MathF.PI * (3 * (MajorAxis + MinorAxis) - MathF.Sqrt((3 * MajorAxis + MinorAxis) * (MajorAxis + 3 * MinorAxis)));
        }

        public override (float X, float Y) CalculateCentroid() => Center;
    }

}