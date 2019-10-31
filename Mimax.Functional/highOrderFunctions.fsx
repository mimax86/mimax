open System

type Customer = { Age : int }

//high order function
let printCustomerAge writer customer = 
    let grade =
        if customer.Age < 12 then "Child"
        elif customer.Age < 18 then "Teenager"
        else "Adult"
    writer grade

//pipeline
{ Age = 10} |> printCustomerAge Console.WriteLine

//partially implemented function
let printToConsole = printCustomerAge Console.WriteLine
printToConsole { Age = 10 }
printToConsole { Age = 20 }
printToConsole { Age = 30 }