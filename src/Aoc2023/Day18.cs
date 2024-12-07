using Aoc.Common;
using Aoc.Common.Helpers;
using Aoc2023.Helpers;
using System.Text.RegularExpressions;

namespace Aoc2023;

public class Day18 : ISolver
{
    public string DayName => nameof(Day18);
    private string[] _lines;
    private readonly IReadOnlyDictionary<char, Point> _directions = new Dictionary<char, Point>
    {
        { 'U', new Point(-1, 0) },
        { 'D', new Point(1, 0) },
        { 'L', new Point(0, -1) },
        { 'R', new Point(0, 1) },
    };
    private const string _hexRegex = @"(?<=#)(.{6})";

    public void Solve()
    {
        _lines = ReadWriteHelpers.ReadTextFile(DayName);

        SolvePart1();
        SolvePart2();
    }

    void SolvePart1()
    {
        var points = new List<Point> { new(0, 0) };
        var boundryPoints = 0;

        foreach (var line in _lines)
        {
            var parts = line.Split(' ');
            var noOfSteps = int.Parse(parts[1]);
            boundryPoints += noOfSteps;
            var direction = _directions[char.Parse(parts[0])];
            var lastPoint = points.Last();

            var newPointX = lastPoint.X + direction.X * noOfSteps;
            var newPointY = lastPoint.Y + direction.Y * noOfSteps;

            //Console.WriteLine($"({newPointX}, {newPointY})", newPointX, newPointY);

            points.Add(new(newPointX, newPointY));
        }

        // shoelace formula
        var areaUsingShoeLaceFormula = PolygonArea(points);
        var i = areaUsingShoeLaceFormula - boundryPoints / 2 + 1;

        ReadWriteHelpers.WriteResult(DayName, "1", i + boundryPoints);
    }

    void SolvePart2()
    {
        var points = new List<Point> { new(0, 0) };
        long boundryPoints = 0;
        var hexRegex = new Regex(_hexRegex);

        foreach (var line in _lines)
        {
            var parts = line.Split(' ');
            var hexMatch = hexRegex.Matches(parts[2]).Single().Value;
            var stepsHex = hexMatch.Substring(0, 5);
            long noOfSteps = ToHexInt(stepsHex);

            boundryPoints += noOfSteps;

            var directionFromLastCharacter = hexMatch.Last() switch
            {
                '0' => 'R',
                '1' => 'D',
                '2' => 'L',
                '3' => 'U',
                _ => throw new NotSupportedException()
            };
            var direction = _directions[directionFromLastCharacter];

            Console.WriteLine($"{directionFromLastCharacter} {noOfSteps}");

            var lastPoint = points.Last();

            var newPointX = lastPoint.X + direction.X * noOfSteps;
            var newPointY = lastPoint.Y + direction.Y * noOfSteps;

            //Console.WriteLine($"({newPointX}, {newPointY})", newPointX, newPointY);

            points.Add(new(newPointX, newPointY));
        }

        // shoelace formula
        var areaUsingShoeLaceFormula = PolygonArea(points);
        var i = areaUsingShoeLaceFormula - boundryPoints / 2 + 1;

        ReadWriteHelpers.WriteResult(DayName, "1", i + boundryPoints);
    }

    static double PolygonArea(List<Point> points)
    {
        // Initialize area
        double area = 0.0;

        // Calculate value of shoelace formula
        int j = points.Count - 1;

        for (int i = 0; i < points.Count; i++)
        {
            area += (points[j].X + points[i].X) * (points[j].Y - points[i].Y);

            // j is previous vertex to i
            j = i;
        }

        // Return absolute value
        return Math.Abs(area / 2.0);
    }

    record Point(long X, long Y);

    int ToHexInt(string str)
    {
        bool negative = false;
        var num = 0;
        var index = 0;
        if (str[index] == '-')
        {
            negative = true;
            index++;
        }
        while (index < str.Length)
        {
            var chr = str[index];
            if (char.IsAsciiDigit(chr))
            {
                num *= 16;
                num += chr - '0';
            }
            else if (char.IsBetween(chr, 'a', 'f'))
            {
                num *= 16;
                num += chr - 'a' + 10;
            }
            else
            {
                break;
            }

            index++;
        }
        return negative ? -num : num;
    }
}
