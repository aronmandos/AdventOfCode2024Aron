using System.Text.RegularExpressions;

namespace Day3;

internal static partial class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("# Day 3");
        DayThreePartOne(true);
        Console.WriteLine();
        DayThreePartOne(false);
        
        Console.WriteLine();
        
        DayThreePartTwo(true);
        Console.WriteLine();
        DayThreePartTwo(false);
    }

    private static void DayThreePartOne(bool testinput)
    {
        
        Console.WriteLine();
        var teststring = testinput ? "test" : "";
        Console.WriteLine($"## Day 3 Part One {teststring}");
        Console.WriteLine();
        
        var fileName = testinput ? "testinput.txt" : "input.txt";
        using var reader = File.OpenText(fileName);
        var fullText = reader.ReadToEnd();
        const string pattern = @"/(mul\()(\d{1,3},\d{1,3})(\))/g";
        var rg = MulRegex();
        var matches = rg.Matches(fullText);
        var totalResult = 0;
        var index = 0;
        
        Console.WriteLine("matches: " + matches.Count);
        foreach (Match match in matches)
        {
            try
            {
                var range = match.Groups[2].Value;
                var numbers = range.Split(',');
                if (!int.TryParse(numbers[0], out var left) )
                {
                    Console.WriteLine($"error-left: {left}");
                }
                if (!int.TryParse(numbers[1], out var right) )
                {
                    Console.WriteLine($"error-right: {right}");
                }
                var result = left * right;
                Console.WriteLine($"{index.ToString(),3} :: {match.ToString(), 14} :: {left,3}*{right,3} = {result,8}");
                totalResult += result;
            }
            catch (Exception e)
            {
                Console.WriteLine(match);
            }

            index++;
        }


        Console.WriteLine("totalResult: " +totalResult);
    }
    
    private static void DayThreePartTwo(bool testinput)
    {
        
        Console.WriteLine();
        var teststring = testinput ? "test" : "";
        Console.WriteLine($"## Day 3 Part Two {teststring}");
        Console.WriteLine();
        
        var fileName = testinput ? "testinputPart2.txt" : "input.txt";
        using var reader = File.OpenText(fileName);
        var fullText = reader.ReadToEnd();
        
        var mulMatches = MulRegex().Matches(fullText).Select(m => (m.Index, m));
        var doMatches = DoRegex().Matches(fullText).Select(m => (m.Index, m));
        var dontMatches = DontRegex().Matches(fullText).Select(m => (Index: m.Index, Match: m));
        var merged = mulMatches.Concat(doMatches);
        merged = merged.Concat(dontMatches).OrderBy(m => m.Index);

        var matches = new List<Match>();
        var doing = true;
        foreach (var (_, match) in merged)
        {
            switch (match.ToString())
            {
                case "do()":
                    doing = true;
                    break;
                case "don't()":
                    doing = false;
                    break;
                default:
                    if(doing) matches.Add(match);
                    break;
            }
        }
        
        
        
        var totalResult = 0;
        var index = 0;
        
        Console.WriteLine("matches: " + matches.Count);
        foreach (Match match in matches)
        {
            try
            {
                
                var range = match.Groups[2].Value;
                var numbers = range.Split(',');
                if (!int.TryParse(numbers[0], out var left) )
                {
                    Console.WriteLine($"error-left: {left}");
                }
                if (!int.TryParse(numbers[1], out var right) )
                {
                    Console.WriteLine($"error-right: {right}");
                }
                var result = left * right;
                Console.WriteLine($"{index.ToString(),3} :: {match, 14} :: {left,3}*{right,3} = {result,8}");
                totalResult += result;
            }
            catch (Exception e)
            {
                Console.WriteLine(match);
            }

            index++;
        }


        Console.WriteLine("totalResult: " +totalResult);
    }

    [GeneratedRegex(@"(mul\()(\d{1,3},\d{1,3})(\))")]
    private static partial Regex MulRegex();
    
    [GeneratedRegex(@"(do\(\))")]
    private static partial Regex DoRegex();
    
    [GeneratedRegex(@"(don't\(\))")]
    private static partial Regex DontRegex();
}