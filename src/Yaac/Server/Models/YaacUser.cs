using Microsoft.AspNetCore.Identity;

namespace Yaac.Server.Models;

public class YaacUser : IdentityUser<Guid>
{
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}