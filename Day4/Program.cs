namespace Day4;

internal static class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("# Day 3");
        DayFourPartOne(true);
        Console.WriteLine();
        DayFourPartOne(false);
        //
        // Console.WriteLine();
        // DayFourPartTwo(true);
        // Console.WriteLine();
        // DayFourPartTwo(false);
    }

    private static void DayFourPartOne(bool testInput)
    {
        Console.WriteLine();
        var testString = testInput ? "test" : "";
        Console.WriteLine($"## Day 4 Part 1 {testString}");
        Console.WriteLine();

        var fileName = testInput ? "testinput.txt" : "input.txt";
        using var reader = File.OpenText(fileName);
        //var fullText = reader.ReadToEnd();
    }

    private static void DayFourPartTwo(bool testInput)
    {
        Console.WriteLine();
        var testString = testInput ? "test" : "";
        Console.WriteLine($"## Day 4 Part 2 {testString}");
        Console.WriteLine();

        var fileName = testInput ? "testinput.txt" : "input.txt";
        using var reader = File.OpenText(fileName);
        //var fullText = reader.ReadToEnd();
    }
}