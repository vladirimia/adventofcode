using Aoc2023.Helpers;

namespace Aoc2023;

public class Day08 : ISolver
{
    public string DayName => nameof(Day08);
    private string[] _lines;
    private List<char> _directions = [];

    public void Solve()
    {
        _lines = ReadWriteHelpers.ReadTextFile(DayName)
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .Select(x => x.Replace(" ", "").Replace("(", "").Replace(")", ""))
            .ToArray();
        _directions = _lines[0].Trim().Select(c => c).ToList();

        var nodes = _lines.Skip(1).Select(line =>
        {
            var parts = line.Split('=');
            var lrNodes = parts[1].Split(',');

            return new Node(parts[0], lrNodes[0], lrNodes[1]);
        }).ToDictionary(node => node.Data);

        // solution 1
        ReadWriteHelpers.WriteResult(
            DayName,
            "1",
            GetPath(
                "AAA",
                n => n.Data == "ZZZ",
                nodes,
                _directions).Count);

        // solution 2
        var paths = nodes
            .Where(k => k.Key.EndsWith('A'))
            .Select(node => GetPath(node.Key, n => n.Data.EndsWith('Z'), nodes, _directions))
            .Select(path => (long)path.Count);

        ReadWriteHelpers.WriteResult(DayName,"2", FindLeastCommonMultiple(paths));
    }

    public List<string> GetPath(
        string startingNodeValue,
        Func<Node, bool> endingCondition,
        Dictionary<string, Node> nodes,
        List<char> directions)
    {
        var path = new List<string>();
        var currentNode = nodes[startingNodeValue];
        var lrCnt = 0;

        while (!endingCondition(currentNode))
        {
            path.Add(currentNode.Data);
            currentNode = nodes[directions[lrCnt % directions.Count] == 'L' ? currentNode.Left : currentNode.Right];
            lrCnt++;
        }

        return path;
    }

    private long FindLeastCommonMultiple(IEnumerable<long> numbers) =>
        numbers.Aggregate((long)1, (current, number) => current / GreatestCommonDivisor(current, number) * number);

    private long GreatestCommonDivisor(long a, long b)
    {
        while (b != 0)
        {
            a %= b;
            (a, b) = (b, a);
        }
        return a;
    }

    public record Node(string Data, string Left, string Right);
}
