namespace Aoc2023.Models;

public record Position(int Row, int Start, int End);
public record Symbol(string Value, Position Position);
public record Number(int Value, Position Position);