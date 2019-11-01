open Domain
open System

[<AutoOpen>]
module Serialization =
        
    let serialize transacion = 
       sprintf "%O***%c***%M"
           transacion.CreationTime
           transacion.Operation
           transacion.Amount

    let deserialize (line:string) id =
        let data = line.Split("***")
        { CreationTime = DateTime.Parse(data.[0])
          Operation = data.[1].[0]
          Amount = Decimal.Parse(data.[2])
          AccountId = id}

let consoleCommands = seq {
    while true do
        Console.Write "Type d/w/x: "
        yield Console.ReadKey().KeyChar 
        Console.WriteLine() }

let isValidCommand cmd = 
    [ 'd'; 'w'; 'x' ] |> List.contains cmd

let isStopCommand cmd = 
    cmd = 'x'

let getAmount command =
    Console.WriteLine()
    Console.Write "Enter Amount: "
    command, Console.ReadLine() |> Decimal.Parse

let printBalance account =
    printfn "Current balance is %M" account.Balance

let applyCommand (account:Account) (command:char, amount:decimal) =
    if command = 'd' then {account with Balance = account.Balance + amount }
    elif command = 'w' && account.Balance >= amount then {account with Balance = account.Balance - amount}
    else account

let applyTransaction (account:Account) (transaction:Transaction) =
    if transaction.Operation = 'd' then {account with Balance = account.Balance + transaction.Amount }
    elif transaction.Operation = 'w' && account.Balance >= transaction.Amount then {account with Balance = account.Balance - transaction.Amount}
    else account

let processWithAudit audit (account:Account) (command:char, amount:decimal) =
    let updatedAccount = (command, amount) |> applyCommand account
    let transaction = { Operation = command; Amount = amount; AccountId = account.AccountId; CreationTime = DateTime.UtcNow }
    audit account transaction
    printBalance updatedAccount
    updatedAccount

let saveToFile (account:Account) (transaction:Transaction) =
    let writer = IO.File.AppendText(account.AccountId.ToString() + ".txt")
    transaction |> Serialization.serialize |> writer.WriteLine
    writer.Dispose()

let processWithAuditFile = processWithAudit saveToFile  

let createAccount id =
     Console.Write "Please, enter your name: "
     let name = Console.ReadLine()
     { Client = { Name = name }; Balance = 0M; AccountId = Guid.Empty }

let readAccount id =
    let newAccount = createAccount id
    let lines = IO.File.ReadAllLines(id.ToString() + ".txt")
    let transactions =
        lines
        |> Seq.map (fun line -> deserialize line id)
        |> Seq.toList
    let loadedAccount = 
        transactions |> Seq.fold (fun account transaction -> applyTransaction account transaction ) newAccount
    loadedAccount

let getAccount (id:Guid) =
    if IO.File.Exists(id.ToString() + ".txt") then readAccount id
    else createAccount id

[<EntryPoint>]
let main argv =
    let openingAccount = getAccount Guid.Empty 

    let account =
        consoleCommands
        |> Seq.filter isValidCommand
        |> Seq.takeWhile (not << isStopCommand)
        |> Seq.map getAmount
        |> Seq.fold processWithAuditFile openingAccount
    Console.WriteLine account
    0 // return an integer exit code

    //let simon = { FirstName = "Simon"; LastName = "Honkonger"; Age = 25 }
    //if simon |> isOlder 18 then printf "%s is adult!" simon.FirstName
    //else printf "%s is a child" simon.FirstName
    //printf "Good night %s!!!" simon.FirstName

    // Learn more about F# at http://fsharp.org
    //open Car
    //open System

    //let items = [| "item"; "item"; "item" |]
    //printfn "Hello World from F#!"
    //let mutable petrol = 100.0
    //let getDestination() = 
    //    Console.Write("Print destination: ")
    //    Console.ReadLine()

    //while true do
    //    try
    //        let destination = getDestination()
    //        printfn "Trying to drive to %s" destination
    //        petrol <- driveTo(petrol, destination)
    //        printfn "Made it to %s! You have %f petrol left" destination petrol
    //    with ex -> printf "ERROR: %s" ex.Message