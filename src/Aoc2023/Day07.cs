using Aoc2023.Helpers;

namespace Aoc2023;

public class Day07 : ISolver
{
    public string DayName => nameof(Day07);
    private string[] lines;
    private char[] _charStrengthOrder = ['2', '3', '4', '5', '6', '7', '8', '9', 'T', 'J', 'Q', 'K', 'A'];
    private char[] _wildCardCharStrengthOrder = ['J', '2', '3', '4', '5', '6', '7', '8', '9', 'T', 'Q', 'K', 'A'];
    private Dictionary<PokerHandType, int> _pokerHandsStrength = new()
    {
        { PokerHandType.HighCard, 1 },
        { PokerHandType.OnePair, 2 },
        { PokerHandType.TwoPair, 3 },
        { PokerHandType.ThreeOfAKind, 4 },
        { PokerHandType.FullHouse, 5 },
        { PokerHandType.FourOfAKind, 6 },
        { PokerHandType.FiveOfAKind, 7 }
    };

    public void Solve()
    {
        lines = ReadWriteHelpers.ReadTextFile(DayName);
        var pokerHands = new List<PokerHand>();

        foreach (var line in lines)
        {
            var parts = line.Trim().Split(' ');

            pokerHands.Add(new(parts[0], int.Parse(parts[1])));
        }

        foreach (var pokerHand in pokerHands)
        {
            pokerHand.SetType(GetPokerHandType(pokerHand));
            pokerHand.SetTypeUsingWildCard(GetPokerHandTypeWithUsingCard(pokerHand, _pokerHandsStrength));
        }

        // solution 1
        var groupedPokerHands = pokerHands.GroupBy(key => key.GetStrength(_pokerHandsStrength)).OrderBy(s => s.Key);
        SolveExercies(groupedPokerHands, 1, _charStrengthOrder);

        // solution 2
        var pokerHandsGroupedUsingWildCard = pokerHands.GroupBy(key => key.GetStrength(_pokerHandsStrength, true)).OrderBy(s => s.Key);
        SolveExercies(pokerHandsGroupedUsingWildCard, 2, _wildCardCharStrengthOrder);
    }

    private void SolveExercies(
        IOrderedEnumerable<IGrouping<int, PokerHand>> groupedPokerHands,
        int exerciseNumber,
        char[] charStrengthOrder)
    {
        var sortedList = new List<PokerHand>();

        foreach (var grouping in groupedPokerHands)
        {
            var pokerHandsInGroup = grouping.ToList();
            var orderInGroup = pokerHandsInGroup.OrderBy(str => str.StringRepresentation, new CharacterStringComparer(charStrengthOrder));

            sortedList.AddRange(orderInGroup);
        }

        ReadWriteHelpers.WriteResult(
            DayName,
            exerciseNumber.ToString(),
            sortedList.Select((item, index) =>
            {
                var sum = item.Bet * (index + 1);
                return sum;
            }).Sum());
    }

    public PokerHandType GetPokerHandType(PokerHand pokerHand)
    {
        var groups = pokerHand.Cards.GroupBy(key => key.Value).ToList();

        if (groups.Count == 1)
        {
            return PokerHandType.FiveOfAKind;
        }

        if (groups.Count == 2) // four of a kind | full house
        {
            if (groups.Any(g => g.Count() == 4))
            {
                return PokerHandType.FourOfAKind;
            }

            return PokerHandType.FullHouse;
        }

        if (groups.Count == 3) // three of a kind | two pair
        {
            if (groups.Any(g => g.Count() == 3))
            {
                return PokerHandType.ThreeOfAKind;
            }

            return PokerHandType.TwoPair;
        }

        if (groups.Count == 4) // one pair
        {
            return PokerHandType.OnePair;
        }

        return PokerHandType.HighCard;
    }

    public PokerHandType GetPokerHandTypeWithUsingCard(PokerHand pokerHand, Dictionary<PokerHandType, int> pokerHandsStrength)
    {
        if (!pokerHand.StringRepresentation.Contains('J'))
        {
            return GetPokerHandType(pokerHand);
        }

        var newPokerHandType = PokerHandType.HighCard;

        foreach (var testChar in _wildCardCharStrengthOrder.Where(c => c != 'J'))
        {
            var test = pokerHand.StringRepresentation.Replace('J', testChar);
            var newPokerHand = new PokerHand(test, pokerHand.Bet);
            var testType = GetPokerHandType(newPokerHand);
            var newStrength = _pokerHandsStrength[testType]

            if (newStrength > _pokerHandsStrength[newPokerHandType])
            {
                newPokerHandType = testType;
            }
        }

        return newPokerHandType;
    }

    public int GetRank(PokerHandType pokerHandType)
    {
        return pokerHandType switch
        {
            PokerHandType.FiveOfAKind => 7,
            PokerHandType.FourOfAKind => 6,
            PokerHandType.FullHouse => 5,
            PokerHandType.ThreeOfAKind => 4,
            PokerHandType.TwoPair => 3,
            PokerHandType.OnePair => 2,
            _ => 1
        };
    }

    public record PokerCard(char Value)
    {
        public int Strength
        {
            get
            {
                return Value switch
                {
                    'A' => 15,
                    'K' => 14,
                    'Q' => 13,
                    'J' => 12,
                    'T' => 10,
                    '9' => 9,
                    '8' => 8,
                    '7' => 7,
                    '6' => 6,
                    '5' => 5,
                    '4' => 4,
                    '3' => 3,
                    '2' => 2,
                    _ => 0
                };
            }
        }
    }

    public record PokerHand(string StringRepresentation, int Bet)
    {
        public PokerHandType Type { get; private set; }
        public PokerHandType TypeUsingWildcard { get; private set; }

        public void SetType(PokerHandType pokerHandType)
        {
            Type = pokerHandType;
        }
        public void SetTypeUsingWildCard(PokerHandType pokerHandTypeUsingWildCard)
        {
            TypeUsingWildcard = pokerHandTypeUsingWildCard;
        }

        public int GetStrength(Dictionary<PokerHandType, int> pokerHandsStrength, bool useWildCard = false) => useWildCard ? pokerHandsStrength[TypeUsingWildcard] : pokerHandsStrength[Type];

        public List<PokerCard> Cards => StringRepresentation.Select(c => c).Select(pk => new PokerCard(pk)).ToList();
    }

    public enum PokerHandType
    {
        FiveOfAKind,
        FourOfAKind,
        FullHouse,
        ThreeOfAKind,
        TwoPair,
        OnePair,
        HighCard
    }

    public class CharacterStringComparer : IComparer<string>
    {
        private readonly char[] _charStrengthOrder;

        public CharacterStringComparer(char[] charStrengthOrder)
        {
            _charStrengthOrder = charStrengthOrder;
        }

        public int Compare(string x, string y)
        {
            for (int i = 0; i < Math.Min(x.Length, y.Length); i++)
            {
                int indexX = Array.IndexOf(_charStrengthOrder, x[i]);
                int indexY = Array.IndexOf(_charStrengthOrder, y[i]);

                if (indexX < indexY) return -1;
                else if (indexX > indexY) return 1;
            }

            return x.Length.CompareTo(y.Length);
        }
    }
}