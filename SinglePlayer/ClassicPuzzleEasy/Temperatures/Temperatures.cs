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
        int n = int.Parse(Console.ReadLine()); // the number of temperatures to analyse
        string temps = Console.ReadLine(); // the n temperatures expressed as integers ranging from -273 to 5526

        string[] vals = temps.Split(' ');

        int closest_negative = -274;
        int closest_positive = 9999;
        bool stop = false;
        Console.Error.WriteLine(temps);

        if (n != 0) {
            for (int x = 0; x < n; x++) {
                //valsInt[x] = Int32.Parse(vals[x]);
                int tmpVal = Int32.Parse(vals[x]);

                if(tmpVal == 0) {
                    //Console.WriteLine("0"); stop = true;
                } else if ((tmpVal < 0) && (tmpVal > closest_negative)) {
                    closest_negative = tmpVal;
                } else if ((tmpVal > 0) && (tmpVal < closest_positive)) {
                    closest_positive = tmpVal;
                }

            }
        } else {
            Console.WriteLine("0"); stop = true;
        }

        if (!stop) {
            int tmp_negative = closest_negative * -1;

            if (closest_negative == -274) { Console.WriteLine(closest_positive);
            } else if (closest_positive == 9999) { Console.WriteLine(closest_negative);
            } else
            if (closest_positive < tmp_negative) {
                Console.WriteLine(closest_positive);
            } else if (closest_positive > tmp_negative) {
                Console.WriteLine(closest_negative);
            } else {
                Console.WriteLine(closest_positive);
            }
        }
    }
}
