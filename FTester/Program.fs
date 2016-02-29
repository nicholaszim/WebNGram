// Learn more about F# at http://fsharp.org
// See the 'F# Tutorial' project for more help.
//let a = NgramProfileGenerator 5 text
// return an integer exit code
module Program

open System
open FBAL.NgramManager
open FBAL.NgramManager.Dublicates
open Models
open FBAL.Generics

[<EntryPoint>]
let main argv = 
    let text = 
        "1As part of exc2ersise to better understand F# which I am currently learning , I wrote function to split given string into n-grams. 
1) I would like to receive feedback about my function : can this be written simpler or in more efficient way?

2) My overall goal is to write function that returns string similarity (on 0.0 .. 1.0 scale) based on n-gram similarity; Does this approach works well for short strings comparisons , or can this method reliably be used to compare large strings (like articles for example). 

3) I am aware of the fact that n-gram comparisons ignore context of two strings. What method would you suggest to accomplish my goal?"
    //let text2 = "Lorem Lorem Lorem"
    //let a = NgramProfileGenerator 5 text2
    //let b = Seq.item 0 a
    //let a = WindowText 5 text

    //let a = strip2 "oat" "glerk"
    //let b = NgramProfileGenerator2 5 text
    //let b = "glerk".StripChars("g")
    //printfn "%A" b
    let text1 = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua."
    let text2 = "Lorem2 ip3sum dolor sit am5et, consectetur adipiscing elit, sed do ei0usmod tempor inci4didunt ut labore et dolore magna aliqua."
//    let a = NgramProfileGenerator 5 text1
//    let b = NgramProfileGenerator3 5 text1
//    let compareSeqs = Seq.compareWith Operators.compare
//    let theSame = (compareSeqs a b = 0)
//
//    let test a b = Seq.fold (&&) true (Seq.zip a b |> Seq.map (fun (aa,bb) -> aa=bb))
//
//    let isSame = test a b

//    let result = NgramProfileGenerator4 5 text2
//    let dictionary = toIDict result
//    let model = CreateModel(CategoryEnum.IT, dictionary)
//    printfn "%A" model

    let sequence = generateNGram 5 text2
    let alist = ofSeq
//    let toNgram = toExample sequence
//    let cmon = ofSeq toNgram
//    printfn "%A" cmon
    printfn "%A" argv
    0
