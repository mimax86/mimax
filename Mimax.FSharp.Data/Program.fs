// Learn more about F# at http://fsharp.org

open System
open FSharp.Data

let [<Literal>] Conn =
    "Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=AdventureWorks.LT;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"

type GetCustomers = 
    SqlCommandProvider<"SELECT * FROM SalesLT.Customer", Conn>

type AdventureWorks = SqlProgrammabilityProvider<Conn>


type GetCategories = 
    SqlCommandProvider<"SELECT * FROM SalesLT.ProductCategory", Conn>


[<EntryPoint>]
let main argv =
     
    let customers =
        GetCustomers.Create(Conn).Execute() |> Seq.toArray
    
    let customer = customers.[0]
    
    printfn "%s %s works for %A" customer.FirstName customer.LastName customer.CompanyName
    
       
    let category = new AdventureWorks.SalesLT.Tables.ProductCategory()
    let r1 = category.AddRow("Mittens", Some 3, Some (System.Guid.NewGuid()))
    category.AddRow("Long Shorts", Some 3, Some (System.Guid.NewGuid()))
    category.AddRow("Wooly Hats", Some 4, Some (System.Guid.NewGuid()))
    category.Update() |> ignore

    
    let categories =
        GetCategories.Create(Conn).Execute() |> Seq.toArray
    
    0 // return an integer exit code
