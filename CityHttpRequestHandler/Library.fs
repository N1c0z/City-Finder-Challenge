namespace CityHttpRequestHandler

open System.Net.Http
open System.Threading.Tasks

module HttpHandlerThingy =
    let GetReponse postalOrZip country =
        use client = new HttpClient()
        let response = 
            client.GetStringAsync($"http://api.zippopotam.us/{country}/{postalOrZip}")
            |> Async.AwaitTask
            |> Async.RunSynchronously
        response
    
