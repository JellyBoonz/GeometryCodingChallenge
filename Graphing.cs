// using SkiaSharp;
// using System;
// using System.IO;

// namespace ShapeCalculator
// {
//     class Graphing
//     {
//         public static void DrawTriangleWithCartesianGrid(EquilateralTriangle triangle, string outputPath)
//         {
//             const int width = 500;
//             const int height = 500;
//             using var surface = SKSurface.Create(new SKImageInfo(width, height));
//             var canvas = surface.Canvas;
//             canvas.Clear(SKColors.White);

//             // Calculate vertices assuming (0, 0) is at the top-left
//             var vertices = triangle.GetVertices();
//             var centerX = triangle.Center.X;
//             var centerY = triangle.Center.Y;

//             // Draw the triangle
//             using (var paint = new SKPaint { Color = SKColors.Blue, StrokeWidth = 2, IsStroke = true })
//             {
//                 var path = new SKPath();
//                 path.MoveTo(vertices[0].X, vertices[0].Y);
//                 path.LineTo(vertices[1].X, vertices[1].Y);
//                 path.LineTo(vertices[2].X, vertices[2].Y);
//                 path.Close();
//                 canvas.DrawPath(path, paint);
//             }

//             // Draw and label the vertices
//             using (var paint = new SKPaint { Color = SKColors.Green, StrokeWidth = 4, IsStroke = true })
//             {
//                 foreach (var vertex in vertices)
//                 {
//                     canvas.DrawCircle(vertex.X, vertex.Y, 2, paint);
//                 }
//             }

//             using (var paint = new SKPaint { Color = SKColors.Black, TextSize = 12, IsAntialias = true })
//             {
//                 for (int i = 0; i < vertices.Length; i++)
//                 {
//                     string label = $"V{i + 1}";
//                     canvas.DrawText(label, vertices[i].X + 10, vertices[i].Y - 10, paint);
//                 }
//             }

//             // Draw the triangle center
//             using (var paint = new SKPaint { Color = SKColors.Red, StrokeWidth = 4, IsStroke = true })
//             {
//                 canvas.DrawCircle(centerX, centerY, 2, paint);
//             }

//             using (var paint = new SKPaint { Color = SKColors.Black, TextSize = 12, IsAntialias = true })
//             {
//                 canvas.DrawText("C", centerX + 10, centerY - 10, paint);
//             }

//             // Draw the legend in the top-left corner
//             using (var paint = new SKPaint { Color = SKColors.Black, TextSize = 12, IsAntialias = true })
//             {
//                 canvas.Save();
//                 canvas.ResetMatrix();

//                 for (int i = 0; i < vertices.Length; i++)
//                 {
//                     string legend = $"V{i + 1} - ({vertices[i].X}, {vertices[i].Y})";
//                     canvas.DrawText(legend, 10, 20 + (i * 15), paint);
//                 }

//                 // Include the center in the legend
//                 string centerLegend = $"C - ({centerX}, {centerY})";
//                 canvas.DrawText(centerLegend, 10, 20 + (vertices.Length * 15), paint);

//                 canvas.Restore();
//             }

//             // Save the image
//             using var image = surface.Snapshot();
//             using var data = image.Encode(SKEncodedImageFormat.Png, 100);
//             using var stream = File.OpenWrite(outputPath);
//             data.SaveTo(stream);
//         }
//     }
// }


using SkiaSharp;
using System;
using System.Collections.Generic;
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
        public static void DrawShapeWithCartesianGrid(IGraphingShape shape, string outputPath)
        {
            const int width = 500;
            const int height = 500;
            using var surface = SKSurface.Create(new SKImageInfo(width, height));
            var canvas = surface.Canvas;
            canvas.Clear(SKColors.White);

            var vertices = shape.GetVertices();
            var centroid = shape.CalculateCentroid();

            // Draw the shape
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

            // Draw and label the vertices
            using (var paint = new SKPaint { Color = SKColors.Green, StrokeWidth = 4, IsStroke = true })
            {
                foreach (var vertex in vertices)
                {
                    canvas.DrawCircle(vertex.X, vertex.Y, 2, paint);
                }
            }
            using (var paint = new SKPaint { Color = SKColors.Black, TextSize = 12, IsAntialias = true })
            {
                for (int i = 0; i < vertices.Length; i++)
                {
                    string label = $"V{i + 1}";
                    canvas.DrawText(label, vertices[i].X + 10, vertices[i].Y - 10, paint);
                }
            }

            // Draw the shape centroid
            using (var paint = new SKPaint { Color = SKColors.Red, StrokeWidth = 4, IsStroke = true })
            {
                canvas.DrawCircle(centroid.X, centroid.Y, 2, paint);
            }
            using (var paint = new SKPaint { Color = SKColors.Black, TextSize = 12, IsAntialias = true })
            {
                canvas.DrawText("C", centroid.X + 10, centroid.Y - 10, paint);
            }

            // Draw the legend in the top-left corner
            using (var paint = new SKPaint { Color = SKColors.Black, TextSize = 12, IsAntialias = true })
            {
                canvas.Save();
                canvas.ResetMatrix();
                for (int i = 0; i < vertices.Length; i++)
                {
                    string legend = $"V{i + 1} - ({vertices[i].X}, {vertices[i].Y})";
                    canvas.DrawText(legend, 10, 20 + (i * 15), paint);
                }
                // Include the centroid in the legend
                string centroidLegend = $"C - ({centroid.X}, {centroid.Y})";
                canvas.DrawText(centroidLegend, 10, 20 + (vertices.Length * 15), paint);
                canvas.Restore();
            }

            // Save the image
            using var image = surface.Snapshot();
            using var data = image.Encode(SKEncodedImageFormat.Png, 100);
            using var stream = File.OpenWrite(outputPath);
            data.SaveTo(stream);
        }
    }
}