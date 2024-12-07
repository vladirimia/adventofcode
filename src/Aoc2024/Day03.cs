using Aoc.Common;
using Aoc.Common.Helpers;
using System.Text.RegularExpressions;

namespace Aoc2024;

public class Day03 : ISolver
{
    public string DayName => nameof(Day03);
    private readonly string[] lines;
    const string pattern = @"mul\((\d{1,3}),(\d{1,3})\)";
    const string _do = "do()";
    const string _dont = "don't()";

    public Day03()
    {
        lines = ReadWriteHelpers.ReadTextFile(DayName);
    }

    public void Solve()
    {
        ReadWriteHelpers.WriteResult(DayName, "1", SolvePart1());
        ReadWriteHelpers.WriteResult(DayName, "2", SolvePart2());
    }

    private int SolvePart1()
    {
        var result = 0;

        foreach (var line in lines)
        {
            MatchCollection matches = Regex.Matches(line, pattern);

            foreach (Match match in matches)
            {
                string num1 = match.Groups[1].Value;
                string num2 = match.Groups[2].Value;

                result += Convert.ToInt32(num1) * Convert.ToInt32(num2);
            }
        }

        return result;
    }

    private int SolvePart2()
    {
        var fullText = ReadWriteHelpers.ReadFullTextFile(DayName);
        var allMatches = Regex.Matches(fullText, $@"mul\((\d{{1,3}}),(\d{{1,3}})\)|do\(\)|don't\(\)");
        var result = 0;
        var performMull = true;

        foreach (Match match in allMatches)
        {
            switch (match.Value)
            {
                case _do:
                    performMull = true;
                    break;
                case _dont:
                    performMull = false;
                    break;
                default:
                    if (performMull)
                    {
                        string num1 = match.Groups[1].Value;
                        string num2 = match.Groups[2].Value;

                        result += Convert.ToInt32(num1) * Convert.ToInt32(num2);
                    }
                    break;
            }
        }

        return result;
    }
}
