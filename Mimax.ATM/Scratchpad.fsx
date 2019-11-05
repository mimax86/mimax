#load "Domain.fs"
#load "Operations.fs"

open Mimax.ATM.Operations
open Mimax.ATM.Domain
open System

type Command =
    | Withdrawal
    | Deposit
    | Exit

let tryParseCommand c =
    match c with
    | 'd' -> Some Deposit
    | 'w' -> Some Withdrawal
    | 'x' -> Some Exit
    | _ -> None

tryParseCommand 'x'
