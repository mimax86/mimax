module Mimax.ATM.Auditing

open Mimax.ATM.Operations
open Mimax.ATM.Domain
open Mimax.ATM.Domain.Transactions

/// Logs to the console
let printTransaction _ accountId transaction =
    printfn "Account %O: %s of %M" accountId (serializeOperation(transaction.Operation)) transaction.Amount

// Logs to both console and file system
let composedLogger = 
    let loggers =
        [ FileRepository.writeTransaction
          printTransaction ]
    fun accountId owner transaction ->
        loggers
        |> List.iter(fun logger -> logger accountId owner transaction)