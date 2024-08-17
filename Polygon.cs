namespace ShapeCalculator
{
    public class Polygon : Shape
    {
        public (float X, float Y)[] Vertices { get; set; }

        public Polygon(float x, float y, (float X, float Y)[] vertices)
            : base(x, y)
        {
            Vertices = vertices;
        }

        public override float CalculateArea()
        {
            float area = 0;
            int n = Vertices.Length;
            for (int i = 0; i < n; i++)
            {
                var (x1, y1) = Vertices[i];
                var (x2, y2) = Vertices[(i + 1) % n];
                area += (x1 * y2) - (x2 * y1);
            }
            return Math.Abs(area) / 2;
        }

        public override float CalculatePerimeter()
        {
            float perimeter = 0;
            int n = Vertices.Length;
            for (int i = 0; i < n; i++)
            {
                var (x1, y1) = Vertices[i];
                var (x2, y2) = Vertices[(i + 1) % n];
                perimeter += MathF.Sqrt(MathF.Pow(x2 - x1, 2) + MathF.Pow(y2 - y1, 2));
            }
            return perimeter;
        }

        public override (float X, float Y) CalculateCentroid()
        {
            float cx = 0, cy = 0;
            float area = CalculateArea();
            int n = Vertices.Length;
            for (int i = 0; i < n; i++)
            {
                var (x1, y1) = Vertices[i];
                var (x2, y2) = Vertices[(i + 1) % n];
                float factor = (x1 * y2 - x2 * y1);
                cx += (x1 + x2) * factor;
                cy += (y1 + y2) * factor;
            }
            cx /= (6 * area);
            cy /= (6 * area);
            return (cx, cy);
        }
    }

}