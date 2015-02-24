using System;
using System.IO;

class Program
{
    static void Main()
    {
  // Put all file names in root directory into array.
  string[] array1 = Directory.GetFiles(@"C:\");

  // Put all txt files in root directory into array.
  string[] array2 = Directory.GetFiles(@"\\vision\00. Software"); // <-- Case-insensitive

  // Display all files.
  Console.WriteLine("--- Files: ---");
  foreach (string name in array1)
  {
      Console.WriteLine(name);
  }

  // Display all BIN files.
  Console.WriteLine("--- BIN Files: ---");
  foreach (string name in array2)
  {
      Console.WriteLine(name);
  }
    }
}