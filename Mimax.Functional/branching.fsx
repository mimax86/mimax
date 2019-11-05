let getCreditLimit customer = 
    match customer with
    //| _, 1 -> 400
    | "medium", 1 -> 500
    | "good", years when years < 2 -> 750
    //| "good", 0 | "good", 1 -> 750
    | "good", 2 -> 1000
    | "good", _ -> 2000
    //| (_, 0) -> 250
    | _ -> 250


getCreditLimit ("medium", 1)

type Customer =
    { Balance : int; Name : string }

let handleCustomers customers = 
    match customers with
    | [] -> failwith "No customers supplied!"
    | [customer] -> printf "single customer, name is %s" customer.Name
    | [first; second] -> printf "Two customers. balance = %d" (first.Balance + second.Balance)
    | customers -> printf "Customers supplied: %d" customers.Length

//handleCustomers []
handleCustomers [ { Balance = 10; Name = "Joe" } ]