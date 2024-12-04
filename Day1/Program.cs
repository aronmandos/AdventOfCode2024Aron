using System.Text.Json;

namespace ConsoleApp1;

internal static class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("# Day 1");
        Console.WriteLine();
        DayOnePartOne();
        DayOnePartTwo();
        
        Console.WriteLine();
        Console.WriteLine("# Ending");
    }

    private static void DayOnePartOne()
    {
        
        Console.WriteLine();
        Console.WriteLine("## Day 1 Part 1");
        Console.WriteLine();
        
        var fileName = "input.txt";
        using StreamReader reader = File.OpenText(fileName);


        var list = new List<(string left, string right)>();
        var listLeft = new List<double>();
        var listRight = new List<double>();
        while (reader.ReadLine() is { } line)
        {
            if (string.IsNullOrWhiteSpace(line)) continue;
            try
            {
                var item = line.Split("   ");
                list.Add((item[0], item[1]));
                listLeft.Add(Convert.ToDouble(item[0]));
                listRight.Add(Convert.ToDouble(item[1]));
            }
            catch (Exception e)
            {
                Console.WriteLine(line);
                Console.WriteLine();
                Console.WriteLine(e);
                throw;
            }
        }

        var leftOrdered = listLeft.Order().ToList();
        var rightOrdered = listRight.Order().ToList();

        var maxLength = leftOrdered.Count > rightOrdered.Count ? leftOrdered.Count : rightOrdered.Count;
        var minLength = leftOrdered.Count > rightOrdered.Count ? rightOrdered.Count : leftOrdered.Count;

        var total = 0.0;
        for (int i = 0; i < minLength; i++)
        {
            Console.WriteLine($"{leftOrdered[i]}, {rightOrdered[i]} :: {Math.Abs(leftOrdered[i] - rightOrdered[i])}");
            total += Math.Abs(leftOrdered[i] - rightOrdered[i]);
        }

        Console.WriteLine(total);
    }


    private static void DayOnePartTwo()
    {
        Console.WriteLine();
        Console.WriteLine("## Day 1 Part 2");
        Console.WriteLine();
        const string fileName = "input.txt";
        using var reader = File.OpenText(fileName);


        var list = new List<(string left, string right)>();
        var listLeft = new List<double>();
        var listRight = new List<double>();
        while (reader.ReadLine() is { } line)
        {
            if (string.IsNullOrWhiteSpace(line)) continue;
            try
            {
                var item = line.Split("   ");
                list.Add((item[0], item[1]));
                listLeft.Add(Convert.ToDouble(item[0]));
                listRight.Add(Convert.ToDouble(item[1]));
            }
            catch (Exception e)
            {
                Console.WriteLine(line);
                Console.WriteLine();
                Console.WriteLine(e);
                throw;
            }
        }

        var leftOrdered = listLeft.Order().ToList();
        var rightOrdered = listRight.Order().ToList();

        var maxLength = leftOrdered.Count > rightOrdered.Count ? leftOrdered.Count : rightOrdered.Count;
        var minLength = leftOrdered.Count > rightOrdered.Count ? rightOrdered.Count : leftOrdered.Count;

        var total = 0.0;

        foreach (var l in leftOrdered)
        {
            var rCount = rightOrdered.Count(r => Math.Abs(r - l) < 0.01);
            total += l * rCount;
        }

        Console.WriteLine(total);
    }
}