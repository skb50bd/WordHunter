using System.Collections.Specialized;
using System.Web;

namespace WordHunter.WebUI;

public record WordFinderRequest
{
    public WordFinderRequest()
    {
        ContainingLetters = string.Empty;
        ExcludingLetters  = string.Empty;
        StartsWith      = string.Empty;
        EndsWith        = string.Empty;
        InTheMiddle     = string.Empty;
        LetterCount     = 0;
        PageIndex       = 0;
        PageSize        = 20;
    }

    public string ContainingLetters { get; set; }
    public string ExcludingLetters { get; set; }
    public string StartsWith { get; set; }
    public string EndsWith { get; set; }
    public string InTheMiddle { get; set; }
    public int LetterCount { get; set; }
    public int PageIndex { get; set; }
    public int PageSize { get; set; }

    private static string ToQueryString(NameValueCollection nvc)
    {
        var array =
            (from key in nvc.AllKeys
             from value in nvc.GetValues(key) ?? new string[0]
             select string.Format(
                    "{0}={1}",
                    HttpUtility.UrlEncode(key),
                    HttpUtility.UrlEncode(value))
            ).ToArray();

        return "?" + string.Join("&", array);
    }

    public override string ToString()
    {
        var query = HttpUtility.ParseQueryString(string.Empty);

        if (ContainingLetters.Length > 0)
        {
            query.Add("containingLetters", ContainingLetters);
        }

        if (StartsWith.Length > 0)
        {
            query.Add("startsWith", StartsWith);
        }

        if (EndsWith.Length > 0)
        {
            query.Add("endsWith", EndsWith);
        }

        if (InTheMiddle.Length > 0)
        {
            query.Add("inTheMiddle", InTheMiddle);
        }

        if (ExcludingLetters.Length > 0)
        {
            query.Add("excludingLetters", ExcludingLetters);
        }

        if (LetterCount > 0)
        {
            query.Add("letterCount", LetterCount.ToString());
        }

        query.Add("pageIndex", PageIndex.ToString());
        query.Add("pageSize", PageSize.ToString());

        var queryString = ToQueryString(query);

        return queryString;
    }
}