# MyShoppingCart - A Portfolio

Welcome to my portfolio project for a simple shopping cart. I wasn't setting out to build a robust shopping cart for actual use, I just needed a subject to start working on to demonstrate my experience in software development for potential employers.

Some things you will find examples of in this code base:

- [Design Patterns](https://refactoring.guru/design-patterns/behavioral-patterns) not limited to:
  - The [Mediator Pattern](https://refactoring.guru/design-patterns/mediator) using the popular open source [MediatR](https://github.com/jbogard/MediatR) project. I used MediatR Pipeline Behaviors to handle cross-cutting concerns like error logging as well as request and security validation.
  - The [Options Pattern](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/configuration/options?view=aspnetcore-7.0) with option validation and option validation on startup for easy problem detection.
  - The [Specification Pattern](https://deviq.com/design-patterns/specification-pattern) and the [Repository Pattern](https://learn.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/infrastructure-persistence-layer-design) using [Ardalis's Specification](https://github.com/ardalis/Specification) open-source project.
  - Not to mention some old favorites like the [Factory Pattern](https://refactoring.guru/design-patterns/factory-method), the [Builder Pattern](https://refactoring.guru/design-patterns/builder) and many more.
- I used a version of [Clean Architecture](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html) to lay out this project.
- I used Entity Framework Core in this project for speed of implementation. Professionally, I mostly use [Dapper](https://github.com/DapperLib/Dapper) and write my own SQL stored procedures, but I wanted to demonstrate I could use EF as well.
- I used Microsoft's [Minimal API Framework](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/minimal-apis?view=aspnetcore-7.0). I find it easier to work with than the controller based method.
- I used JWT token authorization with roles and claims.
- I used the [Fluent Validation](https://docs.fluentvalidation.net/en/latest/) project for all of the validation needs.
- I used the [OneOf](https://github.com/mcintyre321/OneOf) package to implement Value Objects as F#-like [Discriminated Unions](https://learn.microsoft.com/en-us/dotnet/fsharp/language-reference/discriminated-unions). This cuts down on using null values and exception throwing as flow control.
- I used [xUnit](https://xunit.net/), [Moq](https://github.com/moq/) and [FluentAssertions](https://fluentassertions.com/) for unit testing

My next steps are to add unit tests and a simple front-end.

Thanks for taking a look and have a great day!
