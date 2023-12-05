using System.Buffers;

namespace Aoc2023.Helpers;

public class NumericHelpers
{
    public static double GetNumberAsFirstAndLastDigitsFromString(string input)
    {
        var digits = input.Where(char.IsDigit).Select(char.GetNumericValue).ToArray();

        return digits.Length switch
        {
            1 => digits[0] * 10 + digits[0],
            > 1 => digits[0] * 10 + digits[^1],
            _ => 0
        };
    }

    private static Dictionary<string, int> _validDigits = new()
    {
        {"one",  1},
        {"two",  2},
        {"three",  3},
        {"four",  4},
        {"five",  5},
        {"six",  6},
        {"seven",  7},
        {"eight",  8},
        {"nine",  9},
        {"1",  1},
        {"2",  2},
        {"3",  3},
        {"4",  4},
        {"5",  5},
        {"6",  6},
        {"7",  7},
        {"8",  8},
        {"9",  9}
    };

    public static long GetNumberAsFirstAndLastDigitsAsStringsOrIntegersFromString(string input)
    {
        long result = 0;

        var firstIndex = input.Length;
        var lastIndex = -1;
        var firstValue = 0;
        var lastValue = 0;

        foreach (var digit in _validDigits)
        {
            var currentIndex = input.IndexOf(digit.Key);

            if (currentIndex == -1) { continue; }

            if (currentIndex < firstIndex)
            {
                firstIndex = currentIndex;
                firstValue = digit.Value;
            }

            currentIndex = input.LastIndexOf(digit.Key);

            if (currentIndex > lastIndex)
            {
                lastIndex = currentIndex;
                lastValue = digit.Value;
            }
        }

        result += firstValue * 10 + lastValue;

        return result;
    }
}
