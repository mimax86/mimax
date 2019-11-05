module Mimax.ATM.Program

open System
open Mimax.ATM.Domain
open Mimax.ATM.Operations

let withdrawWithAudit amount (CreditAccount account as creditAccount) =
    auditAs Withdrawal Auditing.composedLogger withdraw amount creditAccount account.AccountId account.Owner

let depositWithAudit amount (account:RatedAccount) =
    let accountId = account.GetField (fun a -> a.AccountId)
    let owner = account.GetField(fun a -> a.Owner)
    auditAs Deposit Auditing.composedLogger deposit amount account accountId owner

let loadAccountOptional = Option.map Operations.loadAccount
let tryLoadAccountFromDisk = FileRepository.findTransactionsOnDisk >> loadAccountOptional

[<AutoOpen>]
module CommandParsing =
    let tryGetBankOperation cmd =
        match cmd with 
        | Exit -> None
        | AccountCommand op -> Some op
    let tryParseCommand c =
        match c with
        | 'd' -> Some (AccountCommand(Deposit))
        | 'w' -> Some (AccountCommand(Withdrawal))
        | 'x' -> Some Exit
        | _ -> None

[<AutoOpen>]
module UserInput =
    let commands = seq {
        while true do
            Console.Write "(d)eposit, (w)ithdraw or e(x)it: "
            yield Console.ReadKey().KeyChar
            Console.WriteLine() }
    
    let teyGetAmount command =
        Console.WriteLine()
        Console.Write "Enter Amount: "
        let amount = Console.ReadLine() |> Decimal.TryParse
        match amount with
        | true, amount -> Some(command, amount)
        | false, _ -> None

[<EntryPoint>]
let main _ =
    let account =
        Console.Write "Please enter your name: "
        let owner = Console.ReadLine()
        match (tryLoadAccountFromDisk owner) with
        | Some account -> account
        | None -> 
            { Balance = 0M
              AccountId = Guid.NewGuid()
              Owner = { Name = owner } } |> classifyAccount
    
    printfn "Current balance is £%M" (account.GetField(fun a -> a.Balance))

    let processCommand (account:RatedAccount) (command, amount) =
        printfn ""
        let account =
            match command with 
            | Deposit -> account |> depositWithAudit amount
            | Withdrawal ->
                match account with
                | InCredit account -> account |> withdrawWithAudit amount
                | Overdrawn _ ->
                    printfn "You cannot withdraw funds as your account is overdrawn!"
                    account
        printfn "Current balance is £%M" (account.GetField(fun a -> a.Balance))
        account

    let closingAccount =
        commands
        |> Seq.choose tryParseCommand
        |> Seq.takeWhile (fun command -> command <> Exit)
        |> Seq.choose tryGetBankOperation
        |> Seq.choose teyGetAmount        
        |> Seq.fold processCommand account
    
    printfn ""
    printfn "Closing Balance:\r\n %A" closingAccount
    Console.ReadKey() |> ignore

    0