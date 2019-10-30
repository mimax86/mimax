module Operations

open Domain

let getInitials customer =
    customer.FirstName.[0], customer.LastName.[0]

let isOlder age customer =
    customer.Age > age