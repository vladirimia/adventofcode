namespace Aoc.Common.Helpers;

public static class ReadWriteHelpers
{
    public static string[] ReadTextFile(string day)
    {
        var path = $"{Directory.GetCurrentDirectory()}/inputs/{day}.txt";

        return File.ReadAllLines(path);
    }

    public static string ReadFullTextFile(string day)
    {
        var path = $"{Directory.GetCurrentDirectory()}/inputs/{day}.txt";

        return File.ReadAllText(path);
    }

    public static void WriteResult(string day, string exercise, object result)
    {
        Console.WriteLine($"Result for {day} -> exercise {exercise} is: {result}");
    }
}