module UnitTests

open FsUnit
open FBAL.NgramManager
open NUnit.Framework

[<Test>]
let ``Test this text`` () =
    let text = "lorem ipsum olo golgork"
    let a = NgramProfileGenerator 5 text
    printfn "%A" a
    a |> should not' (be Null)

