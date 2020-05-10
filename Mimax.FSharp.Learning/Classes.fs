module Classes

open System

type Class =
    class
    end

type Class1 =
    class
        val field1 : string
        val field2 : string
    end

type Class2 =
    val field1 : string
    val field2 : string

[<Class>]
type Class3 =
    val field1 : string
    val mutable field2 : string

[<Class>]
type Class4 =
    val field1 : string
    val mutable field2 : string
    new (f1, f2) = { field1 = f1; field2 = f2 }

[<Class>]
type Class5 =
    val field1 : string
    val mutable field2 : string
    new () = { field1 = String.Empty; field2 = String.Empty }
    new (f1, f2) = { field1 = f1; field2 = f2 }

[<Class>]
type Class6 =
    val field1 : string
    val mutable field2 : string
    new () = new Class6(String.Empty, String.Empty)
    new (f1, f2) = { field1 = f1; field2 = f2 }

[<Class>]
type Class7 =
    val field1 : string
    val mutable field2 : string
    new () = new Class7(String.Empty, String.Empty) then
        Console.WriteLine("Making Class7");
    new (f1, f2) = { field1 = f1; field2 = f2 }

[<Class>]
type Class8 =
    val field1 : string
    val mutable field2 : string
    new () as this = new Class8(String.Empty, String.Empty) then
        Console.WriteLine("Fields {0} {1}", this.field1, this.field2)
    new (f1, f2) as this1 = { field1 = f1; field2 = f2 } then
        Console.WriteLine("Fields {0} {1}", this1.field1, this1.field2)

[<Class>]
type Class9(f1 : string, f2 : string) as this =
    [<DefaultValue>]
    val mutable fields : string
    let temp = f1 + f2
    do
        this.fields <- temp
        Console.WriteLine("Primary constructor invoked for Class9")
    new () as this = new Class9(String.Empty, String.Empty) then
        Console.WriteLine("Fields {0}", this.fields)
    member this.IsClass9 = true
    member this.Do() =
        "Done" + " " + this.fields
    member this.Brain
        with get() = "Brain" + " " + this.fields
        and set(fields) = this.fields <- fields

type Class9 with
    member this.Foo() = this.Do() + " " + "Foo"

type Class10(fld1, fld2, a) =
    member this.Field1 = fld1
    member this.Field2 = fld2
    member this.Age = a

type Class11(fld1, fld2, a, fld3, fld4) =
    inherit Class10(fld1, fld2, a)
    member this.Field3 = fld3
    member this.Field4 = fld4

[<Class>]
type Misha =
    member this.Eat() =
        Console.WriteLine("Misha is eating");
    member this.Sleep() = 
        Console.WriteLine("Misha is sleeping");
    member this.Code() = 
        Console.WriteLine("Misha is coding");
    abstract Live : unit -> unit
    default this.Live() =
        while true do
            this.Sleep()
            this.Eat()
            this.Code()

[<Class>]
type Sonya =
    inherit Misha
    member this.Play(misha) =
        Console.WriteLine("Sonya plays with Misha")
    override this.Live() =
        while true do
            this.Sleep()
            this.Eat()
            this.Play()        
