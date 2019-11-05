module Mimax.ATM.Operations

open System
open Mimax.ATM.Domain
open Mimax.ATM

let classifyAccount account =
    if account.Balance >= 0M then (InCredit(CreditAccount account))
    else Overdrawn account

/// Withdraws an amount of an account (if there are sufficient funds)
let withdraw amount (CreditAccount account) =
    { account with Balance = account.Balance - amount }
    |> classifyAccount

/// Deposits an amount into an account
let deposit amount account =
    let account =
        match account with 
        | InCredit(CreditAccount account) -> account
        | Overdrawn account -> account
    { account with Balance = account.Balance + amount }
    |> classifyAccount

/// Runs some account operation such as withdraw or deposit with auditing.
let auditAs operationName audit operation amount account accountId accountOwner =
    let updatedAccount = operation amount account
    let transaction = { Operation = operationName; Amount = amount; Timestamp = DateTime.UtcNow }

    audit accountId accountOwner.Name transaction
    updatedAccount

/// Creates an account from a historical set of transactions
let loadAccount (owner, accountId, transactions) =
    let openingAccount = { AccountId = accountId; Balance = 0M; Owner = { Name = owner } } |> classifyAccount

    transactions
    |> Seq.sortBy(fun txn -> txn.Timestamp)
    |> Seq.fold(fun (account:RatedAccount) txn ->
        match txn.Operation with
        | Deposit -> account |> deposit txn.Amount
        | Withdrawal ->
            match account with
            | InCredit account -> account |> withdraw txn.Amount
            | _ -> account) openingAccount