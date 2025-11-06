namespace Functional.Exercises.Features;

internal record Subject
(
    Option<Age> Age,
    Option<Gender> Gender
);

internal enum Risk { Low, Medium };