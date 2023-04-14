using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace MyShoppingCart.Application.Services;

public interface IUserManagerFacade
{
    Task<IdentityResult> CreateAsync(Customer customer, string password, CancellationToken cancellationToken = default);
    Task<Customer?> FindByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<bool> CheckPasswordAsync(Customer customer, string password);
    Task<IdentityResult> UpdateAsync(Customer customer, string? password = null, CancellationToken cancellationToken = default);
    Task<Customer?> FindByUserNameAsync(string userName, CancellationToken cancellationToken = default);
    Task<List<Claim>> GetClaimsAsync(Customer customer);
    Task<List<string>> GetRolesAsync(Customer customer);
    Task<IdentityResult> DeleteAsync(Customer customer);
    Task<Customer?> FindByNameAsync(string name, CancellationToken cancellationToken = default);
    Task<IdentityResult> AddToRoleAsync(Customer customer, string roleName);
    Task<IdentityResult> AddClaimAsync(Customer customer, Claim claim);
    Task<IdentityResult> ChangePasswordAsync(Customer customer, string currentPassword, string newPassword);
    Task<IdentityResult> RemovePasswordAsync(Customer customer);
    Task<IdentityResult> AddPasswordAsync(Customer customer, string password);
}