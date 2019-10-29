open System
open System.IO
let writeToFile (date:DateTime) filename text = 
    let path = sprintf "%s-%s.txt" (date.ToString "yyMMdd") filename
    File.WriteAllText(path, text)

let writeToToday = writeToFile DateTime.UtcNow.Date
let writeToTomorrow = writeToFile (DateTime.UtcNow.Date.AddDays 1.0)
let writeToTodayHelloWorld = writeToToday "hello-world"

printf "%s" Environment.CurrentDirectory

writeToToday "first-file" "Text"
writeToTomorrow "second-file" "Text"
writeToTodayHelloWorld "Text"