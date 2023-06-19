using System.ComponentModel.DataAnnotations;
using static Yaac.Shared.Constants.Models;

namespace Yaac.Shared.Models;

public class SignUpModel
{
    [Required]
    [StringLength(MaximumUserNameLength, MinimumLength = MinimumUserNameLength)]
    public string UserName { get; set; } = String.Empty;

    [Required]
    [StringLength(MaximumPasswordLength, MinimumLength = MinimumPasswordLength)]
    public string Password { get; set; } = String.Empty;

    [Required]
    [Compare(nameof(Password), ErrorMessage = "Passwords do not match")]
    public string PasswordConfirm { get; set; } = String.Empty;
}
