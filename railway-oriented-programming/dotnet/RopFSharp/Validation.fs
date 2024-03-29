module Validation

    open Input
    open Rop
    
    let validateInput input =
        if input.name = "" then Failure "Name must not be blank"
        else if input.email  = "" then Failure "Email must not be blank"
        else Success input

    let nameIsNotEmpty input =
        if input.name = "" then Failure "Name must not be blank"
        else Success input

    let emailLengthIsLessThan50Chars input =
        if input.email.Length > 50 then Failure "Email must not be longer than 50 chars"
        else Success input

    let emailIsNotEmpty input =
        if input.email  = "" then Failure "Email must not be blank"
        else Success input

    let combinedValidation = 
        // connect the two-tracks together
        nameIsNotEmpty 
        >> bind emailLengthIsLessThan50Chars 
        >> bind emailIsNotEmpty

    // let combinedValidation = 
    //     // convert from switch to two-track input
    //     let validate2' = bind validate2
    //     let validate3' = bind validate3
    //     // connect the two-tracks together
    //     validate1 >> validate2' >> validate3'
