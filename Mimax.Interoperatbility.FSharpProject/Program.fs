// Learn more about F# at http://fsharp.org

open System
open PersonComparer
open System.Collections.Generic
open Mimax.Interoperability.CSharpProject

[<EntryPoint>]
let main argv =
    let andy = Mimax.Interoperability.CSharpProject.Person "Andy"
    let simon = Mimax.Interoperability.CSharpProject.Person "Simon"

    let comparer = PersonComparer() :> IComparer<Person>

    let result = comparer.Compare(andy, simon)


    
    0 // return an integer exit code
