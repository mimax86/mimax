type Disk = 
    | HardDisk of RPM:int * Platters:int
    | SolidState
    | MMC of NumberOfPins:int

let hardDisk = HardDisk(RPM=250, Platters=7)
let solidState = SolidState
let mmc = MMC(6)

let seek disk =
    match disk with
    | HardDisk _ -> "Seeking loudly at a reasonable speed!"
    | MMC _ -> "Seeking quietly but slowly!"
    | SolidState _ -> "Already found it!"

seek hardDisk

let describe disk =
    match disk with 
    | SolidState _ -> "I am newfangled SSD."
    | MMC(1) -> "I have only 1 pin"
    | MMC(pin) when pin < 5 -> "I'm an MMC with few pins"
    | MMC(pin) -> sprintf "I'm an MMC with %d pins"  pin
    | HardDisk(5400,_) -> "I'm a slow hard disk"
    | HardDisk(_,7) -> "I have 7 spindles!"
    | HardDisk _ -> "I'm a hard disk"


describe (HardDisk(10000, 10))
    
type Customer =
    { Name : string
      YearsPassed : int
      SafetyScore : int option }

let calculateAnnualPremiumUsd customer =
    match customer with 
    | { SafetyScore = Some 0 } -> 250
    | { SafetyScore = Some score } when score < 0 -> 400
    | { SafetyScore = Some score } when score > 0 -> 150
    | { SafetyScore = None } -> 
        printfn "No score supplied! Using temporary premium."
        300
    
calculateAnnualPremiumUsd { Name = "Fred"; YearsPassed = 1980; SafetyScore = Some 1}

let tryLoadCustomer id =
    if [|2..7|] |> Array.contains id then Some (sprintf "Customer<%d>" id)
    else None
    
let ids = [0..10]

ids |> List.choose tryLoadCustomer