# MyShoppingCart - A Portfolio

Welcome to my portfolio project for a simple shopping cart. I wasn't setting out to build a robust shopping cart for actual use, I just needed a subject to start working on to demonstrate my experience in software development for potential employers.

Some things you will find examples of in this code base:

- [Design Patterns](https://refactoring.guru/design-patterns/behavioral-patterns) not limited to:
  - The [Mediator Pattern](https://refactoring.guru/design-patterns/mediator) using the popular open source [MediatR](https://github.com/jbogard/MediatR) project. I used MediatR Pipeline Behaviors to handle cross-cutting concerns like error logging as well as request and security validation.
  - The [Options Pattern](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/configuration/options?view=aspnetcore-7.0) with option validation and option validation on startup for easy problem detection.
  - Not to mention some old favorites like the [Factory Pattern](https://refactoring.guru/design-patterns/factory-method), the [Builder Pattern](https://refactoring.guru/design-patterns/builder) and many more.
- I used a version of [Domain Driven Design](https://learn.microsoft.com/en-us/archive/msdn-magazine/2009/february/best-practice-an-introduction-to-domain-driven-design) to lay out this project, but I didn't go full-on aggregate objects etc.
- I used Entity Framework Core in this project for speed of implementation. Professionally, I mostly use [Dapper](https://github.com/DapperLib/Dapper) and write my own SQL stored procedures, but I wanted to demonstrate I could use EF as well.
- I used Microsoft's [Minimal API Framework](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/minimal-apis?view=aspnetcore-7.0). I find it easier to work with than the controller based method.
- I used JWT token authorization with roles and claims.
- I used the [Fluent Validation](https://docs.fluentvalidation.net/en/latest/) project for all of the validation needs.
- I used the [OneOf](https://github.com/mcintyre321/OneOf) package to implement Value Objects as Discriminated Unions. This cuts down on using null values and exception throwing as flow control.

My next steps are to add unit tests and a simple front-end.

Thanks for taking a look and have a great day!
