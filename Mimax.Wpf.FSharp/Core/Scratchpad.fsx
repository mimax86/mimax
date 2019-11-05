#load "Domain.fs"
#load "Operations.fs"
#r @"..\packages\Newtonsoft.Json.12.0.2\lib\net45\Newtonsoft.Json.dll"

open Capstone5.Operations
open Capstone5.Domain
open System
open Newtonsoft.Json

let txn =
    { Amount = 1000M
      Timestamp = DateTime.UtcNow
      Operation = "withdraw"}

let serialized = txn |> JsonConvert.SerializeObject
let deserialized = JsonConvert.DeserializeObject<Transaction> serialized


