module BusinessLogicTests

open BusinessLogic
open Xunit

let isTrue (b:bool) = Assert.True b

[<Fact>]
let isLargeAndYoungTeam_TeamIsLargeAndYoung_ReturnsTrue() =
    let department =
        { Name = "Super Team"
          Team = [ for i in 1..15 -> { Name = sprintf "Person %d" i ; Age = 19}]}
    Assert.True(department |> isLargeAndYoungTeam)

let ``Large, young teams are correctly identified``() =
    let department =
        { Name = "Super Team"
          Team = [ for i in 1..15 -> { Name = sprintf "Person %d" i ; Age = 19}]}
    department |> isLargeAndYoungTeam |> Assert.True
    department |> isLargeAndYoungTeam |> isTrue