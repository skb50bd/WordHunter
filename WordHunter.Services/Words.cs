namespace WordHunter.Services;

public class Words
{
    public IDictionary<int, IList<string>> WordsByLength { get; }
    public IList<string> AllWords { get; }

    public Words()
    {
        var execPath =
            AppDomain.CurrentDomain.BaseDirectory;

        var dataFilePath =
            Path.Combine(execPath, "words.txt");

        AllWords =
            File.ReadAllLines(dataFilePath)
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
