using Aoc2023.Helpers;
using Aoc2023.Models;
using System.Text.RegularExpressions;

namespace Aoc2023;

public class Day03 : ISolver
{
    public string DayName => nameof(Day03);
    private string[] lines;
    private const string _numberRegex = @"(\d)+";
    private const string _symbolRegex = @"[^\.\d\n]";

    public void Solve()
    {
        lines = ReadWriteHelpers.ReadTextFile(DayName);
        var numberRegex = new Regex(_numberRegex);
        var symbolRegex = new Regex(_symbolRegex);
        var numbers = new List<Number>();
        var symbols = new List<Symbol>();
        var currentRow = 0;

        foreach (var line in lines)
        {
            var numberMatches = numberRegex.Matches(line);
            var symbolMatches = symbolRegex.Matches(line);

            foreach (Match numberMatch in numberMatches)
            {
                numbers.Add(new(
                    int.Parse(numberMatch.Value),
                    new(
                        currentRow,
                        numberMatch.Index,
                        numberMatch.Index + numberMatch.Value.Length - 1)));
            }

            foreach (Match symbolMatch in symbolMatches)
            {
                symbols.Add(new(
                    symbolMatch.Value,
                    new(
                        currentRow,
                        symbolMatch.Index,
                        symbolMatch.Index)));
            }

            currentRow++;
        }

        // solution 1
        var result1 = numbers
            .Where(x => symbols.Any(s => CheckIfSymbolIsAdjecentToNumber(s, x)))
            .Sum(n => n.Value);

        var result2 = symbols
            .Where(s => s.Value == "*")
            .Sum(currentSymbol =>
            {
                var numberAdjacentToSymbol = numbers.Where(n => CheckIfSymbolIsAdjecentToNumber(currentSymbol, n)).ToList();

                if (numberAdjacentToSymbol.Count != 2)
                    return 0;

                return numberAdjacentToSymbol[0].Value * numberAdjacentToSymbol[1].Value;
            });

        ReadWriteHelpers.WriteResult(DayName, "1", result1);
        ReadWriteHelpers.WriteResult(DayName, "2", result2);
    }

    public bool CheckIfSymbolIsAdjecentToNumber(Symbol symbol, Number number)
    {
        return true switch
        {
            true when symbol.Position.Row == number.Position.Row - 1
                && (symbol.Position.Start >= number.Position.Start - 1 && symbol.Position.End <= number.Position.End + 1) => true, // symbol is above
            true when symbol.Position.Row == number.Position.Row + 1
                && (symbol.Position.Start >= number.Position.Start - 1 && symbol.Position.End <= number.Position.End + 1) => true, // symbol is below
            true when symbol.Position.Row == number.Position.Row
                && (symbol.Position.End + 1 == number.Position.Start || symbol.Position.Start == number.Position.End + 1) => true, // symbol and number are on the same row
            _ => false
        };
    }
}
