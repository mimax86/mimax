namespace Domain

open System

type Customer =
    { FirstName : string
      LastName : string
      Age : int }

type Client = 
    { Name : string }

type Account =
    { Client : Client; Balance : decimal; AccountId : Guid }

type Transaction =
    { Operation : char; Amount : decimal; AccountId : Guid; CreationTime : DateTime }