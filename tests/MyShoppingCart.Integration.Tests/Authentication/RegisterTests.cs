﻿using MyShoppingCart.Application.Authentication;
namespace MyShoppingCart.Integration.Tests.Authentication
{
    public class RegisterTests
    {
        private readonly HttpClient _client;

        public RegisterTests()
        {
            var factory = new CustomWebApplicationFactory();
            _client = factory.CreateClient();
        }


        [Fact]
        public async Task Register_ReturnsSuccess_WhenParametersAreCorrect()
        {
            //Arrange
            const string FIRST_NAME = "FirstName";
            const string LAST_NAME = "LastName";
            const string EMAIL = "email@test.com";
            const string PASSWORD = "password123!";
            const string USERNAME = "username";
            const string STREET = "123 Test St.";
            const string CITY = "Test City";
            const string STATE = "TS";
            const string POSTAL_CODE = "12345";

            var address = new AddressModel(STREET, CITY, STATE, POSTAL_CODE);
            var request = new RegisterQuery(FIRST_NAME, LAST_NAME, EMAIL, USERNAME, PASSWORD, address, address);

            //Act
            var response = await _client.PostAsJsonAsync("/authentication/register", request);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var customer = await response.Content.ReadFromJsonAsync<CustomerModel>();
            customer.Should().NotBeNull();
            customer.FirstName.Should().Be(FIRST_NAME);
            customer.LastName.Should().Be(LAST_NAME);
            customer.Email.Should().Be(EMAIL);
            customer.UserName.Should().Be(USERNAME);
            customer.ShippingAddress.Street.Should().Be(STREET);
            customer.ShippingAddress.City.Should().Be(CITY);
            customer.ShippingAddress.State.Should().Be(STATE);
            customer.ShippingAddress.PostalCode.Should().Be(POSTAL_CODE);
            customer.BillingAddress.Street.Should().Be(STREET);
            customer.BillingAddress.City.Should().Be(CITY);
            customer.BillingAddress.State.Should().Be(STATE);
            customer.BillingAddress.PostalCode.Should().Be(POSTAL_CODE);

        }

    }
}