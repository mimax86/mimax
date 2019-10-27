module Car

let getConsumption(destination) = 
    if destination = "Home" then 25.0
    else if destination = "Office" then 50.0
    else if destination = "Stadium" then 25.0
    else if destination = "Gas" then 10.0
    else failwith "Unknown destination!"

let calculateRemainingPetrol(petrol, consumption) = 
    if petrol < consumption then failwith "Oops! You've run out of petrol!"
    else petrol - consumption;

let driveTo(petrol, destination) = 
    let consumption = getConsumption(destination)
    let remaining = calculateRemainingPetrol(petrol, consumption)
    if destination = "Gas" then remaining + 50.0
    else remaining