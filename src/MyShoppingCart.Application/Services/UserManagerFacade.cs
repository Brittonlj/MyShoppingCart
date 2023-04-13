using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace MyShoppingCart.Application.Services;

public sealed class UserManagerFacade : IUserManagerFacade
{
    private readonly UserManager<Customer> _userManager;
    private readonly IPasswordHasher<Customer> _passwordHasher;
    private readonly IRepository<Customer> _customerRepository;

    public UserManagerFacade(
        UserManager<Customer> userManager, 
        IPasswordHasher<Customer> passwordHasher, 
        IRepository<Customer> customerRepository)
    {
        _userManager = userManager;
        _passwordHasher = passwordHasher;
        _customerRepository = customerRepository;
    }

    public async Task<IdentityResult> CreateAsync(Customer customer, string password, CancellationToken cancellationToken = default)
    {
        return await _userManager.CreateAsync(customer, password);
    }

    public async Task<IdentityResult> UpdateAsync(Customer customer, string? password = null, CancellationToken cancellationToken = default)
    {
        if (password is not null)
        {
            customer.PasswordHash = _passwordHasher.HashPassword(customer, password);
        }
        return await _userManager.UpdateAsync(customer);
    }

    public async Task<bool> CheckPasswordAsync(Customer customer, string password)
    {
        return await _userManager.CheckPasswordAsync(customer, password);
    }

    public async Task<Customer?> FindByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var spec = new GetCustomerByIdSpec(id).WithNoTracking();
        return await _customerRepository.FirstOrDefaultAsync(spec, cancellationToken);
    }

    public async Task<Customer?> FindByUserNameAsync(string userName, CancellationToken cancellationToken = default)
    {
        var spec = new GetCustomerByUserNameSpec(userName).WithNoTracking();
        return await _customerRepository.FirstOrDefaultAsync(spec, cancellationToken);
    }

    public async Task<IdentityResult> DeleteAsync(Customer customer, CancellationToken cancellationToken = default)
    {
        return await _userManager.DeleteAsync(customer);
    }

    public async Task<List<Claim>> GetClaimsAsync(Customer customer)
    {
        var claims = await _userManager.GetClaimsAsync(customer);
        return claims.ToList();
    }

    public async Task<List<string>> GetRolesAsync(Customer customer)
    {
        var roles = await _userManager.GetRolesAsync(customer);
        return roles.ToList();
    }
}
