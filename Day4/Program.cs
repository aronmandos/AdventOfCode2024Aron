using System.Runtime.Intrinsics.X86;

namespace Day4;

internal static class Program
{
    private static void Main(string[] args)
    {
        // Console.WriteLine("# Day 3");
        // DayFourPartOne(true);
        // Console.WriteLine();
        // DayFourPartOne(false);

        Console.WriteLine();
        DayFourPartTwo(true);
        Console.WriteLine();
        DayFourPartTwo(false);
    }

    private static void DayFourPartOne(bool testInput)
    {
        Console.WriteLine();
        var testString = testInput ? "test" : "";
        Console.WriteLine($"## Day 4 Part 1 {testString}");
        Console.WriteLine();

        var fileName = testInput ? "testinput.txt" : "input.txt";
        using var reader = File.OpenText(fileName);

        var gridList = new List<(int Line, int Column, char Character)>();
        var currLine = 0;
        while (reader.ReadLine() is { } line)
        {
            if (string.IsNullOrWhiteSpace(line)) continue;
            gridList.AddRange(line.Where(c => "XMAS".Contains(c))
                .Select((x, i) => (Line: currLine, Column: i, Character: x)));
            currLine++;
        }

        var directions = new List<(int lineDir, int colDir)>
        {
            (0, 1), //to right
            (1, 1), // to bottom-right
            (1, 0), // to bottom
            (1, -1), // to bottom-left
            (0, -1), // to left
            (-1, -1), // to top-left
            (-1, 0), // to top
            (-1, 1), // to top-right
        }.ToArray();

        Console.WriteLine($"gridList.Count: {gridList.Count}");
        if (gridList.Count == 0) return;
        var matchList =
            new List<((int Line, int Column, char Character) X,
                (int Line, int Column, char Character) M,
                (int Line, int Column, char Character) A,
                (int Line, int Column, char Character) S)>();
        var xmasCount = 0;
        var starts = gridList.Where(i => i.Character == 'X');
        foreach (var start in starts)
        {
            var positions = directions.Select(d => (
                X: (Line: start.Line, Column: start.Column, Character: 'X'),
                M: (Line: start.Line + d.lineDir, Column: start.Column + d.colDir, Character: 'M'),
                A: (Line: start.Line + d.lineDir * 2, Column: start.Column + d.colDir * 2, Character: 'A'),
                S: (Line: start.Line + d.lineDir * 3, Column: start.Column + d.colDir * 3, Character: 'S'))).ToArray();

            var matches = positions
                .Where(p => gridList.Any(i =>
                    i.Line == p.X.Line && i.Column == p.X.Column && i.Character == p.X.Character))
                .Where(p => gridList.Any(i =>
                    i.Line == p.M.Line && i.Column == p.M.Column && i.Character == p.M.Character))
                .Where(p => gridList.Any(i =>
                    i.Line == p.A.Line && i.Column == p.A.Column && i.Character == p.A.Character))
                .Where(p => gridList.Any(i =>
                    i.Line == p.S.Line && i.Column == p.S.Column && i.Character == p.S.Character));
            matchList.AddRange(matches);
        }


        Console.WriteLine(
            $"chars: {gridList.Count}, lines: {gridList.Max(i => i.Line) + 1}, columnsMin: {gridList.Min(i => i.Column) + 1}, columnsMax: {gridList.Max(i => i.Column) + 1}");
        Console.WriteLine($"MatchCount: {matchList.Count}");
    }

    private static void DayFourPartTwo(bool testInput)
    {
        Console.WriteLine();
        var testString = testInput ? "test" : "";
        Console.WriteLine($"## Day 4 Part 1 {testString}");
        Console.WriteLine();

        var fileName = testInput ? "testinput.txt" : "input.txt";
        using var reader = File.OpenText(fileName);

        var gridList = new List<(int Line, int Column, char Character)>();
        var currLine = 0;
        while (reader.ReadLine() is { } line)
        {
            if (string.IsNullOrWhiteSpace(line)) continue;
            gridList.AddRange(line.Where(c => "XMAS".Contains(c))
                .Select((x, i) => (Line: currLine, Column: i, Character: x)));
            currLine++;
        }

        var masks = new List<(
            (int lineDir, int colDir) A,
            (int lineDir, int colDir) M,
            (int lineDir, int colDir) S,
            (int lineDir, int colDir) M2,
            (int lineDir, int colDir) S2
            )>
        {
            /*
             * M.S
             * .A.
             * M2.S2
             */
            ((0, 0),(-1, -1),(-1, 1),(1, -1),(1, 1)),
            /*
             * M2.M
             * .A.
             * S2.S
             */
            ((0, 0),(-1, 1),(1, 1),(-1, -1),(1, -1)),
            /*
             * S2.M2
             * .A.
             * S.M
             */
            ((0, 0),(1, 1),(1, -1),(-1, 1),(-1, -1)),
            /*
             * S.S2
             * .A.
             * M.M2
             */
            ((0, 0),(1, -1),(-1, -1),(1, 1),(-1, 1))
        }.ToArray();

        Console.WriteLine($"gridList.Count: {gridList.Count}");
        if (gridList.Count == 0) return;
        var matchList =
            new List<(
                (int Line, int Column, char Character) A,
                (int Line, int Column, char Character) M,
                (int Line, int Column, char Character) S,
                (int Line, int Column, char Character) M2,
                (int Line, int Column, char Character) S2
                )>();
        var xmasCount = 0;
        var starts = gridList.Where(i => i.Character == 'A');
        foreach (var start in starts)
        {
            // var positions = masks.Select(d => (
            //     X: (Line: start.Line, Column: start.Column, Character: 'X'),
            //     M: (Line: start.Line + d.lineDir, Column: start.Column + d.colDir, Character: 'M'),
            //     A: (Line: start.Line + d.lineDir * 2, Column: start.Column + d.colDir * 2, Character: 'A'),
            //     S: (Line: start.Line + d.lineDir * 3, Column: start.Column + d.colDir * 3, Character: 'S'))).ToArray();

            var shapes = masks.Select(m => (
                A: (Line: start.Line, Column: start.Column, Character: 'A'),
                M: (Line: start.Line + m.M.lineDir, Column: start.Column + m.M.colDir, Character: 'M'),
                S: (Line: start.Line + m.S.lineDir, Column: start.Column + m.S.colDir, Character: 'S'),
                M2: (Line: start.Line + m.M2.lineDir, Column: start.Column + m.M2.colDir, Character: 'M'),
                S2: (Line: start.Line + m.S2.lineDir, Column: start.Column + m.S2.colDir, Character: 'S')
                ));
            
            var matches = shapes
                .Where(p => gridList.Any(i =>
                    i.Line == p.A.Line && i.Column == p.A.Column && i.Character == p.A.Character))
                .Where(p => gridList.Any(i =>
                    i.Line == p.M.Line && i.Column == p.M.Column && i.Character == p.M.Character))
                .Where(p => gridList.Any(i =>
                    i.Line == p.S.Line && i.Column == p.S.Column && i.Character == p.S.Character))
                .Where(p => gridList.Any(i =>
                    i.Line == p.M2.Line && i.Column == p.M2.Column && i.Character == p.M2.Character))
                .Where(p => gridList.Any(i =>
                    i.Line == p.S2.Line && i.Column == p.S2.Column && i.Character == p.S2.Character));
            matchList.AddRange(matches);
        }


        var maxLine = gridList.Max(i => i.Line);
        var maxColumn = gridList.Max(i => i.Column);
        Console.WriteLine(
            $"chars: {gridList.Count}, lines: {maxLine + 1}, columnsMin: {gridList.Min(i => i.Column) + 1}, columnsMax: {gridList.Max(i => i.Column) + 1}");
        Console.WriteLine($"MatchCount: {matchList.Count}");

        if (testInput)
        {
            for (int iLine = 0; iLine < maxLine; iLine++)
            {
                for (int iCol = 0; iCol < maxColumn; iCol++)
                {
                    var matchedA = matchList.Where(m => m.A.Line == iLine && m.A.Column == iCol)
                        .Select(m => m.A.Character);
                    if (matchedA.Any())
                    {
                        Console.Write("A");
                    }
                    else
                    {
                        Console.Write(".");
                    }
                }

                Console.WriteLine();
            }
        }
    }
}