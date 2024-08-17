using System;
using System.IO;
using System.Collections.Generic;

namespace ShapeCalculator
{
    class FileReader
    {
        public string[] ReadFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                return File.ReadAllLines(filePath);
            }
            else
            {
                throw new FileNotFoundException("The specified file was not found.");
            }
        }
    }

}