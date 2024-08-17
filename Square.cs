namespace ShapeCalculator
{
    public class Square : Shape
    {
        public float SideLength { get; set; }

        public Square(float x, float y, float sideLength)
            : base(x, y)
        {
            SideLength = sideLength;
        }

        public override float CalculateArea() => SideLength * SideLength;

        public override float CalculatePerimeter() => 4 * SideLength;

        public override (float X, float Y) CalculateCentroid() => Center;
    }
}