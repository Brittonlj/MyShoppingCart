using MyShoppingCart.Api.Setup;
using MyShoppingCart.Application.Setup;
using MyShoppingCart.Infrastructure.Setup;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();

builder.Services.SetupMyShoppingCartApi(builder.Configuration);
builder.Services.SetupMyShoppingCartDomain();
builder.Services.SetupMyShoppingCartInfrastructure(builder.Configuration);
builder.Services.SetupMyShoppingCartApplication();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.SetupUseCors();
app.UseAuthentication();
app.UseAuthorization();

app.RegisterMyShoppingCartEndpoints();

app.Run();
