namespace catdance

open System
open Microsoft.AspNetCore.Mvc
open Microsoft.Extensions.Logging

open HomeModels
open UrlsStore

type HomeController (logger : ILogger<HomeController>) =
    inherit Controller()

    member this.Index () =
        let rng = Random();
        let urls = UrlsStore.getUrls
        let url = urls.[rng.Next(urls.Length)]
        let model = CatUrlModel(url)
        this.View(model)
