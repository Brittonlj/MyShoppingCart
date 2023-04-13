using System.ComponentModel.DataAnnotations;

namespace MyShoppingCart.Application.Configuration;

public sealed class JwtSettings
{
    public const string SECTION_NAME = "Jwt";

    [Required]
    public string Key { get; set; } = string.Empty;
    [Required]
    public string Issuer { get; set; } = string.Empty;
    [Required]
    public string Audience { get; set; } = string.Empty;
    [Required]
    [Range(15, 10080)]
    public int TimeoutInMinutes { get; set; }
}
