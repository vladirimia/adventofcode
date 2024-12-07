using Aoc.Common;
using Aoc.Common.Helpers;
using Aoc2023.Helpers;
using Aoc2023.Models;
using System.Text.RegularExpressions;

namespace Aoc2023;

public class Day02 : ISolver
{
    public string DayName => nameof(Day02);
    private string[] lines;
    private string _regex = @"\d+\s*(red|blue|green)\b";

    private const int _noRedCubes = 12;
    private const int _noGreenCubes = 13;
    private const int _noBlueCubes = 14;

    public Day02()
    {
    }

    public void Solve()
    {
        lines = ReadWriteHelpers.ReadTextFile(DayName);

        var games = new Dictionary<int, List<ColorAndNumber>>();

        foreach (var line in lines)
        {
            var mainParts = line.Split(':');
            var gameId = int.Parse(mainParts[0].Split(' ')[1]);
            var matches = Regex.Matches(mainParts[1], _regex);
            var listOfColorsAndNumbers = new List<ColorAndNumber>();

            if (matches.Count > 0)
            {
                foreach (var match in matches)
                {
                    var theParts = match.ToString().Split(' ');

                    listOfColorsAndNumbers.Add(new(
                        StringHelpers.StringToColor(theParts[1]),
                        int.Parse(theParts[0])));
                }

                games.Add(gameId, listOfColorsAndNumbers);
            }
        }

        // solution 1
        ReadWriteHelpers.WriteResult(DayName, "1", SolveExercise1(games));

        // solution 2
        ReadWriteHelpers.WriteResult(DayName, "2", SolveExercise2(games));
    }

    public long SolveExercise1(Dictionary<int, List<ColorAndNumber>> games)
    {
        long result = 0;

        foreach (var game in games)
        {
            if (game.Value.Any(x => x.Color == Color.Red && x.Number > _noRedCubes)
                || game.Value.Any(x => x.Color == Color.Blue && x.Number > _noBlueCubes)
                || game.Value.Any(x => x.Color == Color.Green && x.Number > _noGreenCubes))
            {
                continue;
            }

            result += game.Key;
        }

        return result;
    }

    public long SolveExercise2(Dictionary<int, List<ColorAndNumber>> games)
    {
        long result = 0;

        foreach (var game in games)
        {
            var maxRed = game.Value.Where(x => x.Color == Color.Red).Select(y => y.Number).Max();
            var maxGreen = game.Value.Where(x => x.Color == Color.Green).Select(y => y.Number).Max();
            var maxBlue = game.Value.Where(x => x.Color == Color.Blue).Select(y => y.Number).Max();

            result += maxRed * maxBlue * maxGreen;
        }

        return result;
    }
}
