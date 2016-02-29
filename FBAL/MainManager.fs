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

    let toExample seq  =
        seq
        |> Seq.map (fun (a, b) -> new Ngram(Key = a, Value = b))

    let getFloatValue seq =
        seq |> Seq.map(fun (a, b) -> (a, float b))

//    let toGenList (seq : seq<'a*'b>) = new System.Collections.Generic.List<System.Tuple<'a, 'b>>(seq)
    
    /// <summary>
    /// N-gram creation algorithm. Final version.
    /// </summary>
    /// <param name="n"></param>
    /// <param name="resource"></param>
    let generateNGram n resource = 
        resource
        |> stripResource
        |> Seq.windowed n
        |> Seq.groupBy id
        |> Seq.map convertSeq //(fun (ngram, occurrences) -> String(ngram), Seq.length occurrences)
    /// <summary>
    /// N-gram creation algorithm. Final version. C# version
    /// </summary>
    /// <param name="n"></param>
    /// <param name="resource"></param>
    let GenerateNGram(n, resource) = 
        resource
        |> stripResource
        |> Seq.windowed n
        |> Seq.groupBy id
        |> Seq.map convertSeq
    /// <summary>
    /// Wrapper for text resource fetching, cleaning and ngram creating
    /// </summary>
    /// <param name="fetchFn"></param>
    /// <param name="cleanFn"></param>
    /// <param name="createNGramFn"></param>
    /// <param name="resource"></param>
    let ProcessResource fetchFn cleanFn createNGramFn resource = 
        resource
        |> fetchFn
        |> cleanFn
        |> createNGramFn
    /// <summary>
    /// Wrapper for n-gram convertion and db storing
    /// </summary>
    /// <param name="convertFn"></param>
    /// <param name="addFn"></param>
    /// <param name="resource"></param>
    let StoreResource convertFn addFn resource = 
        resource
        |> convertFn
        |> addFn
    /// <summary>
    /// Wrapper for n-gram category creation and storing into db.
    /// </summary>
    /// <param name="processFn"></param>
    /// <param name="storeFn"></param>
    /// <param name="input"></param>
    let CreateCategoryFn processFn storeFn input = 
        input
        |> processFn
        |> storeFn

    /// <summary>
    /// cosine similarity function. Get similarity of ngrams-examples and input-ngrams
    /// </summary>
    /// <param name="example"></param>
    /// <param name="input"></param>
    let cosineSim example input =
        let dot = Seq.sum(Seq.map2 ( * ) example input)
        let magnitude v = Math.Sqrt (Seq.sum (Seq.map2 ( * ) v v))
        dot / (magnitude example * magnitude input)

    // functions for seqs convertion. Additional preparation for distance calculation
    let getvalue seq = seq |> Seq.map snd
    let desparse seq1 seq2 = Seq.filter (fun (a, _) -> Seq.contains (a, _) seq2) seq1
    let prepare seq = seq |> getFloatValue

    /// <summary>
    /// Get similarity of ngrams using example n-gram sequence and input n-gram sequence
    /// </summary>
    /// <param name="example"></param>
    /// <param name="input"></param>
    let getDistance example input =
        let Example = prepare example
        let Input = prepare input
        let exampleValue = desparse Example Input |> getvalue // this could be done with parallel but no time.
        let inputValue = desparse Input Example |> getvalue //
        cosineSim exampleValue inputValue
        
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

