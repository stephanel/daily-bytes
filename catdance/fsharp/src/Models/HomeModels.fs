namespace catdance

module HomeModels =
    type CatUrlModel(url: string) =
        member this.Url with get () = url