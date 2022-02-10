namespace WordHunter.Services;

public class Hunter
{
    private readonly Words _words;

    public Hunter(Words words)
    {
        _words = words;
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

        if (letterCount is > 0)
        {
            matchingWords =
                _words.WordsByLength.ContainsKey(letterCount.Value)
                    ? _words.WordsByLength[letterCount.Value]
                    : new string[0];
        }
        else if (containingLetters.Length > 0)
        {
            matchingWords =
                _words.WordsByLength
                    .Where(kv => kv.Key >= containingLetters.Length)
                    .SelectMany(kv => kv.Value);
        }
        else
        {
            matchingWords = _words.AllWords;
        }

        var excludedLettersSet =
            excludedLetters
                .ToCharArray()
                .ToHashSet();

        if (startsWith.Length > 0)
        {
            matchingWords =
                matchingWords
                    .Where(w => w.StartsWith(startsWith));
        }

        if (endsWith.Length > 0)
        {
            matchingWords =
                matchingWords
                    .Where(w => w.EndsWith(endsWith));
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
