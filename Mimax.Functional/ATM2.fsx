#load "Domain.fs"

open Domain
open System



let openingAccount = 
    { Client = { Name = "Mikhail" }; Balance = 0M; AccountId = Guid.Empty }

let isValidCommand c =
    c = 'd' || c = 'w' || c = 'x'

let isStopCommand c =
    c = 'x'

let getAmount (command:char) = 
    if command = 'd' then command, 50M
    elif command = 'w' then command, 25M
    else command, 0M

let processCommand (account:Account) (command:char, amount:decimal) =
    if command = 'd' then {account with Balance = account.Balance + amount }
    elif command = 'w' && account.Balance >= amount then {account with Balance = account.Balance - amount}
    else account    

let account =
    let commands = ['d'; 'w'; 'z'; 'f'; 'd'; 'x'; 'w']
    commands
    |> Seq.filter isValidCommand
    |> Seq.takeWhile (not << isStopCommand)
    |> Seq.map getAmount
    |> Seq.fold processCommand openingAccount