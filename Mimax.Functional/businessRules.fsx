type CustomerId = CustomerId of string
//type Address = Address of string
//type Email = Email of string
//type Telephone = Telephone of string
type ContactDetails =
    | Address of string
    | Telephone of string
    | Email of string

type Customer =
    { CustomerId : CustomerId
      PrimaryContactDetails : ContactDetails 
      SecondaryContactDetails : ContactDetails option}

let createCustomer customerId contactDetails =
    { CustomerId = customerId
      PrimaryContactDetails = contactDetails
      SecondaryContactDetails = None}

let newCustomer = createCustomer (CustomerId "C-123") (Email "nicki@SuperCorp.com")

type GeniuneCustomer = GeniuneCustomer of Customer

let validateCustomer customer =
    match customer.PrimaryContactDetails with
    | Email e when e.EndsWith "SuperCorp.com" -> Some(GeniuneCustomer customer)
    | Address _ | Telephone _ -> Some(GeniuneCustomer customer)
    | Email _ -> None

let sendWelcomeEmail (GeniuneCustomer customer) =
    printfn "Hello, %A, welcome to our site!" customer.CustomerId

