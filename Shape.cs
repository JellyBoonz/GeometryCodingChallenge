namespace ShapeCalculator
{
    public abstract class Shape
    {
        public (float X, float Y) Center { get; set; }

        public Shape(float x, float y)
        {
            Center = (x, y);
        }

        public abstract float CalculateArea();
        public abstract float CalculatePerimeter();
        public abstract (float X, float Y) CalculateCentroid();
    }
}