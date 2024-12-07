using Aoc.Common;
using Aoc.Common.Helpers;
using Aoc2023.Helpers;

namespace Aoc2023;

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
        var tempSum1 = 0d;
        long tempSum2 = 0;

        foreach (var line in lines)
        {
            tempSum1 += NumericHelpers.GetNumberAsFirstAndLastDigitsFromString(line);
            tempSum2 += NumericHelpers.GetNumberAsFirstAndLastDigitsAsStringsOrIntegersFromString(line);
        }

        ReadWriteHelpers.WriteResult(DayName, "1", tempSum1);
        ReadWriteHelpers.WriteResult(DayName, "2", tempSum2);
    }
}
