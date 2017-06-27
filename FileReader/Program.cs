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

            ReadFullFile(Path); // Read full file

            ReadFileInBatch(Path); // Read 1 or more gb large file in chunks

            Console.Read();
        }

        /* Example 1 :: Read Full file */
        internal static void ReadFullFile(string Path)
        {
            if (File.Exists(Path)) // Check if local path is valid
            {
                var table = Path.FileToTable(heading: true, delimiter: '\t');

                // Process your file here

                table.TableToFile(@"C:\output.txt");
            }
        }

        /* Example 2 :: Read file in small chunks */
        internal static void ReadFileInBatch(string Path)
        {
            int TotalRows = File.ReadLines(Path).Count(); // Count the number of rows in file with lazy load
            int Limit = 100000;
            for (int Offset = 0; Offset < TotalRows; Offset += Limit)
            {
                // Print Logs
                string Logs = string.Format("Processing :: Rows {0} of Total {1} :: Offset {2} : Limit : {3}",
                    (Offset + Limit) < TotalRows ? Offset + Limit : TotalRows,
                    TotalRows, Offset, Limit
                );

                Console.WriteLine(Logs);

                var table = Path.FileToTable(heading: true, delimiter: '\t', offset : Offset, limit: Limit);

                // Do all your processing here and with limit and offset and save to drive in append mode
                // The append mode will write the output in same file for each processed batch.

                table.TableToFile(@"C:\output.txt");
            }
        }
    }
}
