using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;


class Player
{
    static void Main(string[] args)
    {
        int width = int.Parse(Console.ReadLine());
        int height = int.Parse(Console.ReadLine());
        bool[,] matrix = new bool[height, width];

        for (int i = 0; i < height; i++)
        {
            string line = Console.ReadLine();
            int charKey = 0;
            foreach (char c in line) {
                if (c == '0') {
                    matrix[i, charKey] = true;
                }
                charKey++;
            }
        }

        for (int h = 0; h < height; h++) {
            for (int w = 0; w < width; w++) {
                if (matrix[h, w]) {
                    string res = w + " " + h + " ";

                    bool wFound = false;
                    int wClosestMatch = 0;
                    int wOffset = 1;
                    while (wOffset < width && !wFound) {
                        if ((w + wOffset < width) && matrix[h, w + wOffset]) {
                            wClosestMatch = w + wOffset;
                            wFound = true;
                        }
                        wOffset++;
                    }

                    if (wFound) {
                        res += (wClosestMatch) + " " + h + " ";
                    } else {
                        res += "-1 -1 ";
                    }

                    bool hFound = false;
                    int hClosestMatch = 0;
                    int hOffset = 1;
                    while (hOffset < height && !hFound) {
                        if ((h + hOffset < height) && matrix[h + hOffset, w]) {
                            hClosestMatch = h + hOffset;
                            hFound = true;
                        }
                        hOffset++;
                    }

                    if (hFound) {
                        res += w + " " + (hClosestMatch) + " ";
                    } else {
                        res += "-1 -1";
                    }
                    Console.WriteLine(res);
                }
            }
        }
    }
}
