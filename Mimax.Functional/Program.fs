// Learn more about F# at http://fsharp.org
open Car
open System

[<EntryPoint>]
let main argv =
    //let items = [| "item"; "item"; "item" |]
    //printfn "Hello World from F#!"
    let mutable petrol = 100.0
    let getDestination() = 
        Console.Write("Print destination: ")
        Console.ReadLine()

    while true do
        try
            let destination = getDestination()
            printfn "Trying to drive to %s" destination
            petrol <- driveTo(petrol, destination)
            printfn "Made it to %s! You have %f petrol left" destination petrol
        with ex -> printf "ERROR: %s" ex.Message



    0 // return an integer exit code
