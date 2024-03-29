module Email

    open Input

    let canonicalizeEmail input =
        { input with email = input.email.Trim().ToLower() }
