// Learn more about F# at http://fsharp.org

open System

[<EntryPoint>]
let main argv =
    let random = System.Random()
    let pickANumberAsync = async { return random.Next(10) }
    let createFiftyNumbers =
        let workflows = [ for i in 1..50 -> pickANumberAsync ]
        async {
            let! numbers = workflows |> Async.Parallel 
            printfn "Total is %d" (numbers |> Array.sum) }
    createFiftyNumbers |> Async.RunSynchronously

    let downloadData url = async {
        use ws = new System.Net.WebClient()
        printfn "Downoloading data on thread %d" System.Threading.Thread.CurrentThread.ManagedThreadId
        let! data = ws.DownloadDataTaskAsync(System.Uri url) |> Async.AwaitTask
        return data.Length }

    let downloadTask = 
        [ "http://google.com"; "http://microsoft.com" ]
        |> Seq.map downloadData
        |> Async.Parallel
        |> Async.StartAsTask

    downloadTask.Result |> Array.iter (fun n -> printfn "Downloaded %d bytes" n)

    0 // return an integer exit code    