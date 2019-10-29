type Car = 
    { Manufacturer : string
      Engine : float32
      Doors : int }
let car1 =
    { Manufacturer = "Nissan"
      Engine = 1.4f
      Doors = 4 }

type Address = 
    { Street : string 
      Town : string
      City : string }
let address1 : Address =
    { Street = "Street1"
      Town = "Town1"
      City = "City1" }
let address2 : Address =
    { Street = "Street1"
      Town = "Town1"
      City = "City1" }

let compare1 = address1 = address2
let compare2 = address1.Equals(address2)
let compare3 = System.Object.ReferenceEquals(address1, address2)

type Customer = 
    { Forename : string
      Surname : string
      Age : int
      Address : Address
      EmailAddress : string }
let updateCustomer customer = 
    let rnd = new System.Random()
    let updatedCustomer =
        { customer with
           Age = rnd.Next(18, 45) }
    updatedCustomer

let customer1 = 
    { Forename = "Mikhail"
      Surname = "Khodin"
      Age = 33
      Address = 
       { Street = "Podmoskovniy bulvar"
         Town = "Moscow"
         City = "Moscow" }
      EmailAddress = "mikhail.khodin@clsa.com" }

let customer2 = updateCustomer customer1
