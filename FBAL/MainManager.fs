namespace FBAL

open Models
open System.Net
open System

module Utilities = 
    let fetchUrl callback url = 
        let request = WebRequest.Create(Uri(url))
        use response = request.GetResponse()
        use stream = response.GetResponseStream()
        use reader = new IO.StreamReader(stream)
        callback reader url
    
    let strip (text : string) (stripChars : string) = 
        text.Split(stripChars.ToCharArray(), StringSplitOptions.None) |> String.Concat
    
    let strip2 (stripChars : string) (text : string) = 
        text
        |> Seq.where (fun c -> not (stripChars.Contains(c.ToString())))
        |> String.Concat
    
    let stripNumbers (text : string) = text |> Seq.where (fun c -> not (Char.IsNumber(c)))
    let stripPunctuation (text : string) = text |> Seq.where (fun c -> not (Char.IsPunctuation(c)))
    
    let replaceBlanks character = 
        match Char.IsWhiteSpace(character) with
        | true -> '_'
        | false -> character
    
    let stripString (text : string) = 
        text
        |> Seq.where (fun c -> not (Char.IsNumber(c)))
        |> Seq.where (fun c -> not (Char.IsPunctuation(c)))
        |> Seq.map (fun c -> replaceBlanks c)
    
    let convertSeq ((a : char []), b) = (String(a), Seq.length b)
    let striptoSeq (stripChars : string) (text : string) = 
        text |> Seq.where (fun c -> not (stripChars.Contains(c.ToString())))
    let WindowText n text = text |> Seq.windowed n

open Utilities

module Dublicates = 
    let NgramProfileGenerator n (text : string) = 
        text
        |> Seq.windowed n
        |> Seq.groupBy id
        |> Seq.map (fun (ngram, occurrences) -> 
               (if String(ngram).Contains " " then String(ngram).Replace(" ", "_")
                else String(ngram)), Seq.length occurrences)
    
    let NgramProfileGenerator2 n (text : string) = 
        text
        |> Seq.windowed n
        |> Seq.groupBy id
        |> Seq.map 
               (fun (ngram, occurrences) -> 
               (String(ngram).Replace(" ", "_") |> strip2 "1234567890", Seq.length occurrences))
    
    let NgramProfileGenerator3 n (text : string) = 
        text
        |> striptoSeq "1234567890.,"
        |> Seq.map (fun item -> 
               if Char.IsWhiteSpace(item) then '_'
               else item)
        |> Seq.windowed n
        |> Seq.groupBy id
        |> Seq.map (fun (ngram, occurrences) -> (String(ngram).Replace(" ", "_"), Seq.length occurrences))

    let NgramProfileGenerator4 n (text : string) =
        text
        |> stripString
        |> Seq.windowed n
        |> Seq.groupBy id
        |> Seq.map convertSeq

module Current = 
    let createModel category sequence = new Example(Category = category, NGrams = sequence)
    
    let CreateModel(category, sequence) = 
        let newExample = new Example(Category = category, NGrams = sequence)
        newExample

module Generics = 
    let stripResource resource = 
        resource
        |> Seq.where (fun c -> not (Char.IsNumber(c)))
        |> Seq.where (fun c -> not (Char.IsPunctuation(c)))
        |> Seq.map (fun c -> replaceBlanks c)
    
    let toIDict seq = seq |> dict
    let toArray seq = 
        seq
        |> Seq.toArray
        |> Array.map (fun (a, b) -> a, b)
    let toMySeq array =
        array
        |> Array.toSeq
        |> Seq.map (fun (a, b) -> a, b)

    let ofArray (arr: 'T array) = new System.Collections.Generic.List<'T>(arr)
    let ofSeq (arr: 'T seq) = new System.Collections.Generic.List<'T>(arr)

    //let toGenList (seq : seq<'a*'b>) = new System.Collections.Generic.List<System.Tuple<'a, 'b>>(seq)
    
    let generateNGram n resource = 
        resource
        |> stripResource
        |> Seq.windowed n
        |> Seq.groupBy id
        |> Seq.map convertSeq //(fun (ngram, occurrences) -> String(ngram), Seq.length occurrences)
    
    let GenerateNGram(n, resource) = 
        resource
        |> stripResource
        |> Seq.windowed n
        |> Seq.groupBy id
        |> Seq.map convertSeq
    
    let ProcessResource fetchFn cleanFn createNGramFn resource = 
        resource
        |> fetchFn
        |> cleanFn
        |> createNGramFn
    
    let StoreResource convertFn addFn resource = 
        resource
        |> convertFn
        |> addFn

    let CreateCategoryFn processFn storeFn input = 
        input
        |> processFn
        |> storeFn

    let FindSimilarityFn compareFn example input = 
        input
        |> compareFn example

open Generics

module Working =
    let myCallback (reader : IO.StreamReader) url = 
            let html = reader.ReadToEnd()
            html
    let fetch = fetchUrl myCallback
    let createNGram seq = generateNGram 5 seq
    let clean a = a // as placeholder
    let convert = toIDict
    let add a = ()

    let Process = ProcessResource fetch clean createNGram
    let Store seq = StoreResource convert add seq

    /// <summary>
    /// Function for category creation. Needs url as string for processing
    /// </summary>
    let CreateCategory = CreateCategoryFn Process Store

