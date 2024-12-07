using Aoc.Common;
using Aoc.Common.Helpers;
using Aoc2023.Helpers;

namespace Aoc2023;

public class Day14 : ISolver
{
    public string DayName => nameof(Day14);
    private string[] _lines;
    const long _noCycles = 1000000000;

    public void Solve()
    {
        _lines = ReadWriteHelpers.ReadTextFile(DayName);
        var noCharactersPerLine = _lines[0].Length;
        var initialListOfMyArrays = new List<MyArray>();

        // initliaize all the charcters
        for (var i = 0; i < _lines.Length; i++)
        {
            initialListOfMyArrays.Add(new() { Index = i, List = [.. _lines[i]] });
        }

        //// solution 1
        //var tiltNorth = PerformTilt(Direction.North, initialListOfMyArrays, noCharactersPerLine);
        //var solution1 = 0;

        //foreach (var myArray in tiltNorth)
        //{
        //    //ReArrange(myArray.List);
        //    var sum = myArray.List.Select((x, index) =>
        //    {
        //        if (x == 'O')
        //        {
        //            var rockLoad = _lines.Length - index;

        //            return rockLoad;
        //        }

        //        return 0;
        //    }).Sum();

        //    solution1 += sum;
        //}

        //ReadWriteHelpers.WriteResult(DayName, "1", solution1);

        // solution 2
        var initialTiltNorth = PerformTilt(Direction.North, initialListOfMyArrays, noCharactersPerLine);
        var index = 0;
        var allTilts = new List<MyArray>();

        while (index < _noCycles)
        {
            if (index == 0)
            {
                var initialTiltWest = PerformTilt(Direction.West, initialTiltNorth, noCharactersPerLine);
                var initialTiltSouth = PerformTilt(Direction.South, initialTiltWest, noCharactersPerLine);
                var initialTiltEast = PerformTilt(Direction.East, initialTiltSouth, noCharactersPerLine);
                allTilts.AddRange(initialTiltEast);
            }
            else
            {
                allTilts = PerformTilt(Direction.North, allTilts, noCharactersPerLine);
                allTilts = PerformTilt(Direction.West, allTilts, noCharactersPerLine);
                allTilts = PerformTilt(Direction.South, allTilts, noCharactersPerLine);
                allTilts = PerformTilt(Direction.East, allTilts, noCharactersPerLine);
            }

            index++;
        }

        var solution2 = 0;

        foreach (var myArray in allTilts)
        {
            //ReArrange(myArray.List);
            var sum = myArray.List.Select((x, index) =>
            {
                if (x == 'O')
                {
                    var rockLoad = _lines.Length - index;

                    return rockLoad;
                }

                return 0;
            }).Sum();

            solution2 += sum;
        }

        ReadWriteHelpers.WriteResult(DayName, "2", solution2);
    }

    List<MyArray> PerformTilt(Direction direction, List<MyArray> input, int noCharactersPerLine)
    {
        var arrays = new List<MyArray>();

        for (var i = 0; i < noCharactersPerLine; i++)
        {
            arrays.Add(new MyArray { Index = i, List = [] });
        }

        if (direction == Direction.North)
        {
            for (var i = 0; i < input.Count; i++)
            {
                for (var j = 0; j < noCharactersPerLine; j++)
                {
                    arrays.Single(x => x.Index == j).List.Add(input.Find(x => x.Index == i).List[j]);
                }
            }

            foreach (var array in arrays)
            {
                ReArrange(array.List, direction);
            }
        }

        if (direction == Direction.West)
        {

        }

        if (direction == Direction.South)
        {

        }

        if (direction == Direction.East)
        {

        }

        return arrays;
    }

    void ReArrange(List<char> characters, Direction direction)
    {
        //Console.WriteLine("Existing line: {0}", string.Join("", characters));

        if (direction == Direction.North)
        {
            for (var i = 0; i < characters.Count; i++)
            {
                if (characters[i] == '#' || characters[i] == '.') continue;
                if (characters[i] == 'O' && i == 0) continue;
                if (characters[i] == 'O' && characters[i - 1] == 'O') continue;

                // current character is 'O' and previous is '.'
                var index = i;

                while (index > 0 && characters[index - 1] == '.')
                {
                    characters[index] = '.';
                    characters[index - 1] = 'O';
                    index--;
                }
            }
        }

        if (direction == Direction.West)
        {
            for (var i = characters.Count - 1; i >= 0; i--)
            {
                if (characters[i] == '#' || characters[i] == '.') continue;
                if (characters[i] == 'O' && i == characters.Count - 1) continue;
                if (characters[i] == 'O' && characters[i + 1] == 'O') continue;

                // current character is 'O' and previous is '.'
                var index = i;

                while (index < characters.Count - 1 && characters[index + 1] == '.')
                {
                    characters[index] = '.';
                    characters[index + 1] = 'O';
                    index++;
                }
            }
        }

        if (direction == Direction.South)
        {
            for (var i = 0; i < characters.Count; i++)
            {
                if (characters[i] == '#' || characters[i] == '.') continue;
                if (characters[i] == 'O' && i == 0) continue;
                if (characters[i] == 'O' && characters[i - 1] == 'O') continue;

                // current character is 'O' and previous is '.'
                var index = i;

                while (index > 0 && characters[index - 1] == '.')
                {
                    characters[index] = '.';
                    characters[index - 1] = 'O';
                    index--;
                }
            }
        }

        if (direction == Direction.East)
        {

        }

        //Console.WriteLine("Modified line line: {0}", string.Join("", characters));
        //Console.WriteLine();
    }

    class MyArray
    {
        public int Index { get; set; }
        public List<char> List { get; set; }
    }

    enum Direction
    {
        North, South, East, West
    }
}
