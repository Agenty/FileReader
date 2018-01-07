# FileReader

FileReader is a c# library open sourced by [Agenty](https://www.agenty.com) used to read and process very large text files with pagination by setting ``limit`` and ``offset`` parameters.

Because loading the whole text file in memory will cause objects to grow, and .net will throw OOM exceptions if it cannot allocate enough contiguous memory for an object.

So FileReader architecture is designed to stream the file with pagination instead reading the entire content in one go. Which prevents the ``out of memory execption`` when you want to read big TXT files, which is in size around 500 MB or more

## Delimiter
  - Tab (``\t``)
  - Comma (``,``)

## Basic Example 

```
string Path = @"C:\sample.txt";
var table = Path.FileToTable(heading: true, delimiter: '\t');

// All your processing here

table.TableToFile(@"C:\output.txt");
```

## Pagination Example 

```
int Offset = 0;
int Limit = 100000
string Path = @"C:\sample.txt";
var table = Path.FileToTable(heading: true, delimiter: '\t', offset : Offset, limit: Limit);

// Do all your processing here and with limit and offset and save to drive in append mode
// The append mode will write the output in same file for each processed batch.

table.TableToFile(@"C:\output.txt");
```
