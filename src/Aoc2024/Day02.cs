using Aoc.Common;
using Aoc.Common.Helpers;

namespace Aoc2024;

public class Day02 : ISolver
{
    public string DayName => nameof(Day02);
    private readonly string[] lines;

    public Day02()
    {
        lines = ReadWriteHelpers.ReadTextFile(DayName);
    }

    public void Solve()
    {
        ReadWriteHelpers.WriteResult(DayName, "1", SolveLevel1());
        ReadWriteHelpers.WriteResult(DayName, "2", SolveLevel2());
    }

    public int SolveLevel1()
    {
        var numberOfSafeLevls = 0;

        foreach (var line in lines)
        {
            var levels = line.Split(' ')
                .Select(x => Convert.ToInt32(x))
                .ToList();

            if (IsLevelValid(levels))
            {
                numberOfSafeLevls++;
            }
            else
            {
                var xx = levels.Select(x => x.ToString());
            }
        }

        return numberOfSafeLevls;
    }

    public int SolveLevel2()
    {
        Console.Clear();
        Console.WriteLine("--------------------------------------------------------");

        var numberOfSafeLevels = 0;

        foreach (var line in lines)
        {
            var numbers = line.Split(' ')
                .Select(x => Convert.ToInt32(x))
                .ToList();

            if (IsLevelValid(numbers))
            {
                numberOfSafeLevels++;
            }
            else
            {
                for (var i = 0; i < numbers.Count; i++)
                {
                    var allExceptCurrent = numbers.Where((value, index) => index != i)
                        .ToList();

                    if (IsLevelValid(allExceptCurrent))
                    {
                        numberOfSafeLevels++;
                        break;
                    }
                }
            }
        }

        return numberOfSafeLevels;
    }

    public static bool IsLevelValid(List<int> levelItems)
    {
        for (var i = 0; i <= levelItems.Count - 3; i++)
        {
            var firstDifference = levelItems[i] - levelItems[i + 1];
            var secondDifference = levelItems[i + 1] - levelItems[i + 2];

            if (firstDifference == 0 || secondDifference == 0)
            {
                return false;
            }

            if (Math.Abs(firstDifference) > 3 || Math.Abs(secondDifference) > 3)
            {
                return false;
            }

            if (firstDifference < 0 && secondDifference > 0
                || firstDifference > 0 && secondDifference < 0)
            {
                return false;
            }
        }

        return true;
    }
}
