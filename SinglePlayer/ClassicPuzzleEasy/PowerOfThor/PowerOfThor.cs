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
        string[] inputs = Console.ReadLine().Split(' ');
        int lightX = int.Parse(inputs[0]); // the X position of the light of power
        int lightY = int.Parse(inputs[1]); // the Y position of the light of power
        int initialTX = int.Parse(inputs[2]); // Thor's starting X position
        int initialTY = int.Parse(inputs[3]); // Thor's starting Y position

        while (true)
        {
            int remainingTurns = int.Parse(Console.ReadLine());
            string direction = "";

            if ((initialTX > lightX)) {
                direction = "W";
                initialTX -= 1;
            } else if (initialTX < lightX) {
                direction = "E";
                initialTX += 1;
            }

            if (initialTY > lightY) {
                direction = "N" + direction;
                initialTY -= 1;
            } else if (initialTY < lightY) {
                direction = "S" + direction;
                initialTY += 1;
            }

            Console.WriteLine(direction);
        }
    }
}
