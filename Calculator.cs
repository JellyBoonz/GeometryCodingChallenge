namespace ShapeCalculator
{
    class Calculator
    {
        public List<string[]> ProcessShapeData(string[] data)
        {
            List<string[]> outputData = new List<string[]>();

            int graphFlag = 0;

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

                        // if (graphFlag == 0)
                        // {
                        //     Graphing.DrawTriangleWithCartesianGrid(triangle, $"./Images/triangle_{i}.png");
                        //     graphFlag++;
                        // }
                        break;

                    case "Ellipse":
                        float longRadius = float.Parse(shapeData[7]);
                        float shortRadius = float.Parse(shapeData[9]);
                        var ellipse = new Ellipse(centerX, centerY, longRadius, shortRadius);
                        area = ellipse.CalculateArea();
                        perimeter = ellipse.CalculatePerimeter();
                        centroid = ellipse.CalculateCentroid();

                        if (graphFlag == 0 || graphFlag == 1 || graphFlag == 2)
                        {
                            Graphing.DrawShapeWithCartesianGrid(ellipse, $"./Images/ellipse_{i}.png");
                            graphFlag++;
                        }

                        break;

                    case "Square":
                        float sideLengthSquare = float.Parse(shapeData[7]);
                        orientation = float.Parse(shapeData[9]);
                        var square = new Square(centerX, centerY, sideLengthSquare, orientation);
                        area = square.CalculateArea();
                        perimeter = square.CalculatePerimeter();
                        centroid = square.CalculateCentroid();

                        if (graphFlag == 0 || graphFlag == 1 || graphFlag == 2)
                        {
                            Graphing.DrawShapeWithCartesianGrid(square, $"./Images/square_{i}.png");
                            graphFlag++;
                        }

                        break;

                    case "Polygon":
                        // Calculate the number of vertices
                        var polygonData = data[i].Split(',');
                        int numberOfVertices = (polygonData.Length - 2) / 4;

                        // Create an array to store the vertices
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
                        break;

                    default:
                        throw new ArgumentException($"Unknown shape type: {shapeType}");
                }
                outputData.Add(new string[] {
                    (i + 1).ToString(), shapeType, "Area", area.ToString(), "perimeter", perimeter.ToString(), "Centroid", centroid.ToString()
                });
            }

            return outputData;
        }
    }
}