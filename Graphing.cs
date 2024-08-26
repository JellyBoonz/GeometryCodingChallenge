using SkiaSharp;
using System.IO;

namespace ShapeCalculator
{
    public interface IGraphingShape
    {
        (float X, float Y)[] GetVertices();
        (float X, float Y) CalculateCentroid();
    }

    class Graphing
    {
        public static void DrawShape(IGraphingShape shape, string outputPath)
        {
            const int width = 500;
            const int height = 500;
            using var surface = SKSurface.Create(new SKImageInfo(width, height));
            var canvas = surface.Canvas;
            canvas.Clear(SKColors.White);

            var vertices = shape.GetVertices();
            var centroid = shape.CalculateCentroid();

            // Define colors for the vertices
            var vertexColors = new SKColor[]
            {
        SKColors.Blue, SKColors.Green, SKColors.Orange, SKColors.Yellow, SKColors.Purple
            };

            // Draw the shape's outline
            using (var paint = new SKPaint { Color = SKColors.Blue, StrokeWidth = 2, IsStroke = true })
            {
                var path = new SKPath();
                path.MoveTo(vertices[0].X, vertices[0].Y);
                for (int i = 1; i < vertices.Length; i++)
                {
                    path.LineTo(vertices[i].X, vertices[i].Y);
                }
                path.Close();
                canvas.DrawPath(path, paint);
            }

            // Draw vertices as colored circles
            using (var paint = new SKPaint { StrokeWidth = 4, IsStroke = true })
            {
                for (int i = 0; i < vertices.Length; i++)
                {
                    paint.Color = vertexColors[i % vertexColors.Length];
                    canvas.DrawCircle(vertices[i].X, vertices[i].Y, 2, paint);
                }
            }

            // Draw the centroid as a red circle
            using (var paint = new SKPaint { Color = SKColors.Red, StrokeWidth = 4, IsStroke = true })
            {
                canvas.DrawCircle(centroid.X, centroid.Y, 2, paint);
            }

            // Draw the legend with colored dots next to black coordinates
            using (var paint = new SKPaint { TextSize = 12, IsAntialias = true })
            {
                canvas.Save();
                canvas.ResetMatrix();

                for (int i = 0; i < vertices.Length; i++)
                {
                    // Draw colored dot
                    paint.Color = vertexColors[i % vertexColors.Length];
                    canvas.DrawCircle(5, 25 + (i * 15), 5, paint);

                    // Draw black coordinates
                    paint.Color = SKColors.Black;
                    string legend = $"V{i + 1} - ({vertices[i].X}, {vertices[i].Y})";
                    canvas.DrawText(legend, 15, 30 + (i * 15), paint);
                }

                // Draw the centroid legend
                paint.Color = SKColors.Red;
                canvas.DrawCircle(5, 25 + (vertices.Length * 15), 5, paint);

                paint.Color = SKColors.Black;
                string centroidLegend = $"C - ({centroid.X}, {centroid.Y})";
                canvas.DrawText(centroidLegend, 15, 30 + (vertices.Length * 15), paint);

                canvas.Restore();
            }

            // Save the image to the output path
            using var image = surface.Snapshot();
            using var data = image.Encode(SKEncodedImageFormat.Png, 100);
            using var stream = File.OpenWrite(outputPath);
            data.SaveTo(stream);
        }


        public static void DrawEllipse(Ellipse ellipse, string outputPath)
        {
            const int width = 500;
            const int height = 500;
            using var surface = SKSurface.Create(new SKImageInfo(width, height));
            var canvas = surface.Canvas;
            canvas.Clear(SKColors.White);

            var centroid = ellipse.CalculateCentroid();
            var vertices = ellipse.GetVertices();

            // Define colors for the vertices
            var vertexColors = new SKColor[]
            {
        SKColors.Blue, SKColors.Green, SKColors.Orange, SKColors.Yellow, SKColors.Purple
            };

            // Save the initial state
            canvas.Save();

            // Translate and rotate the canvas
            canvas.Translate(centroid.X, centroid.Y);
            canvas.RotateDegrees(ellipse.Orientation * 180 / MathF.PI);

            // Draw the ellipse
            var ellipseBounds = new SKRect(
                -ellipse.MajorAxis,
                -ellipse.MinorAxis,
                ellipse.MajorAxis,
                ellipse.MinorAxis
            );
            using (var paint = new SKPaint { Color = SKColors.Blue, StrokeWidth = 2, IsStroke = true })
            {
                canvas.DrawOval(ellipseBounds, paint);
            }

            // Restore the canvas to its original state before translation and rotation
            canvas.Restore();

            // Draw vertices as colored circles
            using (var paint = new SKPaint { StrokeWidth = 4, IsStroke = true })
            {
                for (int i = 0; i < vertices.Length; i++)
                {
                    paint.Color = vertexColors[i % vertexColors.Length];
                    canvas.DrawCircle(vertices[i].X, vertices[i].Y, 2, paint);
                }
            }

            // Draw the centroid as a red circle
            using (var paint = new SKPaint { Color = SKColors.Red, StrokeWidth = 4, IsStroke = true })
            {
                canvas.DrawCircle(centroid.X, centroid.Y, 2, paint);
            }

            // Draw the legend with colored dots next to black coordinates
            using (var paint = new SKPaint { TextSize = 12, IsAntialias = true })
            {
                for (int i = 0; i < vertices.Length; i++)
                {
                    // Draw colored dot
                    paint.Color = vertexColors[i % vertexColors.Length];
                    canvas.DrawCircle(5, 25 + (i * 15), 5, paint);

                    // Draw black coordinates
                    paint.Color = SKColors.Black;
                    string legend = $"V{i + 1} - ({vertices[i].X}, {vertices[i].Y})";
                    canvas.DrawText(legend, 15, 30 + (i * 15), paint);
                }

                // Draw the centroid legend
                paint.Color = SKColors.Red;
                canvas.DrawCircle(5, 25 + (vertices.Length * 15), 5, paint);

                paint.Color = SKColors.Black;
                string centroidLegend = $"C - ({centroid.X}, {centroid.Y})";
                canvas.DrawText(centroidLegend, 15, 30 + (vertices.Length * 15), paint);
            }

            // Save the image to the output path
            using var image = surface.Snapshot();
            using var data = image.Encode(SKEncodedImageFormat.Png, 100);
            using var stream = File.OpenWrite(outputPath);
            data.SaveTo(stream);
        }

    }
}
