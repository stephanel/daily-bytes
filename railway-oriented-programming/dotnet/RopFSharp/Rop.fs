module Rop

    type Result<'TSuccess, 'TFailure> =
        | Success of 'TSuccess
        | Failure of 'TFailure

    let bind switchFunction =
        function
        | Success s -> switchFunction s
        | Failure f -> Failure f

    // apply either success function or failure function
    let either successFunc failureFunc twoTrackInput =
        match twoTrackInput with
        | Success s -> successFunc s
        | Failure f -> failureFunc f

    // convert a single value into a two-track result
    let succeed x = Success x

    // convert a single value into a two-track result
    let fail x = Failure x

    // convert a one-track function into a two-track function
    let map f = either (f >> succeed) fail
    