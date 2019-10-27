
let text = "Hello, world"
text.Length
let greetPerson name age =
    sprintf "Hello, %s. You are %d years old" name age
let greeting = greetPerson "Fred" 25
let countWords (text:string) =
    let count = text.Split().Length
    sprintf "%s" System.Environment.CurrentDirectory
    System.IO.File.WriteAllText("test.txt", text)
    sprintf "Number of words: %d" count
let test = countWords "1 2 3 4"