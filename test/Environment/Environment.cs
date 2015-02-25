using System;
using System.IO;
using System.Text.RegularExpressions;

class Program
{
    static void Main()
    {
        string app_path = "%windir%\\%HOMEPATH%\\notepad.exe";

        Regex rgx = new Regex("%\\S+?%");

        Console.WriteLine("---");

        foreach(Match match in rgx.Matches(app_path))
        {

            string ev  = match.Value.Substring(1,match.Value.Length-2);
            string evv = Environment.GetEnvironmentVariable(ev);
            
            Console.WriteLine(ev);
            Console.WriteLine(evv);

            app_path = app_path.Replace(match.Value, evv);
        }

        Console.WriteLine(app_path);


    }
}