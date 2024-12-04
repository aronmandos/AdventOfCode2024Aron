using System.Text.Json;

namespace Day2;

internal static class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("# Day 2");
        Console.WriteLine("test:");
        DayTwoPartOne(true);
        Console.WriteLine();
        DayTwoPartOne(false);
    }

    private static void DayTwoPartOne(bool testinput)
    {
        
        Console.WriteLine();
        var teststring = testinput ? "test" : "";
        Console.WriteLine($"## Day 2 Part One {teststring}");
        Console.WriteLine();
        
        var fileName = testinput ? "testinput.txt" : "input.txt";
        using var reader = File.OpenText(fileName);


        var totalSafe = 0;
        var lineNr = 0;
        while (reader.ReadLine() is { } line)
        {
            if (string.IsNullOrWhiteSpace(line)) continue;
            var levels = line.Split(' ');
            var levelsAsInt = levels.Where(l => !string.IsNullOrWhiteSpace(l)).Select(int.Parse).ToList();
            var safe = IsReportSafe(levelsAsInt);
            if (!safe)
            {
                for (var i = 0; i < levelsAsInt.Count; i++)
                {
                    var newReport = levelsAsInt.Where((_, ii) => ii != i).ToList();
                    safe = IsReportSafe(newReport);
                    if (testinput && lineNr == 3)Console.WriteLine($"--- \"{JsonSerializer.Serialize(newReport)}\" :: {safe}");
                    if (safe) break;
                }
            }
            if (testinput) Console.WriteLine($"\"{line}\" :: {safe}");
            if (safe)
            {
                totalSafe += 1;
            }

            lineNr += 1;
        }
        
        Console.WriteLine(totalSafe);
    }

    private static bool IsReportSafe(List<int> levelsAsInt)
    {
        bool? ascending = null;

        for (var i = 1; i < levelsAsInt.Count; i++)
        {
            var prev = levelsAsInt[i - 1];
            var curr = levelsAsInt[i];
            var diff = Math.Abs(prev - curr);
            ascending ??= curr > prev;
            
            if ((diff is < 1 or > 3) || (ascending is true && curr < prev) || (ascending is false && curr > prev))
            {
                return false;
            }
        }

        return true;
    }
}