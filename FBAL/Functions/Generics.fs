namespace FBAL.Functions

open System
open System.Collections.Generic
open FBAL.Functions.Utilities
open Models

module Generics = 
    ///
    ///
    let generateMap seq = seq |> Map.ofSeq
    ///
    ///
    let seqFromMap (map : Map<'a, 'b>) = map |> Map.toSeq
    ///
    ///
    let generateDict seq = seq |> dict
    ///
    ///
    let generateArray seq = 
        seq
        |> Seq.toArray
        |> Array.map (fun (a, b) -> a, b)
    ///
    ///
    let mutateSeq seq = 
        seq |> Seq.map (fun (a, b) -> new Ngram(Key = a, Value = b))
    ///
    ///
    let stripResource resource = 
        resource
        |> Seq.where (fun c -> not (Char.IsNumber(c)))
        |> Seq.where (fun c -> not (Char.IsPunctuation(c)))
        |> Seq.map (fun c -> replaceBlanks c)
    ///
    ///
    let generateNGram n resource = 
        resource
        |> stripResource
        |> Seq.windowed n
        |> Seq.groupBy id
        |> Seq.map convertSeq
    ///
    ///
    let ProcessResource fetchFn cleanFn createNGramFn resource = 
        resource
        |> fetchFn
        |> cleanFn
        |> createNGramFn
    ///
    ///
    let StoreResource convertFn addFn resource = 
        resource
        |> convertFn
        |> addFn
    ///
    ///
    let CreateCategoryFn processFn storeFn input = 
        input
        |> processFn
        |> storeFn
    ///
    ///
    let cosineSim example input = 
        let dot = Seq.sum (Seq.map2 (*) example input)
        let magnitude v = Math.Sqrt(Seq.sum (Seq.map2 (*) v v))
        dot / (magnitude example * magnitude input)
    ///
    ///
    let getSeqVal seq = seq |> Seq.map snd
    ///
    ///
    let getMapVal map = 
        map
        |> Map.toSeq
        |> Seq.map snd
    ///
    ///
    let desparseMap map1 map2 = Map.filter (fun k _ -> Map.containsKey k map2) map1
    ///
    ///
    let seqFloatValue seq = seq |> Seq.map (fun (a, b) -> (a, float b))
    ///
    ///
    let getDistance example input = 
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
        cosineSim exampleVal inputVal
