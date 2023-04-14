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
        _userManager = Guard.Against.Null(userManager);
        _passwordHasher = Guard.Against.Null(passwordHasher);
        _customerRepository = Guard.Against.Null(customerRepository);
    }

    public async Task<IdentityResult> CreateAsync(Customer customer, string password, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(customer);
        ArgumentException.ThrowIfNullOrEmpty(password);

        return await _userManager.CreateAsync(customer, password);
    }

    public async Task<IdentityResult> UpdateAsync(Customer customer, string? password = null, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(customer);

        if (password is not null)
        {
            customer.PasswordHash = _passwordHasher.HashPassword(customer, password);
        }
        return await _userManager.UpdateAsync(customer);
    }

    public async Task<bool> CheckPasswordAsync(Customer customer, string password)
    {
        ArgumentNullException.ThrowIfNull(customer);
        ArgumentException.ThrowIfNullOrEmpty(password);

        return await _userManager.CheckPasswordAsync(customer, password);
    }

    public async Task<Customer?> FindByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrEmpty(name);

        var spec = new GetCustomerByUserNameSpec(name);
        return await _customerRepository.FirstOrDefaultAsync(spec, cancellationToken);
    }

    public async Task<Customer?> FindByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var spec = new GetCustomerByIdSpec(id);
        return await _customerRepository.FirstOrDefaultAsync(spec, cancellationToken);
    }

    public async Task<Customer?> FindByUserNameAsync(string userName, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrEmpty(userName);

        var spec = new GetCustomerByUserNameSpec(userName);
        return await _customerRepository.FirstOrDefaultAsync(spec, cancellationToken);
    }

    public async Task<IdentityResult> DeleteAsync(Customer customer)
    {
        ArgumentNullException.ThrowIfNull(customer);

        return await _userManager.DeleteAsync(customer);
    }

    public async Task<List<Claim>> GetClaimsAsync(Customer customer)
    {
        ArgumentNullException.ThrowIfNull(customer);

        var claims = await _userManager.GetClaimsAsync(customer);
        return claims.ToList();
    }

    public async Task<List<string>> GetRolesAsync(Customer customer)
    {
        ArgumentNullException.ThrowIfNull(customer);

        var roles = await _userManager.GetRolesAsync(customer);
        return roles.ToList();
    }

    public async Task<IdentityResult> AddToRoleAsync(Customer customer, string roleName)
    {
        ArgumentNullException.ThrowIfNull(customer);
        ArgumentException.ThrowIfNullOrEmpty(roleName);

        return await _userManager.AddToRoleAsync(customer, roleName);
    }

    public async Task<IdentityResult> AddClaimAsync(Customer customer, Claim claim)
    {
        ArgumentNullException.ThrowIfNull(customer);
        ArgumentNullException.ThrowIfNull(claim);

        return await _userManager.AddClaimAsync(customer, claim);
    }

    public async Task<IdentityResult> ChangePasswordAsync(Customer customer, string currentPassword, string newPassword)
    {
        ArgumentNullException.ThrowIfNull(customer);
        ArgumentException.ThrowIfNullOrEmpty(currentPassword);
        ArgumentException.ThrowIfNullOrEmpty(newPassword);

        return await _userManager.ChangePasswordAsync(customer, currentPassword, newPassword);
    }

    public async Task<IdentityResult> RemovePasswordAsync(Customer customer)
    {
        ArgumentNullException.ThrowIfNull(customer);

        return await _userManager.RemovePasswordAsync(customer);
    }

    public async Task<IdentityResult> AddPasswordAsync(Customer customer, string password)
    {
        ArgumentNullException.ThrowIfNull(customer);
        ArgumentException.ThrowIfNullOrEmpty(password);

        return await _userManager.AddPasswordAsync(customer, password);
    }
}
