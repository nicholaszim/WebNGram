namespace FBAL

open System
open System.Net
open System.IO

module NgramManager = 
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
    
    //|> Seq.map (fun (ngram, occurrences) -> (if ngram.Contains " " then ngram.Replace(" ", "_") else ngram), occurrences)
    let WindowText n text = text |> Seq.windowed n


    let TextProcessor(url) =
        let myCallback (reader: IO.StreamReader) url =
            let html = reader.ReadToEnd()
            html
        
        let html = fetchUrl myCallback url
        let ngrams = NgramProfileGenerator 5 html
        ngrams
