#r "C:\\Users\\mi\\.nuget\\packages\\fsharp.data\\3.3.2\\lib\\net45\\FSharp.Data.dll"

open System
open FSharp.Data

type Package = HtmlProvider< @"C:\Users\mi\source\repos\Mimax\Mimax.FSharp.Data\sample-package.html">

let getPackage packageName = 
    packageName |> sprintf "https://www.nuget.org/packages/%s" |> Package.Load

let getVersionsForPackage (package:Package) =
    package.Tables.``Version History``.Rows

type PackageVersion =
    | CurrentVersion
    | Prerelease
    | Old

type VersionDetails =
    { Version : Version
      Downloads : decimal
      PackageVersion : PackageVersion
      LastUpdated : DateTime }

type NuGetPackage =
    { PackageName : string
      Versions : VersionDetails list }
    
let parse (versionText:string) =
    let getVersionPart (version:string) isCurrent =
        match version.Split '-', isCurrent with
        | [| version; _ |], true
        | [| version |], true -> Version.Parse version, CurrentVersion
        | [| version; _ |], false -> Version.Parse version, Prerelease
        | [| version |], false -> Version.Parse version, Old
        | _ -> failwith "unknown version format"
    
    let parts = versionText.Split ' ' |> Seq.toList |> List.rev
    match parts with
    | [] -> failwith "Must be at least two elements to a version"
    | "version)" :: "(this" :: version :: _ -> getVersionPart version true
    | version :: _ -> getVersionPart version false
    
let enrich (versionHistory:Package.VersionHistory.Row seq) = 
    { PackageName =
        match versionHistory |> Seq.map(fun row -> row.Version.Split ' ' |> Array.toList |> List.rev) |> Seq.head with
        | "version)" :: "(this" :: _ :: name | _ :: name -> List.rev name |> String.concat " "
        | _ -> failwith "Unable to parse version name"
      Versions =
        versionHistory 
        |> Seq.map(fun versionHistory ->
            let version, packageVersion = parse versionHistory.Version
            { Version = version
              Downloads = versionHistory.Downloads
              LastUpdated = versionHistory.``Last updated``
              PackageVersion = packageVersion })
        |> Seq.toList }
    
let loadPackageVersions = getPackage >> getVersionsForPackage >> enrich >> (fun p -> p.Versions)
let getDetailsForVersion version = loadPackageVersions >> Seq.find(fun p -> p.Version = version)
let getDetailsForCurrentVersion = loadPackageVersions >> Seq.find(fun p -> p.PackageVersion = CurrentVersion)
    
"Newtonsoft.Json" |> getDetailsForVersion (Version.Parse "9.0.1")
    
let getDownloadsForPackage = loadPackageVersions >> Seq.sumBy(fun p -> p.Downloads)
    