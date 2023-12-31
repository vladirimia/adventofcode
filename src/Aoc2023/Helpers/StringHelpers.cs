﻿using Aoc2023.Models;

namespace Aoc2023.Helpers;

internal static class StringHelpers
{
    public static IEnumerable<string> SplitInChunks(this string inputString, int chunkSize)
    {
        return Enumerable
            .Range(0, inputString.Length / chunkSize)
            .Select(x => inputString.Substring(x * chunkSize, chunkSize));
    }

    public static bool CheckIfCharactersAreDifferent(this string input)
    {
        return input.Distinct().Count() == input.Length;
    }

    public static Color StringToColor(string input) => input.ToLower() switch
    {
        "red" => Color.Red,
        "green" => Color.Green,
        _ => Color.Blue
    };
}
