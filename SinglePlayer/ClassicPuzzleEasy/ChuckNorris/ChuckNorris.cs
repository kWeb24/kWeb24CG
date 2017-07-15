using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;

class Solution
{
    static string vals = "";
    static void Main(string[] args)
    {
        string MESSAGE = Console.ReadLine();
        string answer = "";
        List<int> digits = new List<int>();
        List<int> tmp = new List<int>();
        string msg = "";

        for (int i = 0; i <= MESSAGE.Length - 1; i++ ) {
            string binary = Convert.ToString((int)MESSAGE[i], 2);

            if (binary.Length < 7) {
                string addZeros = "";
                for (int x = 0; x <= (7 - binary.Length) - 1; x++) {
                    addZeros += "0";
                }
                binary = addZeros + binary;
            }

            msg += binary;
        }

        tmp = countDigits(msg);
        foreach (int block in tmp) {
            digits.Add(block);
        }
        tmp.Clear();

        int pos = 0;
        foreach (int block in digits) {
            if (vals[pos].ToString() == "0") { answer += "00 "; } else { answer += "0 "; }
            string tmpDigs = "";
            for (int x = 0; x <= block - 1; x++) {
                tmpDigs += "0";
            }
            answer += tmpDigs + " ";
            pos++;
        }
        answer = answer.Remove(answer.Length - 1);
        Console.WriteLine(answer);
    }

    static List<int> countDigits(string binary) {
        List<int> digits = new List<int>();
        int counter = 0;
        int tmp = (int)binary[0];
        for (int i = 0; i <= binary.Length - 1; i++) {
            if (binary[i] == tmp) {
                counter++;
                tmp = (int)binary[i];
            } else {
                digits.Add(counter);
                vals += binary[i - 1];
                counter = 1;
                tmp = (int)binary[i];
            }
        }
        digits.Add(counter);
        vals += binary[binary.Length - 1];
        return digits;
    }
}
