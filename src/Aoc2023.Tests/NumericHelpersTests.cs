using Aoc2023.Helpers;

namespace Aoc2023.Tests;

public class NumericHelpersTests
{
    [Theory]
    [InlineData("1abc2", 12)]
    [InlineData("pqr3stu8vwx", 38)]
    [InlineData("a1b2c3d4e5f", 15)]
    [InlineData("treb7uchet", 77)]
    [InlineData("abcde", 0)]
    [InlineData("zmbqqcd8ztp46567", 87)]
    public void When_GetNumberAsFirstAndLastDigitsFromString_GetCorrectInteger(string input, int expected)
    {
        // Act
        var result = NumericHelpers.GetNumberAsFirstAndLastDigitsFromString(input);

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("1abc2", 12)]
    [InlineData("pqr3stu8vwx", 38)]
    [InlineData("a1b2c3d4e5f", 15)]
    [InlineData("treb7uchet", 77)]
    [InlineData("abcde", 0)]
    [InlineData("zmbqqcd8ztp46567", 87)]
    [InlineData("two1nine", 29)]
    [InlineData("eightwothree", 83)]
    [InlineData("abcone2threexyz", 13)]
    [InlineData("xtwone3four", 24)]
    [InlineData("4nineeightseven2", 42)]
    [InlineData("zoneight234", 14)]
    [InlineData("7pqrstsixteen", 76)]
    public void When_GetNumberAsFirstAndLastDigitsAsStringsOrIntegersFromString_GetCorrectInteger(string input, int expected)
    {
        // Act
        var result = NumericHelpers.GetNumberAsFirstAndLastDigitsAsStringsOrIntegersFromString(input);

        // Assert
        Assert.Equal(expected, result);
    }
}
