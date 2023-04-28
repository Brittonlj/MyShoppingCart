using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;
using Microsoft.AspNetCore.Mvc.Testing;
using MyShoppingCart.Api;

namespace MyShoppingCart.Integration.Tests
{
    public class CustomWebApplicationFactory : WebApplicationFactory<IMyShoppingCartApiMarker>, IAsyncLifetime
    {
        private readonly IContainer _dbContainer = new ContainerBuilder()
                .WithImage("myshoppingcartdb:latest")
                .WithPortBinding(1433, 1433)
                .WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(1433))
                .Build();

        public HttpClient HttpClient { get; private set; } = default!;

        public async Task InitializeAsync()
        {
            await _dbContainer.StartAsync();
            HttpClient = CreateClient();
        }

        public new async Task DisposeAsync()
        {
            await _dbContainer.StopAsync();
        }
    }
}
