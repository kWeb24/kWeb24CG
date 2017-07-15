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
        string LON = Console.ReadLine();
        string LAT = Console.ReadLine();
        int N = int.Parse(Console.ReadLine());

        string closestName = "NONE";
        double closestResult = 0;
        double MY_LON = Convert.ToDouble(LON.Replace(',','.'));
        double MY_LAN = Convert.ToDouble(LAT.Replace(',','.'));

        for (int i = 0; i < N; i++)
        {
            string DEFIB = Console.ReadLine();
            string[] splits = DEFIB.Split(';');
            double DEFIB_LON = Convert.ToDouble(splits[splits.Length - 2].Replace(',','.'));
            double DEFIB_LAN = Convert.ToDouble(splits[splits.Length - 1].Replace(',','.'));

            double d = Math.Sqrt( ((MY_LON-DEFIB_LON) * (MY_LON-DEFIB_LON)) + ((MY_LAN-DEFIB_LAN) * (MY_LAN-DEFIB_LAN)) );

            if (d < closestResult || closestName == "NONE") {
                closestResult = d;
                closestName = splits[1];
            }
        }
        Console.WriteLine(closestName);
    }
}
