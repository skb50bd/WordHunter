namespace WordHunter.WordList;

public static class Words
{
    public static IDictionary<int, IList<string>> WordsByLength { get; }
    public static IList<string> AllWords { get; }

    static Words()
    {
        AllWords =
            File.ReadAllLines("words.txt")
                .Where(w => w.All(ch => char.IsLetter(ch)))
                .Select(w => w.Trim().ToLowerInvariant())
                .ToList();

        WordsByLength =
            AllWords
                .GroupBy(w => w.Length)
                .OrderBy(g => g.Key)
                .ToDictionary(
                    g => g.Key,
                    g => g.ToList() as IList<string>
                );
    }
}
