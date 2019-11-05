/// Provides access to the banking API.
module Capstone5.Api

open Capstone5.Domain
open Capstone5.Operations
open System

/// Loads an account from disk. If no account exists, an empty one is automatically created.
let LoadAccount (customer:Customer) : RatedAccount =
    let load = FileRepository.tryFindTransactionsOnDisk >> Option.map Operations.loadAccount 
    let account = customer.Name |> load
    match account with
    | Some account -> account
    | None -> InCredit(CreditAccount { AccountId = Guid.NewGuid()
                                       Balance = 0M
                                       Owner = customer })

/// Deposits funds into an account.
let Deposit (amount:decimal) (customer:Customer) : RatedAccount =
    let ratedAccount = LoadAccount customer
    let depositWithAudit amount (ratedAccount:RatedAccount) =
        let accountId = ratedAccount.GetField (fun a -> a.AccountId)
        let owner = ratedAccount.GetField(fun a -> a.Owner)
        auditAs "deposit" Auditing.composedLogger deposit amount ratedAccount accountId owner
    depositWithAudit amount ratedAccount

/// Withdraws funds from an account that is in credit.
let Withdraw (amount:decimal) (customer:Customer) : RatedAccount =
    let ratedAccount = LoadAccount customer
    match ratedAccount with
    | InCredit creditAccount ->
        let withdrawWithAudit amount (CreditAccount account as creditAccount) =
            auditAs "withdraw" Auditing.composedLogger withdraw amount creditAccount account.AccountId account.Owner    
        withdrawWithAudit amount creditAccount
    | Overdrawn _ -> ratedAccount
                                 
/// Loads the transaction history for an owner. If no transactions exist, returns an empty sequence.
let LoadTransactionHistory(customer:Customer) : Transaction seq =
    let transactions = customer.Name |> FileRepository.tryFindTransactionsOnDisk 
    match transactions with
    | Some (_, _, transactions) -> transactions
    | None -> Seq.empty