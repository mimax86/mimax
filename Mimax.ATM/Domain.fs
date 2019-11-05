namespace Mimax.ATM.Domain

open System

type Customer = { Name : string }
type Account = { AccountId : Guid; Owner : Customer; Balance : decimal }
type CreditAccount = CreditAccount of Account
type RatedAccount = 
    | InCredit of CreditAccount
    | Overdrawn of Account
    member this.GetField getter =
        match this with
        | InCredit (CreditAccount account) -> getter account
        | Overdrawn account -> getter account
type BankOperation =
    | Deposit
    | Withdrawal
type Transaction = { Timestamp : DateTime; Operation : BankOperation; Amount : decimal }
type Command = AccountCommand of BankOperation | Exit

module Transactions =
    let deserializeOperation operation : BankOperation =
        match operation with
        | "deposit" -> BankOperation.Deposit
        | "withdrawal" -> BankOperation.Withdrawal
        | _ -> failwith "Invalid operation"

    let serializeOperation operation =
        match operation with
        | BankOperation.Deposit -> "deposit"
        | BankOperation.Withdrawal -> "withdrawal"

    /// Serializes a transaction
    let serialize transaction =
        sprintf "%O***%s***%M***%b" transaction.Timestamp (serializeOperation(transaction.Operation)) transaction.Amount
    
    /// Deserializes a transaction
    let deserialize (fileContents:string) =
        let parts = fileContents.Split([|"***"|], StringSplitOptions.None)
        { Timestamp = DateTime.Parse parts.[0]
          Operation = deserializeOperation parts.[1]
          Amount = Decimal.Parse parts.[2] }