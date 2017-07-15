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
        int L = int.Parse(Console.ReadLine()); //letter width
        int H = int.Parse(Console.ReadLine()); //letter height
        string T = Console.ReadLine().ToUpper(); //-65
        int separation = 1;
        int chars = T.Length;

        for (int i = 0; i < H; i++)
        {
            string ROW = Console.ReadLine();
            string answer = "";
            // foreach char
            for (int x = 0; x <= chars - 1; x++) {
                int currentChar = (int)T[x] - 65;
                int padding = T[x] == 'A' ? 1 : (separation + (L * currentChar));

                if ( ((int)T[x] < 65) || ((int)T[x] > 90) ) {
                    currentChar = 26;
                    padding = (separation + (L * currentChar));
                }

                // for char length
                for (int y = 0; y <= L - 1; y++) {
                    answer += ROW[(padding - 1) + y];
                }

            }
            Console.WriteLine(answer);        
        }
    }
}
