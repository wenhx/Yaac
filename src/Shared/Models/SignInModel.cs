using System.ComponentModel.DataAnnotations;
using static Yaac.Shared.Constants.Models;

namespace Yaac.Shared.Models;

public class SignInModel
{
    [Required]
    [StringLength(MaximumUserNameLength, MinimumLength = MinimumUserNameLength)]
    public string UserName { get; set; } = string.Empty;

    [Required]
    [StringLength(MaximumPasswordLength, MinimumLength = MinimumPasswordLength)]
    public string Password { get; set; } = string.Empty;

    /// <summary>
    /// The RememberMe function is not yet implemented
    /// </summary>
    public bool RememberMe { get; set; }
}
