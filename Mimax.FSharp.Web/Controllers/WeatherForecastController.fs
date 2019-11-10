namespace Mimax.FSharp.Web.Controllers

open System
open System.Collections.Generic
open System.Linq
open System.Threading.Tasks
open Microsoft.AspNetCore.Mvc
open Microsoft.Extensions.Logging
open Mimax.FSharp.Web

type Animal = { Name : string; Species : string }

[<AutoOpen>]
module AnimalRepository =
    let all = [ { Name = "Fido"; Species = "Dog" }; { Name = "Felix"; Species = "Cat" }]
    let getAll() = all
    let getAnimal name = all |> List.tryFind(fun r -> r.Name = name)


[<ApiController>]
[<Route("[controller]")>]
type WeatherForecastController (logger : ILogger<WeatherForecastController>) =
    inherit ControllerBase()

    let summaries = [| "Freezing"; "Bracing"; "Chilly"; "Cool"; "Mild"; "Warm"; "Balmy"; "Hot"; "Sweltering"; "Scorching" |]

    [<HttpGet>]
    member __.GetWeatherForecast() : WeatherForecast[] =
        let rng = System.Random()
        [|
            for index in 0..4 ->
                { Date = DateTime.Now.AddDays(float index)
                  TemperatureC = rng.Next(-20,55)
                  Summary = summaries.[rng.Next(summaries.Length)] }
        |]

    [<HttpGet("animals")>]
    member __.GetAnimals()  =
        async { return AnimalRepository.getAll() }

    [<HttpGet("animal/{name}")>]
    member __.GetAnimalByName(name)  =
        async { return AnimalRepository.getAnimal(name) }
        
