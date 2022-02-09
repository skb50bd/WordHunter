namespace WordHunder.Lib;

public class Hunter
{
    private readonly IDictionary<int, IList<string>> _wordsDict;

    public Hunter()
    {
        _wordsDict =
            File.ReadAllLines("words.txt")
                .Select(w => w.Trim().ToLowerInvariant())
                .GroupBy(w => w.Length)
                .OrderBy(g => g.Key)
                .ToDictionary(
                    g => g.Key,
                    g => g.ToList() as IList<string>
                );

    }

    public IList<string> FindWords(
        string containingLetters = "",
        string startsWith = "",
        string endsWith = "",
        string inTheMiddle = "",
        string excludedLetters = "",
        int? letterCount = null)
    {
        containingLetters =
            containingLetters
                .Trim()
                .ToLowerInvariant();

        startsWith =
            startsWith
                .Trim()
                .ToLowerInvariant();

        endsWith =
            endsWith
                .Trim()
                .ToLowerInvariant();

        inTheMiddle =
            inTheMiddle
                .Trim()
                .ToLowerInvariant();

        excludedLetters =
            excludedLetters
                .Trim()
                .ToLowerInvariant();

        IEnumerable<string> matchingWords =
            new List<string>();

        if (letterCount is not null)
        {
            matchingWords =
                _wordsDict[letterCount.Value];
        }
        else if (containingLetters.Length > 0)
        {
            matchingWords =
                _wordsDict
                    .Where(kv => kv.Key >= containingLetters.Length)
                    .SelectMany(kv => kv.Value);
        }
        else
        {
            matchingWords =
                _wordsDict
                    .SelectMany(kv => kv.Value);
        }

        var excludedLettersSet =
            excludedLetters
                .ToCharArray()
                .ToHashSet();

        if (containingLetters.Length > 0)
        {
            matchingWords =
                matchingWords
                    .Where(w =>
                    {
                        foreach (var ch in containingLetters)
                        {
                            if (w.Contains(ch) is false)
                            {
                                return false;
                            }
                        }

                        return true;
                    });
        }

        if (startsWith.Length > 0)
        {
            matchingWords =
                matchingWords
                    .Where(w => w.StartsWith(startsWith));
        }

        if (inTheMiddle.Length > 0)
        {
            matchingWords =
                matchingWords
                    .Where(w =>
                    {
                        var index =
                            w.IndexOf(inTheMiddle);

                        return
                            index is > 0
                            && (w.Length - 1) > (index + inTheMiddle.Length);
                    });
        }

        if (endsWith.Length > 0)
        {
            matchingWords =
                matchingWords
                    .Where(w => w.EndsWith(endsWith));
        }

        if (excludedLetters.Length > 0)
        {
            matchingWords =
                matchingWords
                    .Where(w =>
                        w.All(ch =>
                            excludedLetters
                                .Contains(ch) is false));
        }

        return matchingWords.ToList();
    }
}
