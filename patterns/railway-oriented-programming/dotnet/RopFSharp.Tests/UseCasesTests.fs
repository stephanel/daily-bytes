module UseCasesTests

open Xunit

open Rop
open Input
open UseCases

[<Fact>]
let ``validateThenPersistInput should succeed`` () =
    let input = {name="Bob"; email="email"}
    Assert.Equal(Success input, 
        processUseCase input)
