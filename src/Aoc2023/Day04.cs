using Aoc.Common;
using Aoc.Common.Helpers;
using Aoc2023.Helpers;
using Aoc2023.Models;
using System.Text.RegularExpressions;

namespace Aoc2023;

public class Day04 : ISolver
{
    public string DayName => nameof(Day04);
    private string[] lines;
    private const string _numberRegex = @"(\d)+";
    private List<CardAndNumbers> _cards = [];

    public void Solve()
    {
        lines = ReadWriteHelpers.ReadTextFile(DayName);
        var numberRegex = new Regex(_numberRegex);

        foreach (var line in lines)
        {
            var mainParts = line.Split(':');
            var firstPartSplit = mainParts[0].Split(' ');
            var cardId = int.Parse(firstPartSplit[firstPartSplit.Length - 1]);
            var numbersRaw = mainParts[1].Split('|');

            var winningNumberMatches = numberRegex.Matches(numbersRaw[0]);
            var myNumberMatches = numberRegex.Matches(numbersRaw[1]);

            var winningNumbers = winningNumberMatches.Select(x => int.Parse(x.Value)).ToList();
            var myNumbers = myNumberMatches.Select(x => int.Parse(x.Value)).ToList();

            _cards.Add(new(cardId, winningNumbers, myNumbers));
        }

        // solution 1
        ReadWriteHelpers.WriteResult(DayName, "1", _cards.Sum(c => c.MatchValue));

        // solution 2
        int[] cardCount = Enumerable.Repeat(1, _cards.Count).ToArray();

        for (int cardId = 0; cardId < _cards.Count; cardId++)
        {
            var currentCard = _cards.Single(x => x.CardId == cardId + 1);

            for (var i = 0; i < currentCard.MatchingNumbers.Count; i++)
            {
                cardCount[cardId + 1 + i] += cardCount[cardId];
            }
        }

        ReadWriteHelpers.WriteResult(DayName, "2", cardCount.Sum());
    }
}
