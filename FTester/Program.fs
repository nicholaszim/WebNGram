// Learn more about F# at http://fsharp.org
// See the 'F# Tutorial' project for more help.
//let a = NgramProfileGenerator 5 text
// return an integer exit code
module Program

open System
open FBAL.NgramManager
open FBAL.NgramManager.Dublicates
open Models

[<EntryPoint>]
let main argv = 
    let text = 
        "1As part of exc2ersise to better understand F# which I am currently learning , I wrote function to split given string into n-grams. 
1) I would like to receive feedback about my function : can this be written simpler or in more efficient way?

2) My overall goal is to write function that returns string similarity (on 0.0 .. 1.0 scale) based on n-gram similarity; Does this approach works well for short strings comparisons , or can this method reliably be used to compare large strings (like articles for example). 

3) I am aware of the fact that n-gram comparisons ignore context of two strings. What method would you suggest to accomplish my goal?"
    let text1 = 
        "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua."
    let text2 = 
        "Lorem2 ip3sum dolor sit am5et, consectetur adipiscing elit, sed do ei0usmod tempor inci4didunt ut labore et dolore magna aliqua."
    printfn "%A" argv
    0
