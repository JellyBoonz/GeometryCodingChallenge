namespace ShapeCalculator
{
    class Calculator
    {
        public List<string[]> ProcessShapeData(string[] data)
        {
            List<string[]> outputData = new List<string[]>();

            for (int i = 0; i < data.Length; i++)
            {
                data[i] = data[i].TrimEnd(',');
                string[] shapeData = data[i].Split(',');

                string shapeType = shapeData[1];
                float area = 0.0f;
                float perceivedArea = 0.0f;
                float perimeter = 0.0f;
                float perceivedPerimeter = 0.0f;
                (float X, float Y) centroid = (0.0f, 0.0f);

                float centerX = float.Parse(shapeData[3]);
                float centerY = float.Parse(shapeData[5]);

                string vertexData = ""; // Initialize the vertex data string

                switch (shapeType)
                {
                    case "Circle":
                        float radius = float.Parse(shapeData[7]);
                        var circle = new Circle(centerX, centerY, radius);
                        area = circle.CalculateArea();
                        perimeter = circle.CalculatePerimeter();
                        centroid = circle.CalculateCentroid();
                        break;

                    case "Equilateral Triangle":
                        float sideLengthTriangle = float.Parse(shapeData[7]);
                        float orientation = float.Parse(shapeData[9]);
                        var triangle = new EquilateralTriangle(centerX, centerY, orientation, sideLengthTriangle);
                        area = triangle.CalculateArea();
                        perimeter = triangle.CalculatePerimeter();
                        centroid = triangle.CalculateCentroid();
                        vertexData = FormatVertexData(triangle.GetVertices());
                        Graphing.DrawShape(triangle, $"./Images/triangles/triangle_{i}.png");
                        break;

                    case "Ellipse":
                        float longRadius = float.Parse(shapeData[7]);
                        float shortRadius = float.Parse(shapeData[9]);
                        orientation = float.Parse(shapeData[11]);
                        var ellipse = new Ellipse(centerX, centerY, longRadius, shortRadius, orientation);
                        area = ellipse.CalculateArea();
                        perimeter = ellipse.CalculatePerimeter();
                        centroid = ellipse.CalculateCentroid();
                        vertexData = FormatVertexData(ellipse.GetVertices());
                        Graphing.DrawEllipse(ellipse, $"./Images/ellipses/ellipse_{i}.png");
                        break;

                    case "Square":
                        float sideLengthSquare = float.Parse(shapeData[7]);
                        orientation = float.Parse(shapeData[9]);
                        var square = new Square(centerX, centerY, sideLengthSquare, orientation);
                        area = square.CalculateArea();
                        perimeter = square.CalculatePerimeter();
                        centroid = square.CalculateCentroid();
                        vertexData = FormatVertexData(square.GetVertices());
                        Graphing.DrawShape(square, $"./Images/squares/square_{i}.png");
                        break;

                    case "Polygon":
                        var polygonData = data[i].Split(',');
                        int numberOfVertices = (polygonData.Length - 2) / 4;
                        (float X, float Y)[] vertices = new (float X, float Y)[numberOfVertices];

                        for (int j = 0, k = 2; j < numberOfVertices; j++, k += 4)
                        {
                            float x = float.Parse(polygonData[k + 1]);
                            float y = float.Parse(polygonData[k + 3]);
                            vertices[j] = (x, y);
                        }
                        var polygon = new Polygon(centerX, centerY, vertices);
                        area = polygon.CalculateArea();
                        perceivedArea = area;
                        perimeter = polygon.CalculatePerimeter();
                        perceivedPerimeter = perimeter;
                        centroid = polygon.CalculateCentroid();
                        vertexData = FormatVertexData(vertices);
                        break;

                    default:
                        throw new ArgumentException($"Unknown shape type: {shapeType}");
                }

                outputData.Add(new string[] {
                    (i + 1).ToString(), shapeType, "Area", area.ToString(), "Perimeter", perimeter.ToString(), "Centroid", $"{centroid.X},{centroid.Y}", "Vertices", vertexData
                });
            }

            return outputData;
        }

        private string FormatVertexData((float X, float Y)[] vertices)
        {
            var formattedVertices = new List<string>();
            for (int i = 0; i < vertices.Length; i++)
            {
                formattedVertices.Add($" V{i + 1}, ({vertices[i].X} : {vertices[i].Y}), ");
            }
            return string.Join("", formattedVertices);
        }
    }
}