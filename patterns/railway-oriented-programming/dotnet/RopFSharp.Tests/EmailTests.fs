module EmailTests

open Xunit

open Input
open Email


[<Fact>]
let ``Uppercase email should be chnaged to lowercase`` () =
    let input = {name="Bob"; email="EMAIL"}
    let result = canonicalizeEmail input
    Assert.Equal({name="Bob"; email="email"}, result)

[<Fact>]
let ``Untrimmed email should be trimmed`` () =
    let input = {name="Bob"; email="    email   "}
    let result = canonicalizeEmail input
    Assert.Equal({name="Bob"; email="email"}, result)
