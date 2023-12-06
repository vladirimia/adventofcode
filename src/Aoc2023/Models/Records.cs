namespace Aoc2023.Models;

public record Position(int Row, int Start, int End);

public record Symbol(string Value, Position Position);

public record Number(int Value, Position Position);

public record CardAndNumbers(int CardId, List<int> WinningNumbers, List<int> MyNumbers)
{
    public List<int> MatchingNumbers
    {
        get
        {
            return WinningNumbers.Intersect(MyNumbers).ToList();
        }
    }

    public double MatchValue 
    {
        get 
        {
            if (MatchingNumbers.Count == 0)
            {
                return 0;
            }

            return Math.Pow(2, MatchingNumbers.Count - 1);
        } 
    }
}

public record TimeAndDistance(long Milisecondes, long Milimeters);