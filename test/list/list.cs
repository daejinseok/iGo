using System;
using System.IO;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        struct cc
        {
            int count;
            string cmd;
        }

        List<int, string> l = new List<int, string>();

        l.Add(5, "daejin5");
        l.Add(4, "daejin4");


        foreach( int i, string s in l)
        {
            Console.WriteLine(i);

        }

    }
}