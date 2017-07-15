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
        int N = int.Parse(Console.ReadLine());
        int[] a = new int[N];

        for (int i = 0; i < N; i++)
        {
            int pi = int.Parse(Console.ReadLine());
            a[i] = pi;
        }

        Array.Sort(a);
        int minDiff = a[1]-a[0];
        for (int i = 2 ; i != a.Length ; i++) {
            minDiff = Math.Min(minDiff, a[i]-a[i-1]);
        }

        Console.WriteLine(minDiff);
    }
}
