using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;

class Game {
    private static Opponent opponent = new Opponent();
    private static Checkpoint checkpoint = new Checkpoint();
    private static Player player = new Player(opponent, checkpoint);
    private static LapManager lapManager = new LapManager(checkpoint);
    private static Logger logger = new Logger(player, opponent, checkpoint, lapManager);
    private static bool logEnabled = true;

    static void Main(string[] args) {
        while (true) {
            getInput();
            opponent.exec();
            string result = player.exec();
            if (logEnabled) logger.log();
            Console.WriteLine(checkpoint.x + " " + checkpoint.y + " " + result);
        }
    }

    private static void getInput() {
        string[] inputs;
        inputs = Console.ReadLine().Split(' ');
        player.x = int.Parse(inputs[0]);
        player.y = int.Parse(inputs[1]);
        checkpoint.x = int.Parse(inputs[2]);
        checkpoint.y = int.Parse(inputs[3]);
        checkpoint.dist = int.Parse(inputs[4]);
        checkpoint.setAngle(int.Parse(inputs[5]));
        inputs = Console.ReadLine().Split(' ');
        opponent.x = int.Parse(inputs[0]);
        opponent.y = int.Parse(inputs[1]);
        opponent.playerX = player.x;
        opponent.playerY = player.y;
    }
}

public class Player {
    public int x { get; set; }
    public int y { get; set; }
    public bool shieldUsed = false;
    public bool boostUsed = false;
    public int thrust = 100;
    private int breaking = 0;
    private Opponent opponent;
    private Checkpoint checkpoint;

    public Player(Opponent opponent, Checkpoint checkpoint) {
        this.opponent = opponent;
        this.checkpoint = checkpoint;
    }

    public string exec() {
        if (shouldBoost()) {
            this.boostUsed = true;
            return "BOOST";
        }

        if (shouldShield()) {
            this.shieldUsed = true;
            return "SHIELD";
        }

        this.thrust = calcThrust();
        return this.thrust.ToString();
    }

    private int calcThrust() {
        if (this.checkpoint.angle >= 135) {
            return 40;
        } else if (this.checkpoint.angle >= 90) {
            return 70;
        } else if (this.checkpoint.angle >= 45) {
            return 90;
        }
        return 100;
    }

    private int calcBreaking() {
        return 0;
    }

    private bool shouldBoost() {
        if (this.boostUsed) return false;
        if (this.checkpoint.angle < 5 && this.checkpoint.dist > 5000) {
            return true;
        }
        return false;
    }

    private bool shouldShield() {
        if (this.opponent.velocity > 200 && this.opponent.isApproaching && this.opponent.dist < 1000) {
            return true;
        }
        return false;
    }
}

public class Opponent {
    public int x { get; set; }
    public int y { get; set; }
    public int dist { get; set; }
    public int velocity { get; set; }
    public int angle { get; set; }
    public bool isApproaching { get; set; }
    public int playerX { get; set; }
    public int playerY { get; set; }
    private int lastDist = 0;

    public void exec() {
        this.dist = calcDist();
        this.angle = calcAngle();
        this.velocity = approachSpeed();
        this.isApproaching = calcIsApproaching();
        this.lastDist = this.dist;
    }

    private int calcDist() {
        double a = (double)(this.x - this.playerX);
        double b = (double)(this.y - this.playerY);
        return (int)Math.Sqrt((a * a) + (b * b));
    }

    private int calcAngle() {
        int deltaX = this.x - this.playerX;
        int deltaY = this.y - this.playerY;
        return (int)(Math.Atan2(deltaY, deltaX) * (180 / Math.PI) );
    }

    private bool calcIsApproaching() {
        return this.dist < lastDist ? true : false;
    }

    private int approachSpeed() {
        return this.dist < this.lastDist ? (this.lastDist - this.dist) : 0;
    }
}

public class Checkpoint {
    public int x { get; set; }
    public int y { get; set; }
    public int dist { get; set; }
    public int angle { get; set; }

    public void setAngle(int angle) {
        this.angle = angle < 0 ? (angle * -1) : angle;
    }
}

public class LapManager {
    Checkpoint checkpoint;

    public LapManager(Checkpoint checkpoint) {
        this.checkpoint = checkpoint;
    }
}

public class Logger {
    private Player player;
    private Opponent opponent;
    private Checkpoint checkpoint;
    private LapManager lapManager;

    public Logger(Player player, Opponent opponent, Checkpoint checkpoint, LapManager lapManager) {
        this.player = player;
        this.opponent = opponent;
        this.checkpoint = checkpoint;
        this.lapManager = lapManager;
    }

    public void log() {
        Console.Error.WriteLine("Player:______________________________ ");
        Console.Error.WriteLine("X: " + player.x + " Y: " + player.y + " | Boost: " + player.boostUsed + " Shield: " + player.shieldUsed);
        Console.Error.WriteLine("Thrust " + player.thrust );

        Console.Error.WriteLine("Opponent:___________________________ ");
        Console.Error.WriteLine("Dist: " + opponent.dist + " Angle: " + opponent.angle);
        Console.Error.WriteLine("Approach: " + opponent.isApproaching + " ApSpeed: " + opponent.velocity);

        Console.Error.WriteLine("Checkpoint:_________________________ ");
        Console.Error.WriteLine("NCX: " + checkpoint.x + " NCY: " + checkpoint.y);
        Console.Error.WriteLine("Dist: " + checkpoint.dist +  " Angle: " + checkpoint.angle);

        // if (isLapMapped) {
        //     Console.Error.WriteLine("Lap Mapping: COMPLETE_______________ ");
        // } else {
        //     Console.Error.WriteLine("Lap Mapping: IN PROGRESS____________ ");
        // }
        // Console.Error.WriteLine("Checkpoints: " + currentCheckpoint + "/" + countCheckpoints + " Laps: " + currentLap + "/" + totalLaps);
    }
}
