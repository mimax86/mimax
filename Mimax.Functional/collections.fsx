
type FootbalResult = 
    { HomeTeam : string
      AwayTeam : string
      HomeGoals : int
      AwayGoals : int }

let create (ht, hg) (at, ag) =
    { HomeTeam = ht; HomeGoals = hg; AwayTeam = at; AwayGoals = ag }

let results = 
    [ create ("Messiville", 1) ("Ronaldo City", 2)
      create ("Messiville", 1) ("Bale Town", 3) 
      create ("Bale Town", 3) ("Ronaldo City", 1) 
      create ("Bale Town", 2) ("Messiville", 1) 
      create ("Ronaldo City", 4) ("Messiville", 2) 
      create ("Ronaldo City", 1) ("Bale Town", 2) ]

let winners =
    results
    |> Seq.filter (fun result -> result.AwayGoals > result.HomeGoals)
    |> Seq.groupBy (fun result -> result.AwayTeam)
    |> Seq.map (fun (team, wins) -> (team, wins |> Seq.length))
    |> Seq.sortByDescending (fun (_, wins) -> wins)
    |> Seq.toList

open System.IO

let paths = System.IO.Directory.EnumerateDirectories "C:\\"
paths
|> Seq.map (fun path -> new DirectoryInfo(path))
|> Seq.map (fun directory -> (directory.Name, directory.CreationTimeUtc))
|> Map.ofSeq
|> Map.map (fun name time -> System.DateTime.UtcNow - time)

let fold (folder:('U -> 'T -> 'U)) (state:'U) (inputs:seq<'T>) : 'U = 
    let mutable accumulator = state 
    for input in inputs do
        accumulator <- folder accumulator input
    accumulator

let items = [ 1; 2; 3; 40]
items |> fold (fun (x:float) y -> -x * 10.0 - (float)y) 0.0
let length = items |> fold (fun x y -> x + 1) 0
let maximum = items |> fold (fun x y -> if y > x then y else x) items.[0]
let minimum = items |> fold (fun x y -> if y < x then y else x) items.[0]

let length1 = items |> Seq.fold (fun state x -> state + 1) 0
let maximum1 = items |> Seq.fold (fun state x -> if state > x then state else x) items.[0]
let minimum1 = items |> Seq.fold (fun state x -> if state < x then state else x) items.[0]

(items.[0], items) ||> Seq.fold (fun state x -> if state > x then state else x)

items |> Seq.reduce (fun state x -> if state > x then state else x)