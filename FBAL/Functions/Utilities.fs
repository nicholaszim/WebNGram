namespace FBAL.Functions

open Models
open System.Net
open System

module Utilities = 
    ///
    ///
    let strip (text : string) (stripChars : string) = 
        text.Split(stripChars.ToCharArray(), StringSplitOptions.None) |> String.Concat
    ///
    ///
    let strip2 (stripChars : string) (text : string) = 
        text
        |> Seq.where (fun c -> not (stripChars.Contains(c.ToString())))
        |> String.Concat
    ///
    ///
    let stripNumbers (text : string) = text |> Seq.where (fun c -> not (Char.IsNumber(c)))
    ///
    ///
    let stripPunctuation (text : string) = text |> Seq.where (fun c -> not (Char.IsPunctuation(c)))
    ///
    ///
    let replaceBlanks character = 
        match Char.IsWhiteSpace(character) with
        | true -> '_'
        | false -> character
    ///
    ///
    let stripString (text : string) = 
        text
        |> Seq.where (fun c -> not (Char.IsNumber(c)))
        |> Seq.where (fun c -> not (Char.IsPunctuation(c)))
        |> Seq.map (fun c -> replaceBlanks c)
    ///
    ///
    let convertSeq ((a : char []), b) = (String(a), Seq.length b)
    let convertSeq2 ((a : char []), b) = (String(a), float(Seq.length b))
    ///
    ///
    let striptoSeq (stripChars : string) (text : string) = 
        text |> Seq.where (fun c -> not (stripChars.Contains(c.ToString())))
    ///
    ///
    let WindowText n text = text |> Seq.windowed n
    ///
    ///
    let fetchUrl callback url = 
        let request = WebRequest.Create(Uri(url))
        use response = request.GetResponse()
        use stream = response.GetResponseStream()
        use reader = new IO.StreamReader(stream)
        callback reader url
