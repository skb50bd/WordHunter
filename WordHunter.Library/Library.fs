namespace WordHunter.Library

open System.IO;
open System.Collections.Generic

module Hunter =
    let trim (word: string) =
        word.Trim()

    let toLower (word: string) =
        word.ToLower()

    let onlyAlpha (word: string) =
        word
        |> Seq.map System.Char.IsLetter
        |> Seq.filter (fun b -> not b)
        |> Seq.isEmpty

    let words =
        File.ReadAllLines("words.txt")
        |> Seq.map toLower
        |> Seq.filter onlyAlpha

    let wordsByLength =
        words
        |> Seq.groupBy (fun word ->  word.Length)
        |> dict

    let withMoreLettersThan containingLetters (wordsByLength: KeyValuePair<int, string seq>) =
        let count =
            containingLetters
            |> trim
            |> toLower
            |> fun x -> x.Length

        match count with
        | 0 -> true
        | _ -> wordsByLength.Key >= count

    let withMaybeLetterCount count (wordsByLength: KeyValuePair<int, string seq>) =
        match count with
        | Some 0 -> failwith "Invalid Letter Count"
        | Some x -> wordsByLength.Key = x
        | None _ -> true

    let maybeStartsWith str (word: string) =
        let cleanStr =
            str |> trim |> toLower

        match cleanStr.Length with
        | 0 -> true
        | _ -> word.StartsWith(cleanStr)

    let maybeEndsWith str (word: string) =
        let cleanStr =
            str |> trim |> toLower

        match cleanStr.Length with
        | 0 -> true
        | _ -> word.EndsWith(cleanStr)

    let maybeContains str (word: string) =
        Set(str |> trim |> toLower)
        |> Seq.map (fun ch -> word.Contains(ch))
        |> Seq.filter (fun b -> not b)
        |> Seq.isEmpty

    let maybeExcluding str (word: string) =
        Set(str |> trim |> toLower)
        |> Seq.map (fun ch -> word.Contains(ch))
        |> Seq.filter (fun b -> b)
        |> Seq.isEmpty

    let maybeInTheMiddle str (word: string) =
        let cleanStr =
            str |> trim |> toLower

        match cleanStr.Length with
        | 0 -> true
        | len ->
            let index =
                word.IndexOf(cleanStr)

            index > 0
            && index + len < (word.Length - 1)

    let findWords
        (containingLetters: string)
        (startsWith: string)
        (endsWith: string)
        (inTheMiddle: string)
        (excludingLetters: string)
        (wordLength: int option) =

        let matchingWords =
            wordsByLength
            |> Seq.filter (fun kv -> withMoreLettersThan containingLetters kv)
            |> Seq.filter (fun kv -> withMaybeLetterCount wordLength kv)
            |> Seq.collect (fun kv -> kv.Value)
            |> Seq.filter (fun word -> maybeStartsWith startsWith word)
            |> Seq.filter (fun word -> maybeEndsWith endsWith word)
            |> Seq.filter (fun word -> maybeContains containingLetters word)
            |> Seq.filter (fun word -> maybeExcluding excludingLetters word)
            |> Seq.filter (fun word -> maybeInTheMiddle inTheMiddle word)
            |> Seq.toList

        matchingWords