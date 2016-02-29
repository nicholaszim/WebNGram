namespace FBAL
open Models

open System
open System.Net
open System.IO

module NgramManager = 
    module Utilities = 
        /// <summary>
        /// FUnction for fetching data from internet resourse using urls
        /// </summary>
        /// <param name="callback"></param>
        /// <param name="url"></param>
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

        let stripNumbers (text : string) =
            text
            |> Seq.where (fun c -> not (Char.IsNumber(c)))
        let stripPunctuation (text : string) =
            text
            |> Seq.where (fun c -> not (Char.IsPunctuation(c)))
        let replaceBlanks character =
            match Char.IsWhiteSpace(character) with
            | true -> '_'
            | false -> character

        let stripString (text : string) =
            text
            |> Seq.where (fun c -> not (Char.IsNumber(c))) 
            |> Seq.where (fun c -> not (Char.IsPunctuation(c)))
            |> Seq.map (fun c -> replaceBlanks c)

        let stripResource resource =
            resource
            |> Seq.where (fun c -> not (Char.IsNumber(c))) 
            |> Seq.where (fun c -> not (Char.IsPunctuation(c)))
            |> Seq.map (fun c -> replaceBlanks c)

        let convertSeq ((a : char[]), b) =
            (String(a), Seq.length b)
        
        let striptoSeq (stripChars : string) (text : string) = 
            text |> Seq.where (fun c -> not (stripChars.Contains(c.ToString())))
        //|> Seq.map (fun (ngram, occurrences) -> (if ngram.Contains " " then ngram.Replace(" ", "_") else ngram), occurrences)
        let WindowText n text = text |> Seq.windowed n
    
    module Dublicates = 
        open Utilities
        
        /// <summary>
        /// Function for generation n-grams using specified text resource.
        /// </summary>
        /// <param name="n"></param>
        /// <param name="text"></param>
        let NgramProfileGenerator n (text : string) = 
            //Split the text into separate tokens conssiting only of letters and apostrophes.
            // digits and punctuation are discarded
            // pad the token with sufficient blanks before and after
            // Scan down each token, generating all possible
            //N-grams, for N=1 to 5. Use positions
            //that span the padding blanks, as well.
            //Hash into a table to find the counter for the
            //N-gram, and increment it. The hash table
            //uses a conventional collision handling
            //mechanism to ensure that each N-gram
            //gets its own counter
            //When done, output all N-grams and their counts
            // Sort those counts into reverse order by the
            //number of occurrences. Keep just the Ngrams
            //themselves, which are now in
            //reverse order of frequency
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
                   (String(ngram).Replace(" ", "_") |> strip2 "1234567890", Seq.length occurrences)) //custom extention

        let NgramProfileGenerator3 n (text : string) = 
            text
            |> striptoSeq "1234567890.,"
            |> Seq.map (fun item -> if Char.IsWhiteSpace(item) then '_' else item)
            |> Seq.windowed n
            |> Seq.groupBy id
            |> Seq.map (fun (ngram, occurrences) -> (String(ngram).Replace(" ", "_"), Seq.length occurrences))
    
    open Utilities
    open Dublicates
    open Models
        /// <summary>
        /// Current working n-gram construction algorithm. Takes text as source and an integer of n-gram size
        /// </summary>
        /// <param name="n"></param>
        /// <param name="text"></param>
    let NgramProfileGenerator4 n (text : string) =
        text
        |> stripString
        |> Seq.windowed n
        |> Seq.groupBy id
        |> Seq.map convertSeq //(fun (ngram, occurrences) -> String(ngram), Seq.length occurrences)

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

    /// <summary>
    /// Function that takes sequence<'T, 'U> as input and converts it into idictionary obejct.
    /// </summary>
    /// <param name="seq">sequence<'T, 'U></param>
    let toIDict seq =
        seq |> dict
    /// <summary>
    /// Function that creates Example model by using specified parameters as input
    /// </summary>
    /// <param name="category">Enum value</param>
    /// <param name="sequence">seq<string, int> value</param>
    let CreateModel (category, sequence) =
        let newExample = new Example(Category = category, NGrams = sequence)
        newExample

    let createModel category sequence =
        new Example(Category = category, NGrams = sequence)

    let ProcessResource fetch clean createNGram resource = 
         resource
         |> fetch
         |> clean
         |> createNGram

    let StoreResource convert add resource = 
        resource
        |> convert
        |> add
        
    /// <summary>
    /// Wrapper for text processing and n-gram creation functions
    /// </summary>
    /// <param name="url"></param>
    let TextProcessor(url) = 
        let myCallback (reader : IO.StreamReader) url = 
            let html = reader.ReadToEnd()
            html
        
        let html = fetchUrl myCallback url
        let ngrams = NgramProfileGenerator3 5 html
        ngrams
