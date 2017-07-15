using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;

class Player {
    static void Main(string[] args) {
        string[] inputs;

        string thrust = "0";
        int tmpThrust = 0;
        bool boostUsed = false;
        bool isShielded = false;

        int opponentDist = 0;
        bool isApproaching = false;
        int lastDist = 0;
        int opponentVelocity = 0;
        int opponentAngle = 0;

        bool isLapMapped = false;
        List<string> _checkpoints = new List<string>();
        int countCheckpoints = 0;
        int currentCheckpoint = 0;
        int currentLap = 1;
        const int totalLaps = 3;
        int nextCheckpointXOld = 0;
        bool isLapChanged = false;

        while (true) {
            inputs = Console.ReadLine().Split(' ');
            int x = int.Parse(inputs[0]);
            int y = int.Parse(inputs[1]);
            int nextCheckpointX = int.Parse(inputs[2]); // x position of the next check point
            int nextCheckpointY = int.Parse(inputs[3]); // y position of the next check point
            int nextCheckpointDist = int.Parse(inputs[4]); // distance to the next checkpoint
            int nextCheckpointAngle = int.Parse(inputs[5]); // angle between your pod orientation and the direction of the next checkpoint
            inputs = Console.ReadLine().Split(' ');
            int opponentX = int.Parse(inputs[0]);
            int opponentY = int.Parse(inputs[1]);

            if (!isLapMapped) {
                string mapResult = mapLaps(nextCheckpointX, nextCheckpointY, nextCheckpointAngle, nextCheckpointDist, _checkpoints);
                if (mapResult == "DONE") {
                    isLapMapped = true;
                    currentLap++;
                } else if (mapResult != "WAIT") {
                    _checkpoints.Add(mapResult);
                    countCheckpoints++;
                    currentCheckpoint++;
                }
            } else {
                if (updateCurrentLap(currentCheckpoint, countCheckpoints, isLapChanged) == 1) { isLapChanged = true; }
                if (updateCurrentCheckpoint(currentCheckpoint, countCheckpoints, nextCheckpointXOld, nextCheckpointX) != currentCheckpoint) { isLapChanged = false; }
                currentCheckpoint = updateCurrentCheckpoint(currentCheckpoint, countCheckpoints, nextCheckpointXOld, nextCheckpointX);
                currentLap += updateCurrentLap(currentCheckpoint, countCheckpoints, isLapChanged);
            }
            nextCheckpointXOld = nextCheckpointX;

            opponentDist = getEnemyDist(x, y, opponentX, opponentY);
            isApproaching = isOpponentApproach(opponentDist, lastDist);
            opponentVelocity = getApproachSpeed(opponentDist, lastDist);
            lastDist = opponentDist;
            opponentAngle = getOpponentAngle(x, y, opponentX, opponentY);

            tmpThrust = calculateThrust(nextCheckpointAngle);
            tmpThrust -= calculateBreaking(nextCheckpointDist, nextCheckpointAngle);
            if (tmpThrust < 0) tmpThrust = 0;
            if (tmpThrust > 100) tmpThrust = 100;
            thrust = tmpThrust.ToString();
            if (calculateBoost(boostUsed, nextCheckpointDist, nextCheckpointAngle, opponentDist, opponentVelocity, currentLap, totalLaps)) { thrust = "BOOST"; boostUsed = true; }
            if (calculateShields(opponentVelocity, opponentDist, nextCheckpointDist)) { thrust = "SHIELD"; }

            Console.Error.WriteLine("Input:______________________________ ");
            Console.Error.WriteLine("X: " + x + " Y: " + y + " | OX: " + opponentX + " OY: " + opponentY);
            Console.Error.WriteLine("NCX: " + nextCheckpointX + " NCY: " + nextCheckpointY + " Dist: " + nextCheckpointDist +  " Angle: " + nextCheckpointAngle);
            Console.Error.WriteLine("Player:_____________________________ ");
            Console.Error.WriteLine("Boost: " + boostUsed + " Shield: " + isShielded);
            Console.Error.WriteLine("Breaking: " + calculateBreaking(nextCheckpointDist, nextCheckpointAngle) + " Thrust {TT: " + tmpThrust + " | RT: " + thrust + "}");
            Console.Error.WriteLine("Opponent:___________________________ ");
            Console.Error.WriteLine("Dist: " + opponentDist + " Angle: " + opponentAngle);
            Console.Error.WriteLine("Approach: " + isApproaching + " ApSpeed: " + opponentVelocity);
            Console.Error.WriteLine("Checkpoint:_________________________ ");
            Console.Error.WriteLine("Dist: " + nextCheckpointDist + " Angle: " + nextCheckpointAngle);
            if (isLapMapped) {
                Console.Error.WriteLine("Lap Mapping: COMPLETE_______________ ");
            } else {
                Console.Error.WriteLine("Lap Mapping: IN PROGRESS____________ ");
            }
            Console.Error.WriteLine("Checkpoints: " + currentCheckpoint + "/" + countCheckpoints + " Laps: " + currentLap + "/" + totalLaps);

            Console.WriteLine(nextCheckpointX + " " + nextCheckpointY + " " + thrust);
        }
    }

    private static int calculateThrust(int nextCheckpointAngle) {
        int thrust = 0;
        if (nextCheckpointAngle < 0) nextCheckpointAngle *= -1;

        if (nextCheckpointAngle > 140) {
            thrust = 100;
        } else if (nextCheckpointAngle > 90) {
            thrust = 100;
        } else {
            thrust = 100;
        }


        if (thrust < 0) thrust = 0;
        if (thrust > 100) thrust = 100;
        return thrust;
    }

    private static int calculateBreaking(int nextCheckpointDist, int nextCheckpointAngle) {

        if (nextCheckpointDist < 4000 && nextCheckpointAngle > 15) {
            return 80;
        }

        return 0;
    }

    private static bool calculateBoost(bool boostUsed, int nextCheckpointDist, int nextCheckpointAngle, int opponentDist, int opponentVelocity, int currentLap, int totalLaps) {
        if ((!boostUsed) && /*(currentLap != 1) &&*/ (nextCheckpointDist > 8000) && (nextCheckpointAngle < 5) && (nextCheckpointAngle > -5) && ((opponentDist > 800))) {
            return true;
        } else if ((!boostUsed) && (currentLap == totalLaps) && (nextCheckpointAngle < 5) && (nextCheckpointAngle > -5)) {
            return true;
        } else {
            return false;
        }
    }

    private static bool calculateShields(int opponentVelocity, int opponentDist, int nextCheckpointDist) {
        if (opponentVelocity > 100 && opponentDist < 800) { return true; } else { return false; }
    }

    private static int getEnemyDist(int x, int y, int opponentX, int opponentY) {
        double a = (double)(opponentX - x);
        double b = (double)(opponentY - y);
        return (int)Math.Sqrt((a * a) + (b * b));
    }

    private static bool isOpponentApproach(int opponentDist, int lastOpponentDist) {
        return opponentDist < lastOpponentDist ? true : false;
    }

    private static int getApproachSpeed(int opponentDist, int lastOpponentDist) {
        return opponentDist < lastOpponentDist ? (lastOpponentDist - opponentDist) : 0;
    }

    /* to fix */
    private static int getOpponentAngle(int x, int y, int opponentX, int opponentY) {
        int deltaY = opponentY - y;
        int deltaX = opponentX - x;
        return (int)(Math.Atan2(deltaY, deltaX) * (180 / Math.PI) );
    }

    private static string mapLaps(int nextCheckpointX, int nextCheckpointY, int nextCheckpointAngle, int nextCheckpointDist, List<string> _checkpoints) {
        if (_checkpoints.Count > 0) {
            string[] firstCheckpoint = _checkpoints[0].Split(';');
            string[] lastCheckpoint = _checkpoints[_checkpoints.Count -1].Split(';');

            if (firstCheckpoint[0] == nextCheckpointX.ToString() && firstCheckpoint[1] == nextCheckpointY.ToString() && _checkpoints.Count > 1) {
                return "DONE";
            }

            if (lastCheckpoint[0] == nextCheckpointX.ToString() && lastCheckpoint[1] == nextCheckpointY.ToString()) {
                return "WAIT";
            }
        }

        return nextCheckpointX.ToString() + ";" + nextCheckpointY.ToString() + ";" + nextCheckpointAngle.ToString() + ";" + nextCheckpointDist.ToString();
    }
    /*to doooo */
    private static int updateCurrentCheckpoint(int currentCheckpoint, int countCheckpoints, int nextCheckpointXOld, int nextCheckpointX) {
        if (nextCheckpointXOld != nextCheckpointX) {
            return currentCheckpoint == countCheckpoints ? 1 : currentCheckpoint + 1;
        }
        return currentCheckpoint;
    }

    private static int updateCurrentLap (int currentCheckpoint, int countCheckpoints, bool isLapChanged) {
        return (currentCheckpoint == countCheckpoints && !isLapChanged) ? 1 : 0;
    }

}
