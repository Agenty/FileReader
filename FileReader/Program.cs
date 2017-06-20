using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileReader.Core;
using System.IO;

namespace FileReader
{
    class Program
    {
        static void Main(string[] args)
        {
            string Path = @"C:\sample.txt";

            if (File.Exists(Path)) // Check if local path is valid
            {
                var table = Path.FileToTable(heading: true, delimiter: '\t');
                    
                // Process your file here

                Console.WriteLine(table.Rows.Count);
                Console.Read();
            }
        }
    }
}
