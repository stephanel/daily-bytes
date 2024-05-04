module ValidationTests

open Xunit

open Input
open Rop
open Validation

[<Fact>]
let ``Empty name should not pass validation`` () =
    let input = {name=""; email=""}
    Assert.Equal(Failure "Name must not be blank", 
        Validation.combinedValidation input)

[<Fact>]
let ``Empty email should not pass validation`` () =
    let input = {name="Bob"; email=""}
    Assert.Equal(Failure "Email must not be blank", 
        combinedValidation input)

[<Fact>]
let ``Too long email should not pass validation`` () =
    let input = {name="Bob"; email=String.replicate 11 "email"}
    Assert.Equal(Failure "Email must not be longer than 50 chars", 
        combinedValidation input)
        
[<Fact>]
let ``Validation should succeed when neither name not email is empty`` () =
    let input = {name="Bob"; email="good"}
    Assert.Equal(Success input, 
        combinedValidation input)

