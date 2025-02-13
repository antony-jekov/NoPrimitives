<div align="center">
    <img src="assets/noprimitives.jpg" alt="logo with the letters N and P" width="600">
</div>

![tests workflow](https://github.com/antony-jekov/NoPrimitives/actions/workflows/main.yml/badge.svg)
[![codecov](https://codecov.io/gh/antony-jekov/NoPrimitives/branch/main/graph/badge.svg?token=wSdo9LnqaP)](https://codecov.io/gh/antony-jekov/NoPrimitives)


# No Primitives: Value Objects the easy way!
## Forget the boilerplate nonsense!

It holds your hand from declaring your Value Object to using it in a database, to serializing it to JSON and beyond.
No boilerplate code, just a simple Attribute!

## Install
```shell
dotnet add package NoPrimitives
```

## Usage
```csharp
[ValueObject<Guid>]
partial record UserId;

...

app.MapGet("/v1/users/{{id}}", async (UserId id) => {
    // use your value object and enjoy it being always valid!
})
```

## Test Coverage Graph
![codecov](https://codecov.io/gh/antony-jekov/NoPrimitives/graphs/tree.svg?token=wSdo9LnqaP)
