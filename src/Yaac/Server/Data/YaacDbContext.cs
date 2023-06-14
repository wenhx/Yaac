using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Yaac.Server.Models;

namespace Yaac.Server.Data;

public class YaacDbContext : IdentityDbContext<YaacUser, IdentityRole<Guid>, Guid>
{
    public YaacDbContext(DbContextOptions<YaacDbContext> options) : base(options)
    {
    }
}