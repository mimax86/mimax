//let drive(petrol, distance) = 
//    if distance = "far" then petrol / 2.0
//    elif distance = "medium" then petrol - 10.0
//    else petrol - 1.0

let getConsumption(destination) = 
    if destination = "Home" then 25
    else if destination = "Office" then 50
    else if destination = "Stadium" then 25
    else if destination = "Gas" then 10
    else failwith "Unknown destination!"

let calculateRemainingPetrol(petrol, consumption) = 
    if petrol < consumption then failwith "Oops! You've run out of petrol!"
    else petrol - consumption;

let driveTo destination petrol = 
    let consumption = getConsumption(destination)
    let remaining = calculateRemainingPetrol(petrol, consumption)
    if destination = "Gas" then remaining + 50
    else remaining

let petrol = 100
petrol
|> driveTo "Office"
|> driveTo "Stadium"
|> driveTo "Gas"
|> driveTo "Home"

//let a = driveTo(100, "Office")
//let b = driveTo(a, "Stadium")
//let c = driveTo(b, "Gas")
//let answer = driveTo(c, "Home")



//let consumption = getConsumption(destination)
//if destination <> "Gas station" then petrol - consumption