module PersonComparer

open Mimax.Interoperability.CSharpProject
open System.Collections.Generic

type PersonComparer() =
    interface IComparer<Person> with
        member this.Compare(x: Person, y: Person): int = 
            x.Name.CompareTo y.Name