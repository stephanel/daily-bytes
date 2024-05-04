module UseCases

    open Rop
    open Validation
    open Email

    let processUseCase =
        nameIsNotEmpty 
        >> bind emailLengthIsLessThan50Chars 
        >> bind emailIsNotEmpty
        >> map canonicalizeEmail