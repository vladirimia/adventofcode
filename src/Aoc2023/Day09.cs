using Aoc.Common;
using Aoc.Common.Helpers;
using Aoc2023.Helpers;
using System.Text.RegularExpressions;

namespace Aoc2023;

public class Day09 : ISolver
{
    public string DayName => nameof(Day09);
    private string[] _lines;
    private const string _numberRegex = @"-?(\d)+";

    public void Solve()
    {
        _lines = ReadWriteHelpers.ReadTextFile(DayName);
        var numberRegex = new Regex(_numberRegex);
        var lists = new List<List<int>>();

        foreach (var line in _lines)
        {
            var numberMatches = numberRegex.Matches(line);
            var newList = numberMatches.Select(number => int.Parse(number.Value)).ToList();
            lists.Add(newList);
        }

        // solution 1
        //ReadWriteHelpers.WriteResult(DayName, "1", lists.Select(x => GetPrediction(x, Prediction.LastNumber)).ToList().Sum());

        // solution 2
        ReadWriteHelpers.WriteResult(DayName, "2", lists.Select(x => GetPrediction(x, Prediction.FirstNumber)).ToList().Sum());
    }

    public long GetPrediction(List<int> list, Prediction prediction)
    {
        // constructing the list of differences
        var result = new List<List<int>>
        {
            list
        };

        var differences = list.Zip(list.Skip(1), (current, next) => next - current).ToList();

        while (differences.Any(d => d != 0))
        {
            result.Add([.. differences]);

            differences = differences.Zip(differences.Skip(1), (current, next) => next - current).ToList();
        }

        result.Add(Enumerable.Repeat(0, result.Last().Count - 1).ToList());

        // hidrating the list with the last element
        if(prediction == Prediction.LastNumber)
        {
            for (var i = result.Count - 1; i >= 0; i--)
            {
                if (i == result.Count - 1)
                {
                    result[i].Add(result[i].Last());
                }
                else
                {
                    result[i].Add(result[i].Last() + result[i + 1].Last());
                }
            }

            return result[0].Last();
        }
        else
        {
            for (var i = result.Count - 1; i >= 0; i--)
            {
                if (i == result.Count - 1)
                {
                    result[i].Insert(0, result[i].Last());
                }
                else
                {
                    result[i].Insert(0, result[i].First() - result[i + 1].First());
                }
            }

            return result[0].First();
        }
    }

    public enum Prediction
    {
        LastNumber,
        FirstNumber
    }
}
