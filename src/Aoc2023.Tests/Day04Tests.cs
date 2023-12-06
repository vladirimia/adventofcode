﻿using Aoc2023.Models;

namespace Aoc2023.Tests;

public class Day04Tests
{
    private Day04 _sut = new();

    public static IEnumerable<object[]> TestData
    {
        get
        {
            yield return new object[] { new CardAndNumbers(1, [41, 48, 83, 86, 17], [83, 86, 6, 31, 17, 9, 48, 53]), 8 };
            yield return new object[] { new CardAndNumbers(2, [13, 32, 20, 16, 61], [61, 30, 68, 82, 17, 32, 24, 19]), 2 };
            yield return new object[] { new CardAndNumbers(3, [1, 21, 53, 59, 44], [69, 82, 63, 72, 16, 21, 14, 1]), 2 };
            yield return new object[] { new CardAndNumbers(4, [41, 92, 73, 84, 69], [59, 84, 76, 51, 58, 5, 54, 83]), 1 };
            yield return new object[] { new CardAndNumbers(5, [87, 83, 26, 28, 32], [88, 30, 70, 12, 93, 22, 82, 36]), 0 };
            yield return new object[] { new CardAndNumbers(6, [31, 18, 13, 56, 72], [74, 77, 10, 23, 35, 67, 36, 11]), 0 };
        }
    }

    [Theory]
    [MemberData(nameof(TestData))]
    public void When_CardAndnumbersIsCreated_CalculateCorrectMatchValue(CardAndNumbers card, double expected)
    {
        // Assert
        Assert.Equal(expected, card.MatchValue);
    }
}
