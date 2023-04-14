# MyShoppingCart - A Portfolio

Welcome to my portfolio project for a simple shopping cart. I wasn't setting out to build a robust shopping cart for actual use, I just needed a subject to start working on to demonstrate my experience in software development for potential employers.

Some things you will find examples of in this code base:

- [SOLID Principles](https://en.wikipedia.org/wiki/SOLID) and [Design Patterns](https://refactoring.guru/design-patterns/behavioral-patterns) not limited to:
  - The [Mediator Pattern](https://refactoring.guru/design-patterns/mediator) using the popular open source [MediatR](https://github.com/jbogard/MediatR) project. I used MediatR Pipeline Behaviors to handle cross-cutting concerns like error logging as well as request and security validation.
  - The [Options Pattern](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/configuration/options?view=aspnetcore-7.0) with option validation and option validation on startup for easy problem detection.
  - The [Specification Pattern](https://deviq.com/design-patterns/specification-pattern) and the [Repository Pattern](https://learn.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/infrastructure-persistence-layer-design) using [Ardalis's Specification](https://github.com/ardalis/Specification) open-source project. I started this project with the plan to forgo the repository pattern and just use my context as a UnitOfWork, but when it came time to unit test I couldn't find a reasonable way to mock EF Core calls, even when exposing my DbContext as an injectable interface.  When using dapper, I don't typically use generic repositories but instead build out common use cases for the repository manually.
  - Not to mention some old favorites like the [Factory Pattern](https://refactoring.guru/design-patterns/factory-method), the [Builder Pattern](https://refactoring.guru/design-patterns/builder) and many more.
- I used a version of [Clean Architecture](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html) to lay out this project.  I'm torn between using this stack or the standard Presentation/Business/Data tiers architecture.  I didn't use the full [DDD](https://en.wikipedia.org/wiki/Domain-driven_design) approach but did follow some of the principles.  I definately feel like the [CQRS Pattern](https://learn.microsoft.com/en-us/azure/architecture/patterns/cqrs) is more faithful to SOLID principles than the standard Service/Repository pattern, especially when used with the Mediator Pattern.
- I used Entity Framework Core in this project for speed of implementation. Professionally, I mostly use [Dapper](https://github.com/DapperLib/Dapper) and write my own SQL stored procedures, but I wanted to demonstrate I could use EF as well.
- I used Microsoft's [Minimal API Framework](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/minimal-apis?view=aspnetcore-7.0). I find it easier to work with than the controller based method.
- I used Identity Framework for user management and JWT tokens for API authorization with roles and claims.
- I used the [Fluent Validation](https://docs.fluentvalidation.net/en/latest/) project for all of the validation needs.
- I used the [OneOf](https://github.com/mcintyre321/OneOf) package to implement Value Objects as F#-like [Discriminated Unions](https://learn.microsoft.com/en-us/dotnet/fsharp/language-reference/discriminated-unions). This cuts down on using null values and exception throwing as flow control.
- I used [xUnit](https://xunit.net/), [Moq](https://github.com/moq/) and [FluentAssertions](https://fluentassertions.com/) for unit testing
- I used React and Bootstrap for the fron-end.

My next steps are to add a simple front-end.

Thanks for taking a look and have a great day!


## Project Setup
If you want to run the project locally you will need a SQL Server instance (I use docker for this).

1. Pull down the git repo and open the solution in VS 2022.

### Setup the database
2. Open a powershell window and CD to the src/MyShoppingCart.Infrastructure project folder.
3. Run this command with your own connection string:
		
        dotnet ef database update --connection "connection string"

### Start the API project
4. Add your connection string to the appsettings.json file, or use [Secrets Manager](https://learn.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-7.0&tabs=windows).
5. Set the MyShoppingCart.Api as your Startup Project and run the debugger in https mode

### Start the front end
6. Open the React project by CD to the src/myshoppingcart.web directory and running the command (if you have VS Code installed):
        
        ./code
        
7. Pull down the dependencies by going to the VS Code terminal and typing:
        
        npm install

8. Run the front-end by entering the following command in the VS Code terminal:
        
        npm start

   Please note that the front-end is a work in progress and I'm just getting started on it. :)

9. You should see the home page with some products listed.  You can search or login and play around.  The built-in credentials are "fred.flintstone" password: Password123! and "george.jetson" password: Password123!.  George is setup as an admin and Fred is a customer.  Or you can register your own account.
10. Enjoy!