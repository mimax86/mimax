open System

type Account =
    { Id : Guid
      Holder : string 
      Balance : decimal }

type Transaction =
    { Id : Guid
      AccountId : Guid
      Amount : decimal }
    
let output (value:string) = Console.WriteLine value
let input = Console.ReadLine

let persistAccount (account:Account) =
    let stream = IO.File.AppendText(account.Id.ToString() + "txt")
    stream.Write(account.ToString())
    stream.Dispose()

let persistTransaction (transaction:Transaction) =
    let stream = IO.File.AppendText(transaction.AccountId.ToString() + "txt")
    stream.Write(transaction.ToString())
    stream.Dispose()

let getAmount value =
    let parsed, amount = value |> Decimal.TryParse
    if(parsed) then amount
    else failwith "Value is not allowed as amount"

let createAccount =
    output "Account creation"
    output "Provide holder name: "
    let holder = input()
    let account = { Id = Guid.NewGuid(); Holder = holder; Balance = 0m }
    output ("Account created: " + account.ToString())
    account

let apply (account:Account) (transaction:Transaction) = 
    let balance = account.Balance + transaction.Amount
    if (balance < 0m) then account
    else { account with Balance = balance }

let createDeposit (account:Account) = 
    output "Provide deposit amount: "
    let amount = input() |> getAmount
    if (amount < 0m) then failwith "Signed value is not allowed"
    else { Id = Guid.NewGuid(); AccountId = account.Id; Amount = amount}

let createWithdrawal (account:Account) = 
    output "Provide withdrawal amount: "
    let amount = input() |> getAmount
    if (amount < 0m) then failwith "Signed value is not allowed"
    else { Id = Guid.NewGuid(); AccountId = account.Id; Amount = -amount}

let createTransaction (account:Account) =
    output "Select Deposit(D) or Withdrawal(W): "
    let transaction = input().ToUpper();
    if (transaction = "D") then createDeposit account
    elif (transaction = "W") then createWithdrawal account
    else failwith "Symbol is not allowed"


let mutable account = createAccount
persistAccount account

let initial = createDeposit account
account <- apply account initial
persistTransaction initial
output(account.ToString())

while true do
    try
        let transaction = createTransaction account
        account <- apply account transaction
        persistTransaction transaction
        output(account.ToString())
    with ex -> printf "ERROR: %s" ex.Message
