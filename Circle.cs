namespace ShapeCalculator
{
    public class Circle : Shape
    {
        public float Radius { get; set; }

        public Circle(float x, float y, float radius)
            : base(x, y)
        {
            Radius = radius;
        }

        public override float CalculateArea() => MathF.PI * Radius * Radius;

        public override float CalculatePerimeter() => 2 * MathF.PI * Radius;

        public override (float X, float Y) CalculateCentroid() => Center;
    }
}