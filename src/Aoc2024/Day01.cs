using Aoc.Common;
using Aoc.Common.Helpers;

namespace Aoc2024;

public class Day01 : ISolver
{
    public string DayName => nameof(Day01);
    private readonly string[] lines;

    public Day01()
    {
        lines = ReadWriteHelpers.ReadTextFile(DayName);
    }

    public void Solve()
    {
        var leftNumbers = new List<int>(lines.Length);
        var rightNumbers = new List<int>(lines.Length);

        foreach (var line in lines)
        {
            var items = line.Split([' '], StringSplitOptions.RemoveEmptyEntries);

            leftNumbers.Add(Convert.ToInt32(items[0]));
            rightNumbers.Add(Convert.ToInt32(items[1]));
        }

        leftNumbers.Sort();
        rightNumbers.Sort();

        ReadWriteHelpers.WriteResult(DayName, "1", GetSolutionForPart1(leftNumbers, rightNumbers));
        ReadWriteHelpers.WriteResult(DayName, "2", GetSolutionForPart2(leftNumbers, rightNumbers));
    }

    private static int GetSolutionForPart1(List<int> leftNumbers, List<int> rightNumbers)
    {
        var sum = 0;

        for (var i = 0; i < leftNumbers.Count; i++)
        {
            sum += Math.Abs(leftNumbers[i] - rightNumbers[i]);
        }

        return sum;
    }

    private static int GetSolutionForPart2(List<int> leftNumbers, List<int> rightNumbers)
    {
        Dictionary<int, int> occurrences = [];

        foreach (var rightNumber in rightNumbers)
        {
            occurrences[rightNumber] = occurrences.TryGetValue(rightNumber, out int value) ? value + 1 : 1;
        }

        var sum = 0;

        foreach (var leftnumber in leftNumbers)
        {
            occurrences.TryGetValue(leftnumber, out int x);

            sum += leftnumber * x;
        }

        return sum;
    }
}
