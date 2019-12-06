#r "C:\\Users\\mi\\.nuget\\packages\\fsharp.data\\3.3.2\\lib\\net45\\FSharp.Data.dll"

open FSharp.Data

type AllAnimalsResponse =
    JsonProvider<"https://localhost:44392/weatherforecast/animals">

let names =
    AllAnimalsResponse.GetSamples()
    |> Seq.map(fun a -> a.Name)
    |> Seq.toArray

