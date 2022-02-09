using WordHunder.Lib;

var hunter = new Hunter();

var words =
    hunter.FindWords(
        containingLetters: "atch",
        excludedLetters: "firsplnb",
        inTheMiddle: "",
        startsWith: "",
        endsWith: "atch",
        letterCount: 5);

foreach (var word in words)
{
    Console.WriteLine(word);
}
