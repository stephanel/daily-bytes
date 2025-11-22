
# Functional Exercises

## Core functions

| LaYumba.Functional | LINQ | Common synonyms | LanguageExt |
| ------------------ | ---- | --------------- | ----------- |
| `Map` | `Select` | `fmap`, `Project`, `Lift` | `Map`, `Select` |
| `Bind` | `SelectMany` | `FlatMap`, `Chain`, `Collect`, `Then` | `Bind`, `SelectMany` |
| `Where` | `Where` | `Filter` | `Filter`, `Where` |
| `ForEach` | n/a | `Iter` | `Iter` |
| `Return` | n/a | `Pure` | `List` |

## Abstractions

An `abstraction` is a way to add an effect to the underlying type

| Abstraction | Effect it adds | What it is? |
| ----------- | -------------- | ----------- |
| `Option<T>` | `optionality` | the `possibility` of a `T` |
| `IEnumerable<T>` | `aggregation` | a `sequence` of a `T`'s |
| `Func<T>` | `laziness` |  a `computation` that can be evaluated to obtain a `T` |
| `Task<T>` | `asynchrony` | a `promise` taht at some point you'll get a `T` |

- `Option` adds the effect of `optionality`, which is not a `T` but the `possibility` of a `T`.
- `IEnumerable` adds the effect of `aggregation`, which is not a `T` ot two but a `sequence` of a `T`'s.
- `Func` adds the effect of `laziness`, which is not a `T` but a `computation` that can be evaluated to obtain a `T`
- `Task` adds the effect of `asynchrony`, which is not a `T` but a `promise` that a some point you'll get a `T`

