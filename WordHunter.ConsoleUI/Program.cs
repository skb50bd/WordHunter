using System.Text.Json;
using WordHunter.Models;

var words =
    new[]
    {
        "tch",
        "btch",
        "etch",
        "itch",
        "utch",
        "aitch",
        "batch",
        "bitch",
        "botch",
        "butch",
        "catch",
        "cotch",
        "cutch",
        "datch",
        "ditch",
        "dutch",
        "fetch",
        "fitch",
        "fotch",
        "gatch"
    };

var pagedList =
    PagedList<string>.FromEnumerable(words, 1, 4);

var json =
    JsonSerializer.Serialize(pagedList);

var obj =
    JsonSerializer.Deserialize<PagedList<string>>(json);

Console.WriteLine(obj == pagedList);
Console.WriteLine(pagedList);
Console.WriteLine(obj);