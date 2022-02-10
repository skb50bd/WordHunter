using Microsoft.AspNetCore.Mvc;
using WordHunter.Models;
using WordHunter.Services;

namespace WordHunter.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FindWordsController : ControllerBase
{
    private readonly Hunter _hunter;

    public FindWordsController(Hunter hunter)
    {
        _hunter = hunter;
    }

    [HttpGet]
    public ActionResult<PagedList<string>> Get(
        [FromQuery] string? containingLetters = null,
        [FromQuery] string? startsWith = null,
        [FromQuery] string? endsWith = null,
        [FromQuery] string? inTheMiddle = null,
        [FromQuery] string? excludingLetters = null,
        [FromQuery] int? letterCount = null,
        [FromQuery] int? pageSize = null,
        [FromQuery] int? pageIndex = null)
    {
        containingLetters ??= string.Empty;
        startsWith        ??= string.Empty;
        endsWith          ??= string.Empty;
        inTheMiddle       ??= string.Empty;
        excludingLetters  ??= string.Empty;
        letterCount         = letterCount is null ? 00 : letterCount;
        pageSize            = pageSize is not > 0 ? 20 : pageSize;
        pageIndex           = pageIndex is null   ? 00 : pageIndex;

        var words =
            _hunter
                .FindWords(
                    containingLetters,
                    startsWith,
                    endsWith,
                    inTheMiddle,
                    excludingLetters,
                    letterCount);

        return PagedList<string>.FromEnumerable(
            words,
            pageIndex.Value,
            pageSize.Value);
    }
}