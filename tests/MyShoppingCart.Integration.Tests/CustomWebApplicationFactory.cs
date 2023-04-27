using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;
using Microsoft.AspNetCore.Mvc.Testing;
using MyShoppingCart.Api;
using Testcontainers.MsSql;

namespace MyShoppingCart.Integration.Tests
{
    public class CustomWebApplicationFactory : WebApplicationFactory<IMyShoppingCartApiMarker>, IAsyncLifetime
    {
        private readonly IContainer _dbContainer = new ContainerBuilder()
                .WithImage("myshoppingcartdb:latest")
                .WithPortBinding(1433, true)
                .WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(1433))
                .Build();

        public async Task InitializeAsync()
        {
            await _dbContainer.StartAsync();
        }

        public new async Task DisposeAsync()
        {
            await _dbContainer.StopAsync();
        }
    }
}
