
#r "C:\\Users\\mi\\.nuget\\packages\\fsharp.data.sqlclient\\2.0.6\\lib\\net40\\FSharp.Data.SqlClient.dll"
#r "C:\\Users\\mi\\.nuget\\packages\\sqlprovider\\1.1.71\\lib\\net451\\FSharp.Data.SqlProvider.dll"

open FSharp.Data
open FSharp.Data.Sql

let [<Literal>] Conn =
    "Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=AdventureWorks.LT;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"

type GetCustomers = 
    SqlCommandProvider<"SELECT * FROM SalesLT.Customer", Conn>

let customers =
    GetCustomers.Create(Conn).Execute() |> Seq.toArray

let customer = customers.[0]

printfn "%s %s works for %A" customer.FirstName customer.LastName customer.CompanyName

type AdventureWorks = SqlProgrammabilityProvider<Conn>

//let category = new AdventureWorks.SalesLT.Tables.ProductCategory()
//let r1 = category.AddRow("Mittens", Some 3, Some (System.Guid.NewGuid()))
//category.AddRow("Long Shorts", Some 3, Some (System.Guid.NewGuid()))
//category.AddRow("Wooly Hats", Some 4, Some (System.Guid.NewGuid()))
//category.Update()

type GetCategories = 
    SqlCommandProvider<"SELECT * FROM SalesLT.ProductCategory", Conn>

let categories =
    GetCategories.Create(Conn).Execute() |> Seq.toArray

//------------------------SQLProvider----------------------------------------------------

type AdventureWorksProvider = SqlDataProvider<ConnectionString = "Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=AdventureWorks.LT;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False", UseOptionTypes = true>

let context = AdventureWorksProvider.GetDataContext()

let custimers = 
    query {
        for customer in context.SalesLt.Customer do
        take 10
    } |> Seq.toArray

let customer = customers.[0]

