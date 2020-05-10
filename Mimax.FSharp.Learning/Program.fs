open System
open Classes

[<EntryPoint>]
let main argv =
    
    let inputData = "This is some <script>alert()</script> data";

    let (|Contains|_|) (pat : string) (inStr : string) =
        let results =
            (System.Text.RegularExpressions.Regex.Matches(inStr, pat))
        if results.Count > 0
            then Some [for m in results -> m.Value]
            else None

    let matchStuff inData = 
        match inData with
        | Contains "http://S+" _ -> "Contains urls"
        | Contains "<script>" _ -> "Contains <script>"
        | _ -> "Didn't find what we were looking for"

    System.Console.WriteLine(matchStuff inputData)


    let tuple = ("This", "is", 1, "tuple");

    let class9 = new Class9("Test", "1");

    let result = class9.Foo()





    0 // return an integer exit code
