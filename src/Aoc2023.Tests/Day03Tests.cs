using Aoc2023.Models;

namespace Aoc2023.Tests;

public class Day03Tests
{
    private readonly Day03 _sut;

    public Day03Tests()
    {
        _sut = new Day03();
    }

    public static IEnumerable<object[]> TestData
    {
        get
        {
            yield return new object[] { new Symbol("*", new Position(1, 3, 3)), new Number(123, new Position(1, 0, 2)), true }; // same row, symbol to the right
            yield return new object[] { new Symbol("*", new Position(1, 1, 1)), new Number(35, new Position(1, 2, 3)), true }; // same row, symbol to the left

            yield return new object[] { new Symbol("*", new Position(0, 1, 1)), new Number(35, new Position(1, 2, 3)), true }; // above row, symbol to the left
            yield return new object[] { new Symbol("*", new Position(0, 4, 4)), new Number(35, new Position(1, 2, 3)), true }; // above row, symbol to the right
            yield return new object[] { new Symbol("*", new Position(0, 2, 2)), new Number(35, new Position(1, 2, 3)), true }; // above row, symbol above

            yield return new object[] { new Symbol("*", new Position(2, 1, 1)), new Number(755, new Position(1, 2, 4)), true }; // below row, symbol to the left
            yield return new object[] { new Symbol("*", new Position(2, 5, 5)), new Number(755, new Position(1, 2, 4)), true }; // below row, symbol to the right
            yield return new object[] { new Symbol("*", new Position(2, 2, 2)), new Number(755, new Position(1, 2, 4)), true }; // below row, symbol below
        }
    }

    [Theory]
    [MemberData(nameof(TestData))]
    public void When_CheckIfSymbolIsAdjecentToNumber_ReturnCorrectResult(Symbol symbol, Number number, bool expected)
    {
        // Act
        var result = _sut.CheckIfSymbolIsAdjecentToNumber(symbol, number);

        // Assert
        Assert.Equal(expected, result);
    }
}
