// Learn more about F# at http://fsharp.org
// See the 'F# Tutorial' project for more help.
//let a = NgramProfileGenerator 5 text
// return an integer exit code
module Program

open System
open FBAL.NgramManager

[<EntryPoint>]
let main argv = 
    let text = 
        "As part of excersise to better understand F# which I am currently learning , I wrote function to split given string into n-grams. 
1) I would like to receive feedback about my function : can this be written simpler or in more efficient way?

2) My overall goal is to write function that returns string similarity (on 0.0 .. 1.0 scale) based on n-gram similarity; Does this approach works well for short strings comparisons , or can this method reliably be used to compare large strings (like articles for example). 

3) I am aware of the fact that n-gram comparisons ignore context of two strings. What method would you suggest to accomplish my goal?"
    //let text2 = "Lorem Lorem Lorem"
    //let a = NgramProfileGenerator 5 text2
    //let b = Seq.item 0 a
    //printfn "%A" a

    let myCallback (reader:IO.StreamReader) url = 
        let html = reader.ReadToEnd()
        let html1000 = html.Substring(0,1000)
        printfn "Downloaded %s. First 1000 is %s" url html1000
        html
    let google = fetchUrl myCallback "https://msdn.microsoft.com/ru-ru/library/system.io.stringreader(v=vs.110).aspx"
    printfn "%A" google
    printfn "%A" argv
    0
