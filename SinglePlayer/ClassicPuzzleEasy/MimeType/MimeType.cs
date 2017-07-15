using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;

class Solution
{
    static void Main(string[] args)
    {
        List<string> extensions = new List<string>();
        List<string> mimeTypes = new List<string>();
        List<string> fileNames = new List<string>();

        int N = int.Parse(Console.ReadLine()); // Number of elements which make up the association table.
        int Q = int.Parse(Console.ReadLine()); // Number Q of file names to be analyzed.
        for (int i = 0; i < N; i++)
        {
            string[] inputs = Console.ReadLine().Split(' ');
            string EXT = inputs[0]; // file extension
            string MT = inputs[1]; // MIME type.
            extensions.Add(EXT.ToUpper());
            mimeTypes.Add(MT);
        }
        for (int i = 0; i < Q; i++)
        {
            string FNAME = Console.ReadLine(); // One file name per line.
            fileNames.Add(FNAME.ToUpper());
        }

        foreach(string file in fileNames) {
            if (file.Contains('.') && (file.Split('.').Last().Length <= 10)) {
                string fileExtension = file.Split('.').Last();
                string found = "NONE";
                int i = 0;
                foreach (string ex in extensions) {
                    if (fileExtension == ex) {
                        found = mimeTypes[i];
                    }
                    i++;
                }
                if (found != "NONE") {
                    Console.Error.WriteLine(file + " | " + fileExtension + " | " + found);
                    Console.WriteLine(found);
                    found = "NONE";
                } else {
                    Console.Error.WriteLine(file + " | " + fileExtension + " | " + found);
                    Console.WriteLine("UNKNOWN");
                }
            } else {
                Console.WriteLine("UNKNOWN");
            }
        }
    }
}
