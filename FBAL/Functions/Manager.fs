namespace FBAL.Functions
open System
open System.Collections.Generic
open FBAL.Functions.Utilities
open FBAL.Functions.Generics
open Models

module Manager = 
    ///
    ///
    type CategoryRecord = {
        Category : CategoryEnum;
        Distance : float
    }
    ///
    ///
    let callback (reader : IO.StreamReader) url = 
        let html = reader.ReadToEnd()
        html
    ///
    ///
    let fetch = fetchUrl callback
    ///
    ///
    let createNgram seq = generateNGram 5 seq
    ///
    ///
    let clean a = a
    ///
    ///
    let convertToMap seq = generateMap seq
    ///
    ///
    let Process input = ProcessResource fetch clean createNgram input
    ///
    ///
    let Compare example input = getDistance example input // add category here
