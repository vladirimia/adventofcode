using Aoc2023.Helpers;

namespace Aoc2023;

public class Day05 : ISolver
{
    public string DayName => nameof(Day06);
    private string[] lines;

    public void Solve()
    {
        lines = ReadWriteHelpers.ReadTextFile(DayName);
    }
}
