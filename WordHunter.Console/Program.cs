using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using WordHunder.Lib;
using static WordHunter.Library.Hunter;

BenchmarkRunner.Run<Benchy>();

[MemoryDiagnoser]
public class Benchy
{
    public static Hunter hunter = new Hunter();

    [Benchmark]
    public void CSharp()
    {
        var words =
            hunter.FindWords(
                containingLetters: "atch",
                excludedLetters: "firsplnb",
                inTheMiddle: "",
                startsWith: "",
                endsWith: "atch",
                letterCount: 5);
    }

    [Benchmark]
    public void FSharp()
    {
        var words2 =
            findWords(
                containingLetters: "atch",
                startsWith: "",
                endsWith: "atch",
                inTheMiddle: "",
                excludingLetters: "firsplnb",
                wordLength: 5);
    }
}