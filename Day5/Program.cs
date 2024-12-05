namespace Day5;

internal static class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("# Day 5");
        //DayFivePartOne(true);
        Console.WriteLine();
        DayFivePartOne(false);
    }

    private static void DayFivePartOne(bool testInput)
    {
        Console.WriteLine();
        var testString = testInput ? "test" : "";
        Console.WriteLine($"## Day 5 Part 1 {testString}");
        Console.WriteLine();

        var rulesFileName = testInput ? "testRules.txt" : "rules.txt";
        var updatesFileName = testInput ? "testUpdates.txt" : "updates.txt";
        using var rulesReader = File.OpenText(rulesFileName);
        using var updatesReader = File.OpenText(updatesFileName);

        var rulesDictionary = new Dictionary<int, List<int>>();
        while (rulesReader.ReadLine() is { } line)
        {
            if (string.IsNullOrWhiteSpace(line)) continue;
            var parts = line.Split('|');
            var first = int.Parse(parts[0]);
            var second = int.Parse(parts[1]);
            
            var listExists = rulesDictionary.ContainsKey(first);
            if (!listExists)
            {
                rulesDictionary.Add(first, []);
            }
            var list = rulesDictionary[first];
            list.Add(second);
        }

        var middleTotal = 0;
        while (updatesReader.ReadLine() is { } line)
        {
            if (string.IsNullOrWhiteSpace(line)) continue;
            var update = Update.Parse(line);
            var violatedRule = false;
            foreach (var (pageNr, index) in update.Pages)
            {
                if (!rulesDictionary.TryGetValue(pageNr, out var rules)) continue;
                violatedRule = update.Pages.Any(p => rules.Contains(p.PageNr) && p.Index < index);
                if (violatedRule)
                {
                    break;
                }
            }
            if (!violatedRule) middleTotal += update.MiddlePage;
        }
        
        Console.WriteLine(middleTotal);
    }
}

internal class Update
{
    internal List<(int PageNr, int Index)> Pages { get; set; }

    internal int MiddlePageIndex => Pages.Count / 2;
    
    internal int MiddlePage => Pages[MiddlePageIndex].PageNr;

    internal static Update Parse(string input)
    {
        var parts = input.Split(',');
        var numbers = parts.Select((p, i) => (PageNr: int.Parse(p), Index: i)).ToList();
        return new Update()
        {
            Pages = numbers
        };
    }
}

internal class Rule
{
    internal int first { get; set; }
    internal int second { get; set; }

    internal static Rule Parse(string input)
    {
        var parts = input.Split('|');
        return new Rule()
        {
            first = int.Parse(parts[0]),
            second = int.Parse(parts[1])
        };
    }
}