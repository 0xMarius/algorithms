using System;
using System.Collections.Generic;

public class MazeGenerator
{
    private static Random random = new Random();

    private static int width;
    private static int height;
    private static int seed;
    private static int[][] grid;

    private static int N = 1;
    private static int S = 2;
    private static int E = 4;
    private static int W = 8;

    private static Dictionary<int, int> DX = new Dictionary<int, int>()
    {
        { E, 1 },
        { W, -1 },
        { N, 0 },
        { S, 0 }
    };

    private static Dictionary<int, int> DY = new Dictionary<int, int>()
    {
        { E, 0 },
        { W, 0 },
        { N, -1 },
        { S, 1 }
    };

    private static Dictionary<int, int> OPPOSITE = new Dictionary<int, int>()
    {
        { E, W },
        { W, E },
        { N, S },
        { S, N }
    };

    public static void Main(string[] args)
    {
        width = args.Length > 0 ? int.Parse(args[0]) : 10;
        height = args.Length > 1 ? int.Parse(args[1]) : width;

        Random random = new Random((int)DateTime.Now.Ticks);
        seed = args.Length > 2 ? int.Parse(args[2]) : random.Next();
        random = new Random(seed);
        
        grid = new int[height][];
        for (int i = 0; i < height; i++)
        {
            grid[i] = new int[width];
        }

        CarvePassagesFrom(0, 0);

        Console.WriteLine(" " + new string('_', width * 2 - 1));
        for (int y = 0; y < height; y++)
        {
            Console.Write("|");
            for (int x = 0; x < width; x++)
            {
                Console.Write((grid[y][x] & S) != 0 ? " " : "_");
                if ((grid[y][x] & E) != 0)
                {
                    Console.Write(((grid[y][x] | grid[y][x + 1]) & S) != 0 ? " " : "_");
                }
                else
                {
                    Console.Write("|");
                }
            }
            Console.WriteLine();
        }

        Console.WriteLine($"{AppDomain.CurrentDomain.FriendlyName} {width} {height} {seed}");
    }

    private static void CarvePassagesFrom(int cx, int cy)
    {
        List<int> directions = new List<int>() { N, S, E, W };

        // Shuffle direction list to mitigate direction bias
        directions.Sort((a, b) => random.Next(-1, 2));

        foreach (int direction in directions)
        {
            int nx = cx + DX[direction];
            int ny = cy + DY[direction];
            if (ny >= 0 && ny < grid.Length && nx >= 0 && nx < grid[ny].Length && grid[ny][nx] == 0)
            {
                grid[cy][cx] |= direction;
                grid[ny][nx] |= OPPOSITE[direction];
                CarvePassagesFrom(nx, ny);
            }
        }
    }
}

