using Aoc2023.Helpers;

namespace Aoc2023;

public class Day07 : ISolver
{
    public string DayName => nameof(Day07);
    private string[] lines;

    public void Solve()
    {
        lines = ReadWriteHelpers.ReadTextFile(DayName);

        foreach (var line in lines)
        {
            var parts = line.Split(' ');
        }
    }
}

record PokerCard(string Value)
{
    public int Strength
    {
        get
        {
            return Value switch
            {
                "A" => 15,
                "K" => 14,
                "Q" => 13,
                "J" => 12,
                "T" => 10,
                "9" => 9,
                "8" => 8,
                "7" => 7,
                "6" => 6,
                "5" => 5,
                "4" => 4,
                "3" => 3,
                "2" => 2,
                _ => 0
            };
        }
    }
}

record PokerHand(List<PokerCard> Cards, int Bet);
