namespace FBAL.Functions

open System
open System.Collections.Generic
open FBAL.Functions.Utilities
open Models
open FSharp.Collections.ParallelSeq
open Generics


module Parallel =
    let stripResourceP resource =
        resource
        |> PSeq.filter (fun c -> not (Char.IsNumber(c)))
        |> PSeq.filter (fun c -> not (Char.IsPunctuation(c)))
        |> PSeq.map (fun c -> replaceBlanks c)

    let generateNGramP n resource =
        resource
        |> stripResourceP
        |> Seq.windowed n
        |> PSeq.groupBy id
        |> PSeq.map convertSeq

    let cosineSimP example input =
        let dot = PSeq.sum (PSeq.map2 (*) example input)
        let magnitude v = Math.Sqrt(PSeq.sum (Seq.map2 (*) v v))
        dot / (magnitude example * magnitude input)

    let seqFloatValueP seq = seq |> PSeq.map (fun (a, b) -> (a, float b))

    let getDistanceP example input = 
        let convExample = 
            example
            |> seqFloatValue
            |> generateMap
        
        let convInput = 
            input
            |> seqFloatValue
            |> generateMap
        
        let exampleVal = desparseMap convExample convInput |> getMapVal
        let inputVal = desparseMap convInput convExample |> getMapVal
        double(cosineSimP exampleVal inputVal)
    let getDistanceP2 example input =
        let convExample = 
            example
            |> generateMap
        
        let convInput = 
            input
            |> generateMap
        let exampleVal = desparseMap convExample convInput |> getMapVal
        let inputVal = desparseMap convInput convExample |> getMapVal
        double(cosineSimP exampleVal inputVal)
