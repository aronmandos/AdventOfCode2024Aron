namespace Day3;

internal static class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("# Day 3");
        Console.WriteLine("test:");
        DayThreePartOne(true);
        Console.WriteLine();
        DayThreePartOne(false);
    }

    private static void DayThreePartOne(bool testinput)
    {
        
        Console.WriteLine();
        var teststring = testinput ? "test" : "";
        Console.WriteLine($"## Day 3 Part One {teststring}");
        Console.WriteLine();
        
        var fileName = testinput ? "testinput.txt" : "input.txt";
        using var reader = File.OpenText(fileName);

    }
}