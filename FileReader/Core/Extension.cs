using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileReader.Core
{
    static class Extension
    {
        public static DataTable FileToTable(this string path, bool heading = true, char delimiter = '\t')
        {
            var table = new DataTable();
            string headerLine = File.ReadLines(path).FirstOrDefault(); // Read the first row for headings
            string[] headers = headerLine.Split(delimiter);
            int skip = 1;
            int num = 1;
            foreach (string header in headers)
            {
                if (heading)
                    table.Columns.Add(header);
                else
                {
                    table.Columns.Add("Field" + num); // Create fields header if heading is false
                    num++;
                    skip = 0; // Don't skip the first row if heading is false
                }
            }
            foreach (string line in File.ReadLines(path).Skip(skip))
            {
                if (!string.IsNullOrEmpty(line))
                {
                    table.Rows.Add(line.Split(delimiter));
                }
            }
            return table;
        }

        public static DataTable FileToTable(this string path, bool heading = true, char delimiter = '\t', int offset = 0, int limit = 100000)
        {
            var table = new DataTable();
            string headerLine = File.ReadLines(path).FirstOrDefault(); // Read the first row for headings
            string[] headers = headerLine.Split(delimiter);
            int skip = 1;
            int num = 1;
            foreach (string header in headers)
            {
                if (heading)
                {
                    table.Columns.Add(header); 
                }
                else
                {
                    table.Columns.Add("Field" + num); // Create fields header if heading is false
                    num++;
                    skip = 0; // Don't skip the first row if heading is false
                }
            }

            foreach (string line in File.ReadLines(path).Skip(skip + offset).Take(limit))
            {
                if (!string.IsNullOrEmpty(line))
                {
                    table.Rows.Add(line.Split(delimiter));
                }
            }
            return table;
        }

        public static void TableToFile(this DataTable table, string path, bool append = true)
        {
            StringBuilder stringBuilder = new StringBuilder();
            if (!File.Exists(path) || !append)
                stringBuilder.AppendLine(string.Join("\t", table.Columns.Cast<DataColumn>().Select(arg => arg.ColumnName)));

            using (StreamWriter sw = new StreamWriter(path, append))
            {
                foreach (DataRow dataRow in table.Rows)
                    stringBuilder.AppendLine(string.Join("\t", dataRow.ItemArray.Select(arg => arg.ToString())));
                sw.Write(stringBuilder.ToString());
            }
        }
    }
}
