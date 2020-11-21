module Explorator.FsApi.Program

open Falco
open Falco.Routing
open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Hosting
open Microsoft.Extensions.DependencyInjection
open Microsoft.Extensions.Hosting

// ------------
// Web app
// ------------
let endpoints =
    [            
        get "/" (Response.ofPlainText "Hello world")
    ]

// ------------
// Register services
// ------------
let configureServices (services : IServiceCollection) =
    services.AddFalco() |> ignore

// ------------
// Activate middleware
// ------------
let configureApp (app : IApplicationBuilder) =    
    app.UseFalco(endpoints) |> ignore

[<EntryPoint>]
let main args =    
    try
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(fun webhost ->   
                webhost
                    .ConfigureServices(configureServices)
                    .Configure(configureApp)
                    |> ignore)
            .Build()
            .Run()                        
        0
    with 
    | ex -> 
        printfn $"%s{ex.Message}\n\n%s{ex.StackTrace}"
        -1