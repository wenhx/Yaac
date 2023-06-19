using System.ComponentModel.DataAnnotations;

namespace Yaac.Server;

public class AuthOptions
{
    [StringLength(1024, MinimumLength = 32)]
    public string TokenKey { get; set; } = default!;
}
