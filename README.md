<div style="text-align: center">
    <img src="assets/noprimitives.jpg" alt="logo with the letters N and P" width="300">
</div>
![Build](https://github.com/NoPrimitives/NoPrimitives/actions/workflows/main.yaml/badge.svg)

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