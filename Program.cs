using System;
using System.Runtime.CompilerServices;
using Microsoft.VisualBasic;

namespace ShapeCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            var inputFilePath = "./Machine Vision Development Engineer Coding Exercise _ ShapeList2.csv";
            var outputPath = "./output.csv";

            var calculator = new Calculator();
            var reader = new FileReader();

            string[] data;
            try
            {
                data = reader.ReadFile(inputFilePath);
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }

            List<string[]> output = calculator.ProcessShapeData(data);

            try
            {
                using (StreamWriter writer = new StreamWriter(outputPath))
                {
                    foreach (string[] row in output)
                    {
                        // Join the row into a single comma-separated string
                        string rowString = string.Join(',', row);
                        writer.WriteLine(rowString);
                    }
                }
                Console.WriteLine("Data successfully written to output.csv");
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while writing to the file: " + ex.Message);
            }
        }
    }
}