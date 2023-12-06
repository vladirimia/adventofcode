using Aoc2023.Helpers;
using Aoc2023.Models;
using System.Text.RegularExpressions;

namespace Aoc2023;

public class Day06 : ISolver
{
    public string DayName => nameof(Day06);
    private string[] lines;
    private const string _numberRegex = @"(\d)+";
    private List<TimeAndDistance> _timesAndDistances = [];

    public void Solve()
    {
        lines = ReadWriteHelpers.ReadTextFile(DayName);
        var numberRegex = new Regex(_numberRegex);
        var allNumbers = new List<int>();

        foreach (var line in lines)
        {
            var numbers = numberRegex.Matches(line.Split(':')[1]);

            allNumbers.AddRange(numbers.Select(x => int.Parse(x.Value)));
        }

        var theTime = long.Parse(string.Join("", allNumbers.GetRange(0, allNumbers.Count / 2).Select(x => x.ToString())));
        var theDistance = long.Parse(string.Join("", allNumbers.GetRange(allNumbers.Count / 2, allNumbers.Count / 2)));

        for (var i = 0; i < allNumbers.Count / 2; i++)
        {
            _timesAndDistances.Add(new(
                allNumbers[i],
                allNumbers[i + allNumbers.Count / 2]));
        }

        // solution 1
        ReadWriteHelpers.WriteResult(
            DayName, 
            "1", 
            _timesAndDistances.Select(CalculateWaysToWin).Aggregate(1, (x, y) => x * y));

        // solution 2
        ReadWriteHelpers.WriteResult(
            DayName,
            "2",
            CalculateWaysToWin(new TimeAndDistance(theTime, theDistance)));
    }

    public int CalculateWaysToWin(TimeAndDistance timeAndDistance)
    {
        var noWaysToWin = 0;

        for (var holdFor = 1; holdFor <= timeAndDistance.Milisecondes; holdFor++)
        {
            var remainingMiliseconds = timeAndDistance.Milisecondes - holdFor;
            var distanceTravelled = remainingMiliseconds * holdFor;

            if (distanceTravelled > timeAndDistance.Milimeters)
            {
                noWaysToWin++;
            }
        }

        return noWaysToWin;
    }
}
